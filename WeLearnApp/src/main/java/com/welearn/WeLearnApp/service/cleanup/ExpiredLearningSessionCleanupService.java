package com.welearn.WeLearnApp.service.cleanup;

import com.welearn.WeLearnApp.entity.LearningSession;
import com.welearn.WeLearnApp.entity.Order;
import com.welearn.WeLearnApp.entity.OrderDetail;
import com.welearn.WeLearnApp.repository.LearningSessionRepository;
import com.welearn.WeLearnApp.repository.OrderDetailRepository;
import com.welearn.WeLearnApp.repository.OrderRepository;
import jakarta.transaction.Transactional;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.extern.slf4j.Slf4j;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Slf4j
@Service
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class ExpiredLearningSessionCleanupService {

    LearningSessionRepository learningSessionRepository;
    OrderDetailRepository orderDetailRepository;
    OrderRepository orderRepository;

    @Scheduled(initialDelay = 31536000000L, fixedDelay = 31536000000L) // ms
    @Transactional
    void deleteExpiredLearningSessions() {
        List<LearningSession> expiredLearningSessions = learningSessionRepository.findAllExpiredLearningSessions(LocalDateTime.now());

        log.info("Deleting {} expired learning sessions", expiredLearningSessions.size());

        List<OrderDetail> orderDetails = new ArrayList<>();
        List<Order> orders = new ArrayList<>();
        for (LearningSession learningSession : expiredLearningSessions) {
            OrderDetail orderDetail = orderDetailRepository.findByLearningSession(learningSession);
            orderDetails.add(orderDetail);
            orders.add(orderRepository.findByOrderDetail(orderDetail));
        }

        orderDetailRepository.deleteAll(orderDetails);

        orderRepository.deleteAll(orders);

        learningSessionRepository.deleteAll(expiredLearningSessions);
    }

}
