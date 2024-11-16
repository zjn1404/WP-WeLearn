package com.welearn.WeLearnApp.service.order;

import com.welearn.WeLearnApp.dto.request.order.OrderCreationRequest;
import com.welearn.WeLearnApp.dto.response.LearningSessionResponse;
import com.welearn.WeLearnApp.dto.response.OrderResponse;
import com.welearn.WeLearnApp.dto.response.PageResponse;

public interface OrderService {
    OrderResponse createOrder(OrderCreationRequest request);

    PageResponse<OrderResponse> getMyOrders(int page, int size);

    PageResponse<LearningSessionResponse> getMyComingLearningSessions(int page, int size);

    void cancelOrder(String orderId);
}
