package com.welearn.WeLearnApp.service.userprofile;

import com.welearn.WeLearnApp.dto.request.user.UserCreationRequest;
import com.welearn.WeLearnApp.dto.request.userprofile.UserProfileUpdateRequest;
import com.welearn.WeLearnApp.dto.response.LocationResponse;
import com.welearn.WeLearnApp.dto.response.UserProfileResponse;
import com.welearn.WeLearnApp.entity.Location;
import com.welearn.WeLearnApp.entity.UserProfile;
import com.welearn.WeLearnApp.exception.AppException;
import com.welearn.WeLearnApp.exception.ErrorCode;
import com.welearn.WeLearnApp.mapper.location.LocationMapper;
import com.welearn.WeLearnApp.mapper.userprofile.UserProfileMapper;
import com.welearn.WeLearnApp.repository.UserProfileRepository;
import com.welearn.WeLearnApp.service.location.LocationService;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Service;

import java.util.Objects;

@Service
@RequiredArgsConstructor
@FieldDefaults(level = lombok.AccessLevel.PRIVATE, makeFinal = true)
public class UserProfileServiceImpl implements UserProfileService {

    UserProfileRepository userProfileRepository;

    LocationService locationService;

    UserProfileMapper userProfileMapper;
    LocationMapper locationMapper;

    @Override
    public UserProfileResponse updateProfile(String userId, UserProfileUpdateRequest request) {
        UserProfile userProfile = userProfileRepository.findById(userId)
                .orElseThrow(() -> new AppException(ErrorCode.USER_PROFILE_NOT_FOUND));

        userProfileMapper.updateUserProfile(userProfile, request);

        userProfile.setLocation(locationService.internalUpdateLocation(request));

        return buildUserProfileResponse(userProfile);
    }

    @Override
    public UserProfileResponse updateMyProfile(UserProfileUpdateRequest request) {
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();

        UserProfile userProfile = userProfileRepository.findById(authentication.getName())
                .orElseThrow(() -> new AppException(ErrorCode.USER_PROFILE_NOT_FOUND));

        userProfileMapper.updateUserProfile(userProfile, request);

        userProfile.setLocation(locationService.internalUpdateLocation(request));

        return buildUserProfileResponse(userProfile);
    }

    @Override
    public UserProfileResponse getProfileByUserId(String userId) {
        UserProfile userProfile = userProfileRepository.findById(userId)
                .orElseThrow(() -> new AppException(ErrorCode.USER_PROFILE_NOT_FOUND));

        return buildUserProfileResponse(userProfile);
    }

    @Override
    public UserProfileResponse getMyProfile() {
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
        UserProfile userProfile = userProfileRepository.findById(Objects.requireNonNull(authentication.getName()))
                .orElseThrow(() -> new AppException(ErrorCode.USER_PROFILE_NOT_FOUND));

        return buildUserProfileResponse(userProfile);
    }

    @Override
    public void internalCreateProfile(String userId, Location location, UserCreationRequest request) {
        UserProfile userProfile = userProfileMapper.toUserProfile(request);
        userProfile.setId(userId);
        userProfile.setLocation(location);
        userProfileRepository.save(userProfile);
    }

    private UserProfileResponse buildUserProfileResponse(UserProfile userProfile) {
        UserProfileResponse userProfileResponse = userProfileMapper.toUserProfileResponse(userProfile);
        if (!Objects.isNull(userProfile.getLocation())) {
            LocationResponse locationResponse = locationMapper.toLocationResponse(userProfile.getLocation());
            userProfileResponse.setLocation(locationResponse);
        }

        return userProfileResponse;
    }
}
