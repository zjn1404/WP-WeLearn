package com.welearn.WeLearnApp.mapper.location;

import com.welearn.WeLearnApp.dto.request.location.LocationUpdateRequest;
import com.welearn.WeLearnApp.dto.request.user.UserCreationRequest;
import com.welearn.WeLearnApp.dto.request.userprofile.UserProfileUpdateRequest;
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
    public Location toLocation(LocationUpdateRequest request) {
        return toLocation(request.getCity(), request.getDistrict(), request.getStreet());
    }

    @Override
    public Location toLocation(UserCreationRequest request) {
        return toLocation(request.getCity(), request.getDistrict(), request.getStreet());
    }

    @Override
    public Location toLocation(UserProfileUpdateRequest request) {
        return toLocation(request.getCity(), request.getDistrict(), request.getStreet());
    }

    @Override
    public void updateLocation(Location location, LocationUpdateRequest request) {
        updateLocation(location, request.getCity(), request.getDistrict(), request.getStreet());
    }

    @Override
    public void updateLocation(Location location, UserProfileUpdateRequest request) {
        updateLocation(location, request.getCity(), request.getDistrict(), request.getStreet());
    }

    @Override
    public LocationResponse toLocationResponse(Location location) {
        return LocationResponse.builder()
                .city(location.getCity())
                .district(location.getDistrict())
                .street(location.getStreet())
                .build();
    }

    private Location toLocation(String city, String district, String street) {
        return Location.builder()
                .city(city)
                .district(district)
                .street(street)
                .build();
    }

    private void updateLocation(Location location, String city, String district, String street) {
        if (city != null) {
            location.setCity(city);
        }

        if (district != null) {
            location.setDistrict(district);
        }

        if (street != null) {
            location.setStreet(street);
        }
    }

}
