package com.welearn.WeLearnApp.controller;

import com.welearn.WeLearnApp.dto.request.order.OrderCreationRequest;
import com.welearn.WeLearnApp.dto.response.ApiResponse;
import com.welearn.WeLearnApp.dto.response.LearningSessionResponse;
import com.welearn.WeLearnApp.dto.response.OrderResponse;
import com.welearn.WeLearnApp.dto.response.PageResponse;
import com.welearn.WeLearnApp.service.order.OrderService;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.experimental.NonFinal;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/order")
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class OrderController {

    @NonFinal
    @Value("${message.controller.order.cancel-success}")
    String CANCEL_SUCCESS_MESSAGE;

    OrderService orderService;

    @PostMapping
    public ApiResponse<OrderResponse> createOrder(@RequestBody OrderCreationRequest request) {
        return ApiResponse.<OrderResponse>builder()
                .data(orderService.createOrder(request))
                .build();
    }

    @GetMapping("/my-orders")
    public ApiResponse<PageResponse<OrderResponse>> getMyOrders(@RequestParam("page") int page, @RequestParam("size") int size) {
        return ApiResponse.<PageResponse<OrderResponse>>builder()
                .data(orderService.getMyOrders(page, size))
                .build();
    }

    @GetMapping("/my-coming-learning-sessions")
    public ApiResponse<PageResponse<LearningSessionResponse>> getMyComingLearningSessions(@RequestParam("page") int page,@RequestParam("size") int size) {
        return ApiResponse.<PageResponse<LearningSessionResponse>>builder()
                .data(orderService.getMyComingLearningSessions(page, size))
                .build();
    }

    @DeleteMapping("/cancel/{orderId}")
    public ApiResponse<Void> cancelOrder(@PathVariable("orderId") String orderId) {
        orderService.cancelOrder(orderId);
        return ApiResponse.<Void>builder()
                .message(CANCEL_SUCCESS_MESSAGE)
                .build();
    }
}
