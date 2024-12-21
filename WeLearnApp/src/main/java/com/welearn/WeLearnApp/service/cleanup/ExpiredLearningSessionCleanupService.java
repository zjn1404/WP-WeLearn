package com.welearn.WeLearnApp.service.cleanup;

import com.welearn.WeLearnApp.repository.LearningSessionRepository;
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
public class ExpiredLearningSessionCleanupService {

    LearningSessionRepository learningSessionRepository;

    @Scheduled(fixedDelay = 3600000) // ms
    @Transactional
    void deleteExpiredLearningSessions() {
        learningSessionRepository.deleteExpiredLearningSessions(LocalDateTime.now());
    }

}
