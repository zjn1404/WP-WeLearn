package com.welearn.WeLearnApp.controller;

import com.welearn.WeLearnApp.dto.request.order.OrderCreationRequest;
import com.welearn.WeLearnApp.dto.response.ApiResponse;
import com.welearn.WeLearnApp.dto.response.VNPAYResponse;
import com.welearn.WeLearnApp.service.order.OrderService;
import com.welearn.WeLearnApp.service.payment.vnpay.VNPAYService;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.experimental.NonFinal;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import java.io.IOException;
import java.io.UnsupportedEncodingException;

@Slf4j
@RestController
@RequestMapping("/payment")
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class PaymentController {

    VNPAYService vnpayService;
    OrderService orderService;

    @NonFinal
    @Value("${payment.vnp.client-payment-success-url}")
    String CLIENT_PAYMENT_SUCCESS_URL;

    @NonFinal
    @Value("${payment.vnp.client-payment-fail-url}")
    String CLIENT_PAYMENT_FAIL_URL;

    @NonFinal
    @Value("${payment.vnp.vnp-success-code}")
    String VNP_SUCCESS_CODE;

    @GetMapping("/create-payment")
    public ApiResponse<VNPAYResponse> createPayment(HttpServletRequest req) throws UnsupportedEncodingException {
        VNPAYResponse vnpayResponse = vnpayService.createPayment(req);

        return ApiResponse.<VNPAYResponse>builder()
                .data(vnpayResponse)
                .build();
    }

    @GetMapping("/vnp/callback")
    public void vnpCallback(
            @RequestParam("vnp_ResponseCode") String responseCode,
            @RequestParam("vnp_TxnRef") String txnRef,
            HttpServletResponse response)
            throws IOException {
        if (responseCode.equals(VNP_SUCCESS_CODE)) {
            String learningSessionId = txnRef.split("-sessionId-")[1].split("-userId-")[0];
            String userId = txnRef.split("-userId-")[1];
            orderService.createOrder(OrderCreationRequest.builder()
                            .learningSessionId(learningSessionId)
                            .userId(userId)
                            .build());

            response.sendRedirect(CLIENT_PAYMENT_SUCCESS_URL);
        } else {
            response.sendRedirect(CLIENT_PAYMENT_FAIL_URL);
        }
    }
}
