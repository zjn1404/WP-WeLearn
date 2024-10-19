package com.welearn.WeLearnApp.dto.request.user;

import jakarta.validation.constraints.NotNull;
import jakarta.validation.constraints.Pattern;
import jakarta.validation.constraints.Size;
import lombok.*;
import lombok.experimental.FieldDefaults;

@Getter
@Setter
@Builder
@AllArgsConstructor
@NoArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class UserCreationRequest {
    @Size(min = 5, message = "INVALID_USERNAME")
    String username;

    @Size(min = 8, message = "INVALID_PASSWORD")
    @Pattern(regexp = "^(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&*(){}\\[\\]!~`|])(?=.*\\d).*$", message = "INVALID_PASSWORD")
    String password;

    @NotNull(message = "REQUIRED_EMAIL")
    String email;

    String role;

    // User Profile
    String firstName;
    String lastName;
    String dob;
    String city;
    String district;
    String street;
}
