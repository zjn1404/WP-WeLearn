package com.welearn.WeLearnApp.dto.request.verificationcode;

import lombok.*;
import lombok.experimental.FieldDefaults;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class VerifyCodeRequest {
    String userId;
    String code;
}
