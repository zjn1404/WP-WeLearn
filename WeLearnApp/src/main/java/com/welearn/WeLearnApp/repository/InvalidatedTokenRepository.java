package com.welearn.WeLearnApp.repository;

import com.welearn.WeLearnApp.entity.InvalidatedToken;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

@Repository
public interface InvalidatedTokenRepository extends JpaRepository<InvalidatedToken, String> {

    @Query(value = "SELECT CASE WHEN COUNT(i) > 0 THEN true ELSE false END FROM invalidated_token i WHERE i.ac_id = :id OR i.rf_id = :id",
            nativeQuery = true)
    boolean existsByTokenId(String id);

}
