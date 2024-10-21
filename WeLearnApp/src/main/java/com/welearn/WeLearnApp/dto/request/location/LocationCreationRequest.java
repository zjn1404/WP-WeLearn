package com.welearn.WeLearnApp.dto.request.location;

import lombok.*;
import lombok.experimental.FieldDefaults;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class LocationCreationRequest {
    String city;
    String district;
    String street;
}
