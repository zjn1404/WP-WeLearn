package com.welearn.WeLearnApp.service.authentication;

import com.nimbusds.jose.*;
import com.nimbusds.jose.crypto.MACSigner;
import com.nimbusds.jose.crypto.MACVerifier;
import com.nimbusds.jwt.JWTClaimsSet;
import com.nimbusds.jwt.SignedJWT;
import com.welearn.WeLearnApp.dto.request.authentication.AuthenticationRequest;
import com.welearn.WeLearnApp.dto.request.authentication.IntrospectRequest;
import com.welearn.WeLearnApp.dto.request.authentication.LogoutRequest;
import com.welearn.WeLearnApp.dto.response.AuthenticationResponse;
import com.welearn.WeLearnApp.dto.response.IntrospectResponse;
import com.welearn.WeLearnApp.entity.InvalidatedToken;
import com.welearn.WeLearnApp.entity.User;
import com.welearn.WeLearnApp.exception.AppException;
import com.welearn.WeLearnApp.exception.ErrorCode;
import com.welearn.WeLearnApp.repository.InvalidatedTokenRepository;
import com.welearn.WeLearnApp.repository.UserRepository;
import com.welearn.WeLearnApp.service.verificationcode.VerificationCodeService;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.experimental.NonFinal;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.time.Instant;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.time.ZonedDateTime;
import java.time.temporal.ChronoUnit;
import java.util.Date;
import java.util.UUID;

