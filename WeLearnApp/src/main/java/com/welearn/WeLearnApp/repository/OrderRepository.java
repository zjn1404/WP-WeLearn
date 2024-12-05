package com.welearn.WeLearnApp.repository;

import com.welearn.WeLearnApp.entity.LearningSession;
import com.welearn.WeLearnApp.entity.Order;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

@Repository
public interface OrderRepository extends JpaRepository<Order, String> {

    @Query("""
    SELECT CASE WHEN COUNT(o) > 0 THEN TRUE ELSE FALSE END
    FROM Order o
    WHERE o.orderDetail.learningSession.id = :learningSessionId
    """)
    boolean existsByLearningSession(String learningSessionId);

}
