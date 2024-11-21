package com.welearn.WeLearnApp.dto.request.evaluation;

import lombok.*;
import lombok.experimental.FieldDefaults;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class EvaluationRequest {
    String tutorId;
    Integer star;
    String comment;
}
