package com.welearn.WeLearnApp.service.user;

import com.nimbusds.jose.*;
import com.nimbusds.jose.crypto.MACSigner;
import com.nimbusds.jose.crypto.MACVerifier;
import com.nimbusds.jwt.JWTClaimsSet;
import com.nimbusds.jwt.SignedJWT;
import com.welearn.WeLearnApp.dto.request.user.UserCreationRequest;
import com.welearn.WeLearnApp.dto.request.user.UserUpdateRequest;
import com.welearn.WeLearnApp.dto.request.userprofile.UpdateUnverifiedEmailRequest;
import com.welearn.WeLearnApp.dto.response.UpdateUnverifiedEmailInvitationResponse;
import com.welearn.WeLearnApp.dto.response.UserResponse;
import com.welearn.WeLearnApp.entity.InvalidatedToken;
import com.welearn.WeLearnApp.entity.Location;
import com.welearn.WeLearnApp.entity.Role;
import com.welearn.WeLearnApp.entity.User;
import com.welearn.WeLearnApp.exception.AppException;
import com.welearn.WeLearnApp.exception.ErrorCode;
import com.welearn.WeLearnApp.mapper.role.RoleMapper;
import com.welearn.WeLearnApp.mapper.user.UserMapper;
import com.welearn.WeLearnApp.repository.InvalidatedTokenRepository;
import com.welearn.WeLearnApp.repository.RoleRepository;
import com.welearn.WeLearnApp.repository.UserRepository;
import com.welearn.WeLearnApp.service.location.LocationService;
import com.welearn.WeLearnApp.service.userprofile.UserProfileService;
import com.welearn.WeLearnApp.service.verificationcode.VerificationCodeService;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.experimental.NonFinal;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Service;

import java.text.ParseException;
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
public class UserServiceImp implements UserService{

    @NonFinal
    @Value("${jwt.signer-key.update-unverified-email}")
    String UPDATE_UNVERIFIED_EMAIL_SIGNER_KEY;

    @NonFinal
    @Value("${jwt.duration.update-unverified-email}")
    long UPDATE_UNVERIFIED_EMAIL_DURATION;

    UserRepository userRepository;
    RoleRepository roleRepository;
    InvalidatedTokenRepository invalidatedTokenRepository;

    UserProfileService userProfileService;
    LocationService locationService;
    VerificationCodeService verificationCodeService;

    UserMapper userMapper;
    RoleMapper roleMapper;

    @Override
    public UserResponse createUser(UserCreationRequest request) {

        if (userRepository.existsByUsername(request.getUsername())) {
            throw new AppException(ErrorCode.USER_EXISTED);
        }

        User user = userMapper.toUser(request);
        Role role = roleRepository.findById(request.getRole())
                .orElseThrow(() -> new AppException(ErrorCode.ROLE_NOT_FOUND));
        user.setRole(role);

        userRepository.save(user);

        Location location = locationService.internalCreateLocation(request);

        userProfileService.internalCreateProfile(user.getId(), location, request);

        verificationCodeService.sendVerificationCode(user.getId());

        return buildUserResponse(user);
    }

    @Override
    public UserResponse getUserById(String id) {
        User user = userRepository.findById(id)
                .orElseThrow(() -> new AppException(ErrorCode.USER_NOT_FOUND));

        return buildUserResponse(user);
    }

    @Override
    public UserResponse getMyAccount() {
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();

        User user = userRepository.findByUsername(authentication.getName())
                .orElseThrow(() -> new AppException(ErrorCode.USER_NOT_FOUND));

        return buildUserResponse(user);
    }

    @Override
    public UserResponse updateUser(String id, UserUpdateRequest request) {
        User user = userRepository.findById(id)
                .orElseThrow(() -> new AppException(ErrorCode.USER_NOT_FOUND));

        userMapper.updateUser(user, request);
        userRepository.saveAndFlush(user);

        return buildUserResponse(user);
    }

    @Override
    public UpdateUnverifiedEmailInvitationResponse acceptUpdateUnverifiedEmailInvitation(String userId) {
        return UpdateUnverifiedEmailInvitationResponse.builder()
                .token(generateUpdateUnverifiedEmailToken(userId))
                .build();
    }

    @Override
    public void updateUnverifiedEmail(UpdateUnverifiedEmailRequest request) throws JOSEException, ParseException {
            SignedJWT signedJWT = SignedJWT.parse(request.getToken());
            JWSVerifier verifier = new MACVerifier(UPDATE_UNVERIFIED_EMAIL_SIGNER_KEY);

            if (!signedJWT.verify(verifier)) {
                throw new AppException(ErrorCode.INVALID_TOKEN);
            }

            String tokenId = signedJWT.getJWTClaimsSet().getStringClaim("id");
            log.info("Token id: {}", tokenId);
            if (invalidatedTokenRepository.existsByTokenId(tokenId)) {
                throw new AppException(ErrorCode.INVALID_TOKEN);
            }

            Date expirationTime = signedJWT.getJWTClaimsSet().getExpirationTime();
            if (expirationTime.before(new Date())) {
                throw new AppException(ErrorCode.INVALID_TOKEN);
            }

            String userId = signedJWT.getJWTClaimsSet().getSubject();
            User user = userRepository.findById(userId)
                    .orElseThrow(() -> new AppException(ErrorCode.USER_NOT_FOUND));

            user.setEmail(request.getEmail());

            userRepository.saveAndFlush(user);

            ZoneId zoneId = ZoneId.systemDefault();
            LocalDateTime localDateTime = ZonedDateTime.ofInstant(expirationTime.toInstant(), zoneId).toLocalDateTime();

            invalidatedTokenRepository.save(InvalidatedToken.builder()
                            .acId(tokenId)
                            .rfId(tokenId)
                            .expirationTime(localDateTime)
                    .build());

            verificationCodeService.sendVerificationCode(user.getId());
    }

    @Override
    public void deleteUser(String id) {
        User user = userRepository.findById(id)
                .orElseThrow(() -> new AppException(ErrorCode.USER_NOT_FOUND));

        userRepository.delete(user);
    }

    private UserResponse buildUserResponse(User user) {
        UserResponse userResponse = userMapper.toUserResponse(user);
        userResponse.setRole(roleMapper.toRoleResponse(user.getRole()));

        return userResponse;
    }

    private String generateUpdateUnverifiedEmailToken(String userId) {
        JWSHeader header = new JWSHeader.Builder(JWSAlgorithm.HS512).build();

        JWTClaimsSet claimsSet = new JWTClaimsSet.Builder()
                .subject(userId)
                .expirationTime(new Date(Instant.now().plus(UPDATE_UNVERIFIED_EMAIL_DURATION, ChronoUnit.SECONDS).toEpochMilli()))
                .claim("id", UUID.randomUUID().toString())
                .build();

        Payload payload = new Payload(claimsSet.toJSONObject());

        JWSObject jwsObject = new JWSObject(header, payload);

        try {
            jwsObject.sign(new MACSigner(UPDATE_UNVERIFIED_EMAIL_SIGNER_KEY));
            return jwsObject.serialize();
        } catch (JOSEException e) {
            throw new AppException(ErrorCode.UNCATEGORIZED_EXCEPTION);
        }
    }
}
