package com.welearn.WeLearnApp.service.verificationcode;

import com.welearn.WeLearnApp.dto.request.mail.MailRequest;
import com.welearn.WeLearnApp.dto.request.mail.mailparam.Recipient;
import com.welearn.WeLearnApp.dto.request.mail.mailparam.Sender;
import com.welearn.WeLearnApp.dto.request.verificationcode.VerifyCodeRequest;
import com.welearn.WeLearnApp.dto.response.MailResponse;
import com.welearn.WeLearnApp.dto.response.VerifyCodeResponse;
import com.welearn.WeLearnApp.entity.User;
import com.welearn.WeLearnApp.entity.VerificationCode;
import com.welearn.WeLearnApp.exception.AppException;
import com.welearn.WeLearnApp.exception.ErrorCode;
import com.welearn.WeLearnApp.repository.UserRepository;
import com.welearn.WeLearnApp.repository.VerificationCodeRepository;
import com.welearn.WeLearnApp.repository.httpclient.MailClient;
import com.welearn.WeLearnApp.utils.EmailBuilder;
import jakarta.transaction.Transactional;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.experimental.NonFinal;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.List;

@Service
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class VerificationCodeServiceImpl implements VerificationCodeService {

    @NonFinal
    @Value("${mail.sender.email}")
    String SENDER_EMAIL;

    @NonFinal
    @Value("${mail.sender.name}")
    String SENDER_NAME;

    @NonFinal
    @Value("${mail.api-key}")
    String API_KEY;

    MailClient mailClient;

    EmailBuilder emailBuilder;

    VerificationCodeRepository verificationCodeRepository;
    UserRepository userRepository;

    @Transactional
    @Override
    public MailResponse sendVerificationCode(String userId) {
        verificationCodeRepository.deleteByUserId(userId);

        User user = userRepository.findById(userId)
                .orElseThrow(() -> new AppException(ErrorCode.USER_NOT_FOUND));

        String emailContent = emailBuilder.buildEmail(user);

        return mailClient.sendEmail(API_KEY, buildMailRequest(user, emailContent));
    }

    @Override
    public boolean isVerified(String userId) {
        return !verificationCodeRepository.existsByUserId(userId);
    }

    @Override
    public VerifyCodeResponse verifyCode(VerifyCodeRequest request) {
        VerificationCode verificationCode = verificationCodeRepository
                .findByIdAndUser(request.getCode(), request.getUserId())
                .orElseThrow(() -> new AppException(ErrorCode.INVALID_VERIFICATION_CODE));

        if (verificationCode.getExpirationTime().isBefore(LocalDateTime.now())) {
            throw new AppException(ErrorCode.INVALID_VERIFICATION_CODE);
        }

        verificationCodeRepository.delete(verificationCode);

        return VerifyCodeResponse.builder()
                .isValid(true)
                .build();
    }

    private MailRequest buildMailRequest(User user, String emailContent) {
        return MailRequest.builder()
                .sender(Sender.builder()
                        .email(SENDER_EMAIL)
                        .name(SENDER_NAME)
                        .build())
                .to(List.of(Recipient.builder()
                        .email(user.getEmail())
                        .name(user.getUsername())
                        .build()))
                .subject("Verification Code")
                .htmlContent(emailContent)
                .build();
    }
}
