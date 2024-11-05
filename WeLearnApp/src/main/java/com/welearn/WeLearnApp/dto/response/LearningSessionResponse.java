package com.welearn.WeLearnApp.dto.response;

import lombok.*;
import lombok.experimental.FieldDefaults;

import java.time.LocalDateTime;

@Getter
@Setter
@Builder
@AllArgsConstructor
@NoArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class LearningSessionResponse {
    String id;
    LocalDateTime startTime;
    long duration;
    int grade;
    String subject;
    String learningMethod;
    double tuition;
    UserProfileResponse tutor;
}
