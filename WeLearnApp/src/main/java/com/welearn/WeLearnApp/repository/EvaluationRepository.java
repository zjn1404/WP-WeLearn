package com.welearn.WeLearnApp.repository;

import com.welearn.WeLearnApp.entity.Evaluation;
import com.welearn.WeLearnApp.entity.UserProfile;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface EvaluationRepository extends JpaRepository<Evaluation, String> {
    Page<Evaluation> findAllByTutor(UserProfile tutor, Pageable pageable);
}
