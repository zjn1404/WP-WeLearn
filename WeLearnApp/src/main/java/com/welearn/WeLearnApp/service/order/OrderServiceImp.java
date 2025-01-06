package com.welearn.WeLearnApp.service.order;

import com.welearn.WeLearnApp.dto.request.order.OrderCreationRequest;
import com.welearn.WeLearnApp.dto.response.LearningSessionResponse;
import com.welearn.WeLearnApp.dto.response.OrderResponse;
import com.welearn.WeLearnApp.dto.response.PageResponse;
import com.welearn.WeLearnApp.entity.LearningSession;
import com.welearn.WeLearnApp.entity.Order;
import com.welearn.WeLearnApp.entity.OrderDetail;
import com.welearn.WeLearnApp.entity.UserProfile;
import com.welearn.WeLearnApp.exception.AppException;
import com.welearn.WeLearnApp.exception.ErrorCode;
import com.welearn.WeLearnApp.mapper.learningsession.LearningSessionMapper;
import com.welearn.WeLearnApp.mapper.location.LocationMapper;
import com.welearn.WeLearnApp.mapper.order.OrderMapper;
import com.welearn.WeLearnApp.mapper.userprofile.UserProfileMapper;
import com.welearn.WeLearnApp.repository.LearningSessionRepository;
import com.welearn.WeLearnApp.repository.OrderRepository;
import com.welearn.WeLearnApp.repository.UserProfileRepository;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.extern.slf4j.Slf4j;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.List;
import java.util.UUID;

@Slf4j
@Service
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class OrderServiceImp implements OrderService {

    OrderRepository orderRepository;

    LearningSessionRepository learningSessionRepository;

    UserProfileRepository userProfileRepository;

    OrderMapper orderMapper;

    LearningSessionMapper learningSessionMapper;

    UserProfileMapper userProfileMapper;

    LocationMapper locationMapper;

    @Override
    public OrderResponse createOrder(OrderCreationRequest request) {
        if (orderRepository.existsByLearningSession(request.getLearningSessionId())) {
            throw new AppException(ErrorCode.ORDER_ALREADY_EXISTS);
        }

        UserProfile student = userProfileRepository.findById(request.getUserId())
                .orElseThrow(() -> new AppException(ErrorCode.USER_NOT_FOUND));
        LearningSession learningSession = learningSessionRepository.findById(request.getLearningSessionId())
                .orElseThrow(() -> new AppException(ErrorCode.LEARNING_SESSION_NOT_FOUND));

        if (learningSession.getTutor().getId().equals(student.getId())) {
            throw new AppException(ErrorCode.TUTOR_CANNOT_ORDER);
        }

        Order order = Order.builder()
                .id(UUID.randomUUID().toString())
                .student(student)
                .tutor(learningSession.getTutor())
                .orderTime(LocalDateTime.now())
                .build();

        order.setOrderDetail(OrderDetail.builder()
                .orderId(order.getId())
                .order(order)
                .learningSession(learningSession)
                .build());

        orderRepository.save(order);

        return orderMapper.toOrderResponse(order);
    }

    @Override
    public PageResponse<OrderResponse> getMyOrders(int page, int size) {
        Pageable pageable = PageRequest.of(page - 1, size);

        Page<Order> orders = orderRepository.findAllByLearningSessionEndTimeAfter(LocalDateTime.now(), pageable);

        List<OrderResponse> orderResponses = orders.stream().map(this::buildOrderResponse).toList();

        return PageResponse.<OrderResponse>builder()
                .currentPage(page)
                .totalPage(orders.getTotalPages())
                .totalElement(orders.getTotalElements())
                .elementPerPage(size)
                .data(orderResponses)
                .build();
    }

    @Override
    public PageResponse<LearningSessionResponse> getMyComingLearningSessions(int page, int size) {
        Pageable pageable = PageRequest.of(page - 1, size);

        Page<LearningSession> learningSessions = learningSessionRepository.findAll(pageable);

        List<LearningSessionResponse> learningSessionResponses = learningSessions.stream()
                .map(learningSessionMapper::toLearningSessionResponse)
                .toList();

        return PageResponse.<LearningSessionResponse>builder()
                .currentPage(page)
                .totalPage(learningSessions.getTotalPages())
                .totalElement(learningSessions.getTotalElements())
                .elementPerPage(size)
                .data(learningSessionResponses)
                .build();
    }

    @Override
    public void cancelOrder(String orderId) {
        Order order = orderRepository.findById(orderId)
                .orElseThrow(() -> new AppException(ErrorCode.ORDER_NOT_FOUND));

        orderRepository.delete(order);
    }

    private OrderResponse buildOrderResponse(Order order) {
        OrderResponse response = orderMapper.toOrderResponse(order);
        response.setTutor(userProfileMapper.toUserProfileResponse(order.getTutor()));
        response.getTutor().setLocation(locationMapper.toLocationResponse(order.getTutor().getLocation()));
        response.getOrderDetail().getLearningSession().setTutor(response.getTutor());

        return response;
    }
}
