package com.welearn.WeLearnApp.dto.response;

import lombok.*;
import lombok.experimental.FieldDefaults;

@Getter
@Setter
@Builder
@AllArgsConstructor
@NoArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class EvaluationResponse {
    Integer star;
    String comment;
    String studentId;
    String studentName;
    String tutorId;
    String tutorName;
}
