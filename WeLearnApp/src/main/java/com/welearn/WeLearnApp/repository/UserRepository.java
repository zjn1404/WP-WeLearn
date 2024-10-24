package com.welearn.WeLearnApp.repository;

import com.welearn.WeLearnApp.entity.User;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface UserRepository extends JpaRepository<User, String> {

    @Query(value = "SELECT CASE WHEN COUNT(u) > 0 THEN true ELSE false END FROM user u WHERE u.username = :username",
            nativeQuery = true)
    boolean existsByUsername(@Param("username") String username);

    @Query(value = "SELECT * FROM user u WHERE u.username = :username", nativeQuery = true)
    Optional<User> findByUsername(@Param("username") String username);
}
