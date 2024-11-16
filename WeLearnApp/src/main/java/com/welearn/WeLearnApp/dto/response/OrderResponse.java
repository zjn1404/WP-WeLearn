package com.welearn.WeLearnApp.dto.response;

import lombok.*;
import lombok.experimental.FieldDefaults;

import java.time.LocalDateTime;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class OrderResponse {
    String id;
    LocalDateTime orderTime;
    String studentId;
    UserProfileResponse tutor;
    OrderDetailResponse orderDetail;
}
