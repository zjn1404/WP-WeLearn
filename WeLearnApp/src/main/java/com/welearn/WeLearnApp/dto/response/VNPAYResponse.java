package com.welearn.WeLearnApp.dto.response;

import lombok.*;
import lombok.experimental.FieldDefaults;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class VNPAYResponse {
    String vnpCode;
    String vnpMessage;
    String paymentUrl;
}
