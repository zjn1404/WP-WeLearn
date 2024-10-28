package com.welearn.WeLearnApp.service.verificationcode;

import com.welearn.WeLearnApp.dto.request.verificationcode.VerifyCodeRequest;
import com.welearn.WeLearnApp.dto.response.MailResponse;
import com.welearn.WeLearnApp.dto.response.VerifyCodeResponse;

public interface VerificationCodeService {
    MailResponse sendVerificationCode(String userId);

    boolean isVerified(String userId);

    VerifyCodeResponse verifyCode(VerifyCodeRequest request);
}
