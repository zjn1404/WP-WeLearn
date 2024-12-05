package com.welearn.WeLearnApp.repository;

import com.welearn.WeLearnApp.entity.InvalidatedToken;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.time.LocalDateTime;

@Repository
public interface InvalidatedTokenRepository extends JpaRepository<InvalidatedToken, String> {

    @Query("SELECT CASE WHEN COUNT(i) > 0 THEN true ELSE false END FROM InvalidatedToken i WHERE i.acId = :id OR i.rfId = :id")
    boolean existsByTokenId(String id);

    @Modifying
    @Query("DELETE FROM InvalidatedToken i WHERE i.expirationTime < :date")
    void deleteAllByExpiryTimeBefore(LocalDateTime date);

}
