package com.welearn.WeLearnApp.service.authentication;

import com.welearn.WeLearnApp.dto.request.authentication.AuthenticationRequest;
import com.welearn.WeLearnApp.dto.request.authentication.IntrospectRequest;
import com.welearn.WeLearnApp.dto.request.authentication.LogoutRequest;
import com.welearn.WeLearnApp.dto.response.AuthenticationResponse;
import com.welearn.WeLearnApp.dto.response.IntrospectResponse;

public interface AuthenticationService {
    AuthenticationResponse authenticate(AuthenticationRequest request);

    AuthenticationResponse refreshToken(String refreshToken);

    IntrospectResponse introspect(IntrospectRequest request);

    void logout(LogoutRequest request);
}
