package com.welearn.WeLearnApp.repository;

import com.welearn.WeLearnApp.entity.UserProfile;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public interface UserProfileRepository extends JpaRepository<UserProfile, String> {

    @Query("""
    SELECT u
    FROM UserProfile u
    JOIN User us ON u.id = us.id
    WHERE us.role.name = 'TUTOR'
    AND LOWER(CONCAT(u.firstName, ' ', u.lastName)) LIKE LOWER(CONCAT('%', :keyword, '%'))
    """)
    Page<UserProfile> findAllByNameContainingIgnoreCase(@Param("keyword") String keyword, Pageable pageable);

    @Query("""
    SELECT u FROM UserProfile u
    JOIN Order o ON u.id = o.tutor.id
    WHERE o.orderDetail.learningSession.grade.id = :grade 
    AND o.orderDetail.learningSession.subject.name = :subject
    GROUP BY u.id
    ORDER BY COUNT(o.id) DESC
    LIMIT 3
    """)
    List<UserProfile> findTopThreeTutorsByGradeAndSubject(int grade, String subject);

    @Query("""
    SELECT u FROM UserProfile u
    JOIN User us ON u.id = us.id
    LEFT JOIN LearningSession l ON u.id = l.tutor.id
    LEFT JOIN Location loc ON u.location.id = loc.id
    WHERE us.role.name = :role
      AND (:city IS NULL OR loc IS NULL OR :city = loc.city)
      AND (:district IS NULL OR loc IS NULL OR :district = loc.district)
      AND (:street IS NULL OR loc IS NULL OR :street = loc.street)
      AND (:grade IS NULL OR l IS NULL OR :grade = l.grade.id)
      AND (:subject IS NULL OR l IS NULL OR :subject = l.subject.name)
      AND (:learningMethod IS NULL OR l IS NULL OR :learningMethod = l.learningMethod.name)
      AND (:tuition IS NULL OR l IS NULL OR :tuition = l.tuition)
    """)
    Page<UserProfile> findAllByLocationAndGradeAndSubject(
            @Param("role") String role,
            @Param("city") String city,
            @Param("district") String district,
            @Param("street") String street,
            @Param("grade") Integer grade,
            @Param("subject") String subject,
            @Param("learningMethod") String learningMethod,
            @Param("tuition") Double tuition,
            Pageable pageable
    );

    @Query("SELECT u FROM UserProfile u JOIN User us ON u.id = us.id WHERE us.role.name = :role")
    Page<UserProfile> findAllByRole(String role, Pageable pageable);
}