@Slf4j
@Service
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class AuthenticationServiceImp implements AuthenticationService{

    @Value("${jwt.signer-key.access}")
    @NonFinal
    String AC_SIGNER_KEY;

    @Value("${jwt.signer-key.refresh}")
    @NonFinal
    String RF_SIGNER_KEY;

    @Value("${jwt.duration.access}")
    @NonFinal
    long AC_DURATION;

    @Value("${jwt.duration.refresh}")
    @NonFinal
    long RF_DURATION;

    UserRepository userRepository;
    InvalidatedTokenRepository invalidatedTokenRepository;

    VerificationCodeService verificationCodeService;

    PasswordEncoder passwordEncoder;

    @Override
    public AuthenticationResponse authenticate(AuthenticationRequest request) {
        User user = userRepository.findByUsername(request.getUsername())
                .orElseThrow(() -> new AppException(ErrorCode.AUTHENTICATION_FAIL));

        if (!verificationCodeService.isVerified(user.getId())) {
            throw new AppException(ErrorCode.NOT_VERIFIED);
        }

        if (!passwordEncoder.matches(request.getPassword(), user.getPassword())) {
            throw new AppException(ErrorCode.AUTHENTICATION_FAIL);
        }

        return buildAuthenticationResponse(user);
    }

    @Override
    public AuthenticationResponse refreshToken(String refreshToken) {
        SignedJWT signedJWT = verifyToken(refreshToken, true);

        try {
            String acId = signedJWT.getJWTClaimsSet().getStringClaim("acId");
            String rfId = signedJWT.getJWTClaimsSet().getStringClaim("id");
            Date expirationTime = signedJWT.getJWTClaimsSet().getExpirationTime();

            saveInvalidatedToken(acId, rfId, expirationTime);

            User user = userRepository.findById(signedJWT.getJWTClaimsSet().getSubject())
                    .orElseThrow(() -> new AppException(ErrorCode.USER_NOT_FOUND));

            return buildAuthenticationResponse(user);
        } catch (Exception e) {
            throw new AppException(ErrorCode.UNCATEGORIZED_EXCEPTION);
        }
    }

    @Override
    public IntrospectResponse introspect(IntrospectRequest request) {
        boolean valid = true;
        try {
            verifyToken(request.getToken(), false);
        } catch (Exception ex) {
            valid = false;
        }

        return IntrospectResponse.builder()
                .isValid(valid)
                .build();
    }

    @Override
    public void logout(LogoutRequest request) {
        SignedJWT signedJWT = verifyToken(request.getToken(), false);
        try {
            String acId = signedJWT.getJWTClaimsSet().getStringClaim("id");
            String rfId = signedJWT.getJWTClaimsSet().getStringClaim("rfId");
            Date expirationTime = signedJWT.getJWTClaimsSet().getExpirationTime();

            saveInvalidatedToken(acId, rfId, expirationTime);

        } catch (Exception e) {
            throw new AppException(ErrorCode.UNCATEGORIZED_EXCEPTION);
        }
    }

    private SignedJWT verifyToken(String token, boolean isRefresh) {
        try {
            SignedJWT signedJWT = SignedJWT.parse(token);
            JWSVerifier verifier = isRefresh ? new MACVerifier(RF_SIGNER_KEY) : new MACVerifier(AC_SIGNER_KEY);

            if (!signedJWT.verify(verifier)) {
                throw new AppException(ErrorCode.INVALID_TOKEN);
            }

            if (invalidatedTokenRepository.existsByTokenId(signedJWT.getJWTClaimsSet().getStringClaim("id"))) {
                throw new AppException(ErrorCode.INVALID_TOKEN);
            }

            Date expirationTime = signedJWT.getJWTClaimsSet().getExpirationTime();
            if (expirationTime.before(new Date())) {
                throw new AppException(ErrorCode.INVALID_TOKEN);
            }

            return signedJWT;

        } catch (Exception e) {
            throw new AppException(ErrorCode.UNCATEGORIZED_EXCEPTION);
        }
    }

    private AuthenticationResponse buildAuthenticationResponse(User user) {
        String acId = UUID.randomUUID().toString();
        String rfId = UUID.randomUUID().toString();
        return AuthenticationResponse.builder()
                .accessToken(generateToken(user, AC_SIGNER_KEY, AC_DURATION, acId, rfId))
                .refreshToken(generateToken(user, RF_SIGNER_KEY, RF_DURATION, rfId, acId))
                .build();
    }

    private String generateToken(User user, String signerKey, long duration, String id, String otherId) {
        JWSHeader header = new JWSHeader.Builder(JWSAlgorithm.HS512).build();

        JWTClaimsSet claimsSet = null;

        if (signerKey.equals(AC_SIGNER_KEY)) {
            claimsSet = new JWTClaimsSet.Builder()
                    .subject(user.getId())
                    .issueTime(new Date())
                    .expirationTime(new Date(Instant.now().plus(duration, ChronoUnit.SECONDS).toEpochMilli()))
                    .claim("scope", user.getRole().getName())
                    .claim("id", id)
                    .claim("rfId", otherId)
                    .build();
        } else {
            claimsSet = new JWTClaimsSet.Builder()
                    .subject(user.getId())
                    .issueTime(new Date())
                    .expirationTime(new Date(Instant.now().plus(duration, ChronoUnit.SECONDS).toEpochMilli()))
                    .claim("scope", user.getRole().getName())
                    .claim("id", id)
                    .claim("acId", otherId)
                    .build();
        }

        Payload payload = new Payload(claimsSet.toJSONObject());

        JWSObject jwsObject = new JWSObject(header, payload);

        try {
            jwsObject.sign(new MACSigner(signerKey));
            return jwsObject.serialize();
        } catch (JOSEException e) {
            throw new AppException(ErrorCode.UNCATEGORIZED_EXCEPTION);
        }
    }

    private void saveInvalidatedToken(String acId, String rfId, Date expirationTime) {
        ZoneId zoneId = ZoneId.systemDefault();
        LocalDateTime localDateTime = ZonedDateTime.ofInstant(expirationTime.toInstant(), zoneId).toLocalDateTime();
        invalidatedTokenRepository.save(InvalidatedToken.builder()
                .acId(acId)
                .rfId(rfId)
                .expirationTime(localDateTime)
                .build());
    }
}
