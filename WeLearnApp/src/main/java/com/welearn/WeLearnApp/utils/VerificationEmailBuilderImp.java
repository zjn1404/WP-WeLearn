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
import java.util.Random;
import java.util.UUID;

@Component
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class VerificationEmailBuilderImp implements EmailBuilder{

    @NonFinal
    @Value("${verification-code.duration}")
    long VERIFICATION_CODE_DURATION;

    @NonFinal
    @Value("${verification-code.length}")
    int VERIFICATION_CODE_LENGTH;

    @NonFinal
    static String CHARACTERS = "qwe12rTYuiop3456asDf8ghjkl90zxcvtydbnmQWERUIOPASFGHJKLZXCVBNM";

    @NonFinal
    static Random RAND = new Random();

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

    private VerificationCode generateVerificationCode(User user) {
        String code = generateOTP();
        VerificationCode verificationCode = VerificationCode.builder()
                .code(code)
                .user(user)
                .expirationTime(LocalDateTime.now().plusMinutes(VERIFICATION_CODE_DURATION))
                .build();

        return verificationCodeRepository.save(verificationCode);
    }

    private String generateOTP() {
        StringBuilder otpBuilder = new StringBuilder();
        for (int i = 0; i < VERIFICATION_CODE_LENGTH; i++) {
            int index = RAND.nextInt(CHARACTERS.length());
            otpBuilder.append(CHARACTERS.charAt(index));
        }

        return otpBuilder.toString();
    }
}
