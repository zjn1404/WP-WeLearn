package com.welearn.WeLearnApp.service.location;

import com.welearn.WeLearnApp.dto.request.user.UserCreationRequest;
import com.welearn.WeLearnApp.dto.request.userprofile.UserProfileUpdateRequest;
import com.welearn.WeLearnApp.entity.Location;

public interface LocationService {
    Location internalUpdateLocation(UserProfileUpdateRequest request);

    Location internalCreateLocation(UserCreationRequest request);
}
