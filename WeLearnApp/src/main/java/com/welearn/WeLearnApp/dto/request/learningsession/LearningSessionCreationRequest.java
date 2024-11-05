package com.welearn.WeLearnApp.dto.request.learningsession;

import lombok.*;
import lombok.experimental.FieldDefaults;

import java.time.LocalDateTime;

@Getter
@Setter
@Builder
@AllArgsConstructor
@NoArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class LearningSessionCreationRequest {
    LocalDateTime startTime;
    long duration;
    int grade;
    String subject;
    String learningMethod;
    double tuition;
}
