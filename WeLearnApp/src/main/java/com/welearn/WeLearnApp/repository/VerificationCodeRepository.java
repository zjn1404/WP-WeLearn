package com.welearn.WeLearnApp.repository;

import com.welearn.WeLearnApp.entity.VerificationCode;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface VerificationCodeRepository extends JpaRepository<VerificationCode, String> {
    @Query("SELECT v FROM VerificationCode v WHERE v.code = :id AND v.user.id = :userId")
    Optional<VerificationCode> findByIdAndUser(String id, String userId);

    @Query("SELECT CASE WHEN COUNT(v) > 0 THEN TRUE ELSE FALSE END FROM VerificationCode v WHERE v.user.id = :userId")
    boolean existsByUserId(String userId);

    @Query("DELETE FROM VerificationCode v WHERE v.user.id = :userId")
    void deleteByUserId(String userId);
}
