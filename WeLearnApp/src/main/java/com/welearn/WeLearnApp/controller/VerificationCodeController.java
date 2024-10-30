package com.welearn.WeLearnApp.controller;

import com.welearn.WeLearnApp.dto.request.verificationcode.VerifyCodeRequest;
import com.welearn.WeLearnApp.dto.response.ApiResponse;
import com.welearn.WeLearnApp.dto.response.MailResponse;
import com.welearn.WeLearnApp.dto.response.VerifyCodeResponse;
import com.welearn.WeLearnApp.service.verificationcode.VerificationCodeService;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/verification-code")
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class VerificationCodeController {
    VerificationCodeService verificationCodeService;

    @PostMapping("/verify")
    public ApiResponse<VerifyCodeResponse> verifyCode(@RequestBody VerifyCodeRequest request) {
        return ApiResponse.<VerifyCodeResponse>builder()
                .data(verificationCodeService.verifyCode(request))
                .build();
    }

    @PostMapping("/{userId}")
    public ApiResponse<MailResponse> sendVerificationCode(@PathVariable("userId") String userId) {
        return ApiResponse.<MailResponse>builder()
                .data(verificationCodeService.sendVerificationCode(userId))
                .build();
    }
}
