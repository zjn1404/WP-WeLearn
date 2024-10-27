package com.welearn.WeLearnApp.mapper.location;

import com.welearn.WeLearnApp.dto.request.location.LocationUpdateRequest;
import com.welearn.WeLearnApp.dto.request.user.UserCreationRequest;
import com.welearn.WeLearnApp.dto.request.userprofile.UserProfileUpdateRequest;
import com.welearn.WeLearnApp.dto.response.LocationResponse;
import com.welearn.WeLearnApp.entity.Location;

public interface LocationMapper {
    Location toLocation(LocationUpdateRequest request);

    Location toLocation(UserCreationRequest request);

    Location toLocation(UserProfileUpdateRequest request);

    void updateLocation(Location location, LocationUpdateRequest request);

    void updateLocation(Location location, UserProfileUpdateRequest request);

    LocationResponse toLocationResponse(Location location);
}
