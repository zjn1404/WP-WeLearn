package com.welearn.WeLearnApp.mapper.location;

import com.welearn.WeLearnApp.dto.request.location.LocationCreationRequest;
import com.welearn.WeLearnApp.dto.response.LocationResponse;
import com.welearn.WeLearnApp.entity.Location;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import org.springframework.stereotype.Component;

@Component
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class LocationMapperImp implements LocationMapper {

    @Override
    public Location toLocation(LocationCreationRequest request) {
        return Location.builder()
                .city(request.getCity())
                .district(request.getDistrict())
                .street(request.getStreet())
                .build();
    }

    @Override
    public LocationResponse toLocationResponse(Location location) {
        return LocationResponse.builder()
                .city(location.getCity())
                .district(location.getDistrict())
                .street(location.getStreet())
                .build();
    }
}
