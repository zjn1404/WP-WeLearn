package com.welearn.WeLearnApp.repository;

import com.welearn.WeLearnApp.entity.Tutor;
import org.springframework.data.jpa.repository.JpaRepository;

public interface TutorRepository extends JpaRepository<Tutor, String> {
}
