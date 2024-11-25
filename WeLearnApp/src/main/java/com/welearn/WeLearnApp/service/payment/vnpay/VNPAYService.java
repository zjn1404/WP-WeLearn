package com.welearn.WeLearnApp.service.payment.vnpay;

import com.welearn.WeLearnApp.dto.response.VNPAYResponse;
import jakarta.servlet.http.HttpServletRequest;

import java.io.UnsupportedEncodingException;

public interface VNPAYService {
    VNPAYResponse createPayment(HttpServletRequest request) throws UnsupportedEncodingException;
}
