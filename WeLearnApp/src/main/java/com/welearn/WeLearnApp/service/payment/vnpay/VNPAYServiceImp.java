package com.welearn.WeLearnApp.service.payment.vnpay;

import com.welearn.WeLearnApp.configuration.payment.VNPAYConfig;
import com.welearn.WeLearnApp.dto.response.VNPAYResponse;
import jakarta.servlet.http.HttpServletRequest;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.experimental.NonFinal;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.stereotype.Service;

import javax.crypto.Mac;
import javax.crypto.spec.SecretKeySpec;
import java.net.URLEncoder;
import java.nio.charset.StandardCharsets;
import java.util.*;

@Service
@RequiredArgsConstructor
@FieldDefaults(level = lombok.AccessLevel.PRIVATE, makeFinal = true)
public class VNPAYServiceImp implements VNPAYService {

    @NonFinal
    @Value("${payment.vnp.vnp-success-code}")
    String VNP_SUCCESS_CODE;

    @NonFinal
    @Value("${payment.vnp.vnp-Locale}")
    String VNP_LOCALE;

    @NonFinal
    @Value("${payment.vnp.vnp-OrderInfo}")
    String VNP_ORDER_INFO;

    VNPAYConfig vnpayConfig;

    // params: amount, bankCode, language
    @Override
    public VNPAYResponse createPayment(HttpServletRequest request) {
        long amount = Integer.parseInt(request.getParameter("amount")) * 100L;
        String bankCode = request.getParameter("bankCode");
        String vnpIpAddress = getIpAddress(request);
        String vnpTxnRef = UUID.randomUUID().toString();

        Map<String, String> vnpParams = vnpayConfig.getVNPAYConfigs();
        vnpParams.put("vnp_Amount", String.valueOf(amount));
        vnpParams.put("vnp_TxnRef", vnpTxnRef);
        vnpParams.put("vnp_OrderInfo", VNP_ORDER_INFO + vnpTxnRef);

        if (bankCode != null && !bankCode.isEmpty()) {
            vnpParams.put("vnp_BankCode", bankCode);
        }

        String locate = request.getParameter("language");
        if (locate != null && !locate.isEmpty()) {
            vnpParams.put("vnp_Locale", locate);
        } else {
            vnpParams.put("vnp_Locale", VNP_LOCALE);
        }
        vnpParams.put("vnp_IpAddr", vnpIpAddress);

        String queryUrl = buildUrl(vnpParams, vnpayConfig.getHASH_SECRET());

        String paymentUrl = vnpayConfig.getPAY_URL() + "?" + queryUrl;

        return VNPAYResponse.builder()
                .vnpCode(VNP_SUCCESS_CODE)
                .vnpMessage("success")
                .paymentUrl(paymentUrl)
                .build();
    }

    private String getIpAddress(HttpServletRequest request) {
        String ipAdress;
        try {
            ipAdress = request.getHeader("X-FORWARDED-FOR");
            if (ipAdress == null) {
                ipAdress = request.getRemoteAddr();
            }
        } catch (Exception e) {
            ipAdress = "Invalid IP:" + e.getMessage();
        }
        return ipAdress;
    }

    private String buildUrl(Map<String, String> vnpParams, String secretKey) {
        List<String> fieldNames = new ArrayList<>(vnpParams.keySet());
        Collections.sort(fieldNames);
        StringBuilder hashData = new StringBuilder();
        StringBuilder query = new StringBuilder();
        Iterator<String> itr = fieldNames.iterator();

        while (itr.hasNext()) {
            String fieldName = itr.next();
            String fieldValue = vnpParams.get(fieldName);
            if ((fieldValue != null) && (!fieldValue.isEmpty())) {
                // Build hash data
                hashData.append(fieldName);
                hashData.append('=');
                hashData.append(URLEncoder.encode(fieldValue, StandardCharsets.US_ASCII));
                // Build query
                query.append(URLEncoder.encode(fieldName, StandardCharsets.US_ASCII));
                query.append('=');
                query.append(URLEncoder.encode(fieldValue, StandardCharsets.US_ASCII));
                if (itr.hasNext()) {
                    query.append('&');
                    hashData.append('&');
                }
            }
        }

        String vnpSecureHash = hmacSHA512(secretKey, hashData.toString());
        query.append("&vnp_SecureHash=").append(vnpSecureHash);

        return query.toString();
    }

    private String hmacSHA512(final String key, final String data) {
        try {

            if (key == null || data == null) {
                throw new NullPointerException();
            }
            final Mac hmac512 = Mac.getInstance("HmacSHA512");
            byte[] hmacKeyBytes = key.getBytes();
            final SecretKeySpec secretKey = new SecretKeySpec(hmacKeyBytes, "HmacSHA512");
            hmac512.init(secretKey);
            byte[] dataBytes = data.getBytes(StandardCharsets.UTF_8);
            byte[] result = hmac512.doFinal(dataBytes);
            StringBuilder sb = new StringBuilder(2 * result.length);
            for (byte b : result) {
                sb.append(String.format("%02x", b & 0xff));
            }
            return sb.toString();

        } catch (Exception ex) {
            return "";
        }
    }

}
