package com.welearn.WeLearnApp.dto.request.userprofile;

import lombok.*;
import lombok.experimental.FieldDefaults;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class UserProfileUpdateRequest {
    String firstName;
    String lastName;
    String dob;
    String city;
    String district;
    String street;
}
