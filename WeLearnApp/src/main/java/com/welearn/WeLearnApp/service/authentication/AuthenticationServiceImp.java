package com.welearn.WeLearnApp.service.authentication;

import com.nimbusds.jwt.SignedJWT;
import com.welearn.WeLearnApp.dto.request.authentication.AuthenticationRequest;
import com.welearn.WeLearnApp.dto.request.authentication.IntrospectRequest;
import com.welearn.WeLearnApp.dto.request.authentication.LogoutRequest;
import com.welearn.WeLearnApp.dto.response.AuthenticationResponse;
import com.welearn.WeLearnApp.dto.response.IntrospectResponse;
import com.welearn.WeLearnApp.entity.User;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class AuthenticationServiceImp implements AuthenticationService{

    PasswordEncoder passwordEncoder;

    @Override
    public AuthenticationResponse authenticate(AuthenticationRequest request) {
        return null;
    }

    @Override
    public AuthenticationResponse refreshToken(String refreshToken) {
        return null;
    }

    @Override
    public IntrospectResponse introspect(IntrospectRequest request) {
        return null;
    }

    @Override
    public void logout(LogoutRequest request) {

    }

    private SignedJWT verifyToken(String token) {
        return null;
    }

    private AuthenticationResponse buildAuthenticationResponse() {
        return null;
    }

    private String generateToken(User user, String signerKey, long duration, String id, String otherId) {
        return null;
    }
}
