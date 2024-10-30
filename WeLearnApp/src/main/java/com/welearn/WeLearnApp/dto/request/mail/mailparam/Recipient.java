package com.welearn.WeLearnApp.dto.request.mail.mailparam;

import lombok.*;
import lombok.experimental.FieldDefaults;

@Setter
@Getter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class Recipient {
    String userId;
    String name;
    String email;
}
