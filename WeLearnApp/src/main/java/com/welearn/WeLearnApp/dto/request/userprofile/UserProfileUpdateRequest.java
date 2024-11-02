package com.welearn.WeLearnApp.dto.request.userprofile;

import jakarta.validation.constraints.Size;
import lombok.*;
import lombok.experimental.FieldDefaults;

import java.time.LocalDate;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class UserProfileUpdateRequest {
    String firstName;
    String lastName;
    LocalDate dob;

    @Size(min = 10, max = 10, message = "INVALID_PHONE_NUMBER")
    String phoneNumber;

    String city;
    String district;
    String street;
    String avatarUrl;
}
