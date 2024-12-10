package com.welearn.WeLearnApp.dto.request.userprofile;

import jakarta.validation.constraints.Max;
import jakarta.validation.constraints.Min;
import lombok.*;
import lombok.experimental.FieldDefaults;

@ToString
@Getter
@Setter
@Builder
@AllArgsConstructor
@NoArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class TutorFilterRequest {
    String city;
    String district;
    String street;

    @Min(value = 1, message = "INVALID_GRADE")
    @Max(value = 12, message = "INVALID_GRADE")
    Integer grade;

    String subject;
    String learningMethod;

    @Min(value = 0, message = "INVALID_TUITION")
    Double tuition;
}
