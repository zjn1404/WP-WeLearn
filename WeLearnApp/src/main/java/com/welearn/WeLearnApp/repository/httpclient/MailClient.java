package com.welearn.WeLearnApp.repository.httpclient;

import com.welearn.WeLearnApp.dto.request.mail.MailRequest;
import com.welearn.WeLearnApp.dto.response.MailResponse;
import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.http.MediaType;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;

@FeignClient(name = "mail-client", url = "https://api.brevo.com")
public interface MailClient {
    @PostMapping(value = "/v3/smtp/email", produces = MediaType.APPLICATION_JSON_VALUE)
    MailResponse sendEmail(@RequestHeader("api-key") String apiKey, @RequestBody MailRequest mailRequest);
}
