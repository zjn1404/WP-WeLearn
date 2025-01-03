package com.welearn.WeLearnApp.repository;

import com.welearn.WeLearnApp.entity.Order;
import com.welearn.WeLearnApp.entity.OrderDetail;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;

import java.time.LocalDateTime;
import java.util.List;

@Repository
public interface OrderRepository extends JpaRepository<Order, String> {

    @Query("""
    SELECT CASE WHEN COUNT(o) > 0 THEN TRUE ELSE FALSE END
    FROM Order o
    WHERE o.orderDetail.learningSession.id = :learningSessionId
    """)
    boolean existsByLearningSession(String learningSessionId);

    @Query(value = """
    SELECT o
    FROM Order o
    WHERE FUNCTION('ADDTIME', o.orderDetail.learningSession.startTime, 
                   FUNCTION('SEC_TO_TIME', o.orderDetail.learningSession.duration * 60)) >= :time
    """)
    Page<Order> findAllByLearningSessionEndTimeAfter(
            @Param("time") LocalDateTime time,
            Pageable pageable
    );

    @Query("""
    SELECT o
    FROM Order o
    WHERE FUNCTION('ADDTIME', :time, FUNCTION('SEC_TO_TIME', 30 * 60)) >= o.orderDetail.learningSession.startTime
    """)
    List<Order> findAllUpcomingLearningSessions(@Param("time") LocalDateTime time);

    Order findByOrderDetail(OrderDetail orderDetail);
}
