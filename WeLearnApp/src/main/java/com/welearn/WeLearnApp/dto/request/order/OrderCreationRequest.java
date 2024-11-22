package com.welearn.WeLearnApp.dto.request.order;

import lombok.*;
import lombok.experimental.FieldDefaults;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class OrderCreationRequest {
    String learningSessionId;
}
