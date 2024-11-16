package com.welearn.WeLearnApp.service.cleanup;

import com.welearn.WeLearnApp.repository.InvalidatedTokenRepository;
import jakarta.transaction.Transactional;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;

@Service
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class ExpiredTokenCleanupService {

    InvalidatedTokenRepository invalidatedTokenRepository;

    @Scheduled(fixedDelay = 86400000) // ms
    @Transactional
    public void deleteExpiredTokens() {
        invalidatedTokenRepository.deleteAllByExpiryTimeBefore(LocalDateTime.now());
    }

}
