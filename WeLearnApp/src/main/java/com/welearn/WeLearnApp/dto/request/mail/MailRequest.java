package com.welearn.WeLearnApp.dto.request.mail;

import com.welearn.WeLearnApp.dto.request.mail.mailparam.Recipient;
import com.welearn.WeLearnApp.dto.request.mail.mailparam.Sender;
import lombok.*;
import lombok.experimental.FieldDefaults;

import java.util.List;

@Getter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class MailRequest {
    Sender sender;
    List<Recipient> to;
    String subject;
    String htmlContent;
}
