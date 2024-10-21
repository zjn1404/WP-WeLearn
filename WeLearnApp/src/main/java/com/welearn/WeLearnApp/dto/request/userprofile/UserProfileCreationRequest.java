package com.welearn.WeLearnApp.dto.request.userprofile;

import lombok.*;
import lombok.experimental.FieldDefaults;

import java.time.LocalDate;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class UserProfileCreationRequest {
    String firstName;
    String lastName;
    LocalDate dob;
    String city;
    String district;
    String street;
}
