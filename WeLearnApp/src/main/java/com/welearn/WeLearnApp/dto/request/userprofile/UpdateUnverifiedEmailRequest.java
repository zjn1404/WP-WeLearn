package com.welearn.WeLearnApp.dto.request.userprofile;

import lombok.*;
import lombok.experimental.FieldDefaults;

@Getter
@Setter
@Builder
@AllArgsConstructor
@NoArgsConstructor
@FieldDefaults(level = lombok.AccessLevel.PRIVATE)
public class UpdateUnverifiedEmailRequest {
    String email;
    String token;
}
