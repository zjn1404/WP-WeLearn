package com.welearn.WeLearnApp.configuration.payment;

import java.text.SimpleDateFormat;
import java.util.*;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Configuration;

import lombok.AccessLevel;
import lombok.Getter;
import lombok.experimental.FieldDefaults;

@Configuration
@Getter
@FieldDefaults(level = AccessLevel.PRIVATE)
public class VNPAYConfig {

    @Value("${payment.vnp.url}")
    String PAY_URL;

    @Value("${payment.vnp.vnp-ReturnUrl}")
    String RETURN_URL;

    @Value("${payment.vnp.tmn-code}")
    String TMN_CODE;

    @Value("${payment.vnp.hash-secret}")
    String HASH_SECRET;

    @Value("${payment.vnp.vnp-version}")
    String VNP_VERSION;

    @Value("${payment.vnp.vnp_command}")
    String VNP_COMMAND;

    @Value("${payment.vnp.vnp-CurrCode}")
    String CURR_CODE;

    @Value("${payment.vnp.vnp-OrderType}")
    String ORDER_TYPE;

    @Value("${payment.vnp.vnp-ExpireDuration}")
    int EXPIRE_DURATION;

    public Map<String, String> getVNPAYConfigs() {
        Map<String, String> vnpParams = new HashMap<>();

        vnpParams.put("vnp_Version", VNP_VERSION);
        vnpParams.put("vnp_Command", VNP_COMMAND);
        vnpParams.put("vnp_TmnCode", TMN_CODE);
        vnpParams.put("vnp_CurrCode", CURR_CODE);
        vnpParams.put("vnp_ReturnUrl", RETURN_URL);
        vnpParams.put("vnp_OrderType", ORDER_TYPE);

        Calendar cld = Calendar.getInstance(TimeZone.getTimeZone("Etc/GMT+7"));
        SimpleDateFormat formatter = new SimpleDateFormat("yyyyMMddHHmmss");
        String vnpCreateDate = formatter.format(cld.getTime());
        vnpParams.put("vnp_CreateDate", vnpCreateDate);

        cld.add(Calendar.MINUTE, EXPIRE_DURATION);
        String vnpExpireDate = formatter.format(cld.getTime());
        vnpParams.put("vnp_ExpireDate", vnpExpireDate);

        return vnpParams;
    }
}
