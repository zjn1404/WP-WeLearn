package com.welearn.WeLearnApp.utils;

import com.welearn.WeLearnApp.entity.User;
import com.welearn.WeLearnApp.entity.VerificationCode;
import com.welearn.WeLearnApp.repository.VerificationCodeRepository;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.experimental.NonFinal;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Component;

import java.time.LocalDateTime;
import java.util.UUID;

@Component
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class VerificationEmailBuilderImp implements EmailBuilder{

    @NonFinal
    @Value("${verification-code.duration}")
    long VERIFICATION_CODE_DURATION;

    VerificationCodeRepository verificationCodeRepository;

    @Override
    public String buildEmail(User user) {

        VerificationCode verificationCode = generateVerificationCode(user);

        String mailContent = "<p>Dear " + user.getUsername() + ",</p>";
        mailContent += "<p>Your verification code:</p>";
        mailContent += "<p><b>" + verificationCode.getCode() + "</b></p>";
        mailContent += "<p> Thank you! <br> The WeLearn Team <p>";

        return mailContent;
    }

    VerificationCode generateVerificationCode(User user) {
        String code = UUID.randomUUID().toString();
        VerificationCode verificationCode = VerificationCode.builder()
                .code(code)
                .user(user)
                .expirationTime(LocalDateTime.now().plusMinutes(VERIFICATION_CODE_DURATION))
                .build();

        return verificationCodeRepository.save(verificationCode);
    }
}
