package com.welearn.WeLearnApp.mapper.location;

import com.welearn.WeLearnApp.dto.request.location.LocationCreationRequest;
import com.welearn.WeLearnApp.dto.response.LocationResponse;
import com.welearn.WeLearnApp.entity.Location;

public interface LocationMapper {
    Location toLocation(LocationCreationRequest request);

    LocationResponse toLocationResponse(Location location);
}
