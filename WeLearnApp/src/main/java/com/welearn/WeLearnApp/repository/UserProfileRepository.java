package com.welearn.WeLearnApp.repository;

import com.welearn.WeLearnApp.entity.UserProfile;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.util.List;
import java.util.Optional;

@Repository
public interface UserProfileRepository extends JpaRepository<UserProfile, String> {

    Page<UserProfile> findAllByFirstNameContainingIgnoreCaseOrLastNameContainingIgnoreCase(String firstName, String lastName, Pageable pageable);

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
    LEFT JOIN LearningSession l ON u.id = l.tutor.id
    WHERE (:city IS NULL OR u.location.city = :city)
        AND (:district IS NULL OR u.location.district = :district)
        AND (:street IS NULL OR u.location.street = :street)
        AND (:grade IS NULL OR :grade = l.grade.id)
        AND (:subject IS NULL OR :subject = l.subject.name)
        AND (:learningMethod IS NULL OR :learningMethod = l.learningMethod.name)
        AND (:tuition IS NULL OR :tuition = l.tuition)
    """)
    Page<UserProfile> findAllByLocationAndGradeAndSubject(
            @Param("city") String city,
            @Param("district") String district,
            @Param("street") String street,
            @Param("grade") Integer grade,
            @Param("subject") String subject,
            @Param("learningMethod") String learningMethod,
            @Param("tuition") Double tuition,
            Pageable pageable
    );
}
