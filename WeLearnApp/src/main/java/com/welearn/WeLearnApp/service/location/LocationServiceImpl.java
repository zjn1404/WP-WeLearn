package com.welearn.WeLearnApp.service.location;

import com.welearn.WeLearnApp.dto.request.user.UserCreationRequest;
import com.welearn.WeLearnApp.dto.request.userprofile.UserProfileUpdateRequest;
import com.welearn.WeLearnApp.dto.response.LocationResponse;
import com.welearn.WeLearnApp.entity.Location;
import com.welearn.WeLearnApp.mapper.location.LocationMapper;
import com.welearn.WeLearnApp.repository.LocationRepository;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.extern.slf4j.Slf4j;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Service;

import java.util.Objects;

@Slf4j
@Service
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class LocationServiceImpl implements LocationService {

    LocationRepository locationRepository;

    LocationMapper locationMapper;

    @Override
    public Location internalUpdateLocation(UserProfileUpdateRequest request) {
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();

        Location location = locationRepository.findLocationByUserId(authentication.getName()).orElse(null);
        log.info("Id: {}", authentication.getName());
        log.info("Location: {}", location);

        if (!Objects.isNull(location)) {
            locationMapper.updateLocation(location, request);
        } else {
            location = locationMapper.toLocation(request);
        }

        return locationRepository.saveAndFlush(location);
    }

    @Override
    public Location internalCreateLocation(UserCreationRequest request) {
        if (!Objects.isNull(request.getCity()) &&
                !locationRepository.existsByCityAndDistrictAndStreet(request.getCity(),
                        request.getDistrict(), request.getStreet())) {
            Location location = locationMapper.toLocation(request);
            locationRepository.save(location);
            return location;
        }
        return null;
    }
}
