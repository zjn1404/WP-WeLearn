package com.welearn.WeLearnApp.controller;

import com.welearn.WeLearnApp.dto.request.authentication.AuthenticationRequest;
import com.welearn.WeLearnApp.dto.request.authentication.IntrospectRequest;
import com.welearn.WeLearnApp.dto.request.authentication.LogoutRequest;
import com.welearn.WeLearnApp.dto.request.authentication.RefreshTokenRequest;
import com.welearn.WeLearnApp.dto.response.ApiResponse;
import com.welearn.WeLearnApp.dto.response.AuthenticationResponse;
import com.welearn.WeLearnApp.dto.response.IntrospectResponse;
import com.welearn.WeLearnApp.service.authentication.AuthenticationService;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.experimental.NonFinal;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/auth")
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class AuthenticationController {

    @Value("${message.controller.authentication.logout-success}")
    @NonFinal
    String LOGOUT_SUCESS_MSG;

    AuthenticationService authenticationService;

    @PostMapping("/introspect")
    public ApiResponse<IntrospectResponse> introspect(@RequestBody IntrospectRequest request) {
        return ApiResponse.<IntrospectResponse>builder()
                .data(authenticationService.introspect(request))
                .build();
    }

    @PostMapping("/authenticate")
    public ApiResponse<AuthenticationResponse> authenticate(@RequestBody AuthenticationRequest request) {
        return ApiResponse.<AuthenticationResponse>builder()
                .data(authenticationService.authenticate(request))
                .build();
    }

    @PostMapping("/logout")
    public ApiResponse<Void> logout(@RequestBody LogoutRequest request) {
        authenticationService.logout(request);

        return ApiResponse.<Void>builder()
                .message(LOGOUT_SUCESS_MSG)
                .build();
    }

    @PostMapping("/refresh")
    public ApiResponse<AuthenticationResponse> refresh(@RequestBody RefreshTokenRequest request) {
        return ApiResponse.<AuthenticationResponse>builder()
                .data(authenticationService.refreshToken(request.getRefreshToken()))
                .build();
    }
}
