package com.welearn.WeLearnApp.repository;

import com.welearn.WeLearnApp.entity.LearningSession;
import com.welearn.WeLearnApp.entity.OrderDetail;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface OrderDetailRepository extends JpaRepository<OrderDetail, String> {
    OrderDetail findByLearningSession(LearningSession learningSession);
}
