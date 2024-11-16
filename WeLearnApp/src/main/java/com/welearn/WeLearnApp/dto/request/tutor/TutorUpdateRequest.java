package com.welearn.WeLearnApp.dto.request.tutor;

import lombok.*;
import lombok.experimental.FieldDefaults;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class TutorUpdateRequest {
    String degree;
    String description;
}
