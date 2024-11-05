package com.welearn.WeLearnApp.repository;

import com.welearn.WeLearnApp.entity.LearningMethod;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface LearningMethodRepository extends JpaRepository<LearningMethod, String> {
}
