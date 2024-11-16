package com.welearn.WeLearnApp.repository;

import com.welearn.WeLearnApp.entity.LearningSession;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.time.LocalDateTime;

@Repository
public interface LearningSessionRepository extends JpaRepository<LearningSession, String> {

    @Modifying
    @Query(value = "DELETE FROM learning_session WHERE DATE_ADD(start_time, INTERVAL duration MINUTE) < :now", nativeQuery = true)
    void deleteExpiredLearningSessions(@Param("now") LocalDateTime now);

    boolean existsByStartTime(LocalDateTime startTime);
}
