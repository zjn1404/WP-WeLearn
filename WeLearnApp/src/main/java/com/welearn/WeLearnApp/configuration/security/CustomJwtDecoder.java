package com.welearn.WeLearnApp.configuration.security;

import com.welearn.WeLearnApp.dto.request.authentication.IntrospectRequest;
import com.welearn.WeLearnApp.dto.response.IntrospectResponse;
import com.welearn.WeLearnApp.exception.ErrorCode;
import com.welearn.WeLearnApp.service.authentication.AuthenticationService;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.experimental.NonFinal;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.security.oauth2.jose.jws.MacAlgorithm;
import org.springframework.security.oauth2.jwt.Jwt;
import org.springframework.security.oauth2.jwt.JwtDecoder;
import org.springframework.security.oauth2.jwt.JwtException;
import org.springframework.security.oauth2.jwt.NimbusJwtDecoder;
import org.springframework.stereotype.Component;

import javax.crypto.spec.SecretKeySpec;
import java.util.Objects;

@Slf4j
@Component
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class CustomJwtDecoder implements JwtDecoder {

    @NonFinal
    @Value("${jwt.signer-key.access}")
    String SIGNER_KEY;

    @NonFinal
    NimbusJwtDecoder nimbusJwtDecoder = null;

    AuthenticationService authenticationService;

    @Override
    public Jwt decode(String token) throws JwtException {
        IntrospectResponse introspectResponse = authenticationService.introspect(
                IntrospectRequest.builder().token(token).build());

        if (!introspectResponse.isValid()) {
            throw new JwtException(ErrorCode.INVALID_TOKEN.getMessage());
        }

        if (Objects.isNull(nimbusJwtDecoder)) {
            SecretKeySpec secretKeySpec = new SecretKeySpec(SIGNER_KEY.getBytes(), "SH512");

            nimbusJwtDecoder = NimbusJwtDecoder.withSecretKey(secretKeySpec)
                    .macAlgorithm(MacAlgorithm.HS512)
                    .build();
        }

        return nimbusJwtDecoder.decode(token);
    }
}
