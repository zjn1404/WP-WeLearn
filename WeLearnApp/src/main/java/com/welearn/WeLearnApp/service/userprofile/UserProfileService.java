package com.welearn.WeLearnApp.service.userprofile;

import com.welearn.WeLearnApp.dto.request.user.UserCreationRequest;
import com.welearn.WeLearnApp.dto.request.userprofile.UserProfileUpdateRequest;
import com.welearn.WeLearnApp.dto.response.UserProfileResponse;
import com.welearn.WeLearnApp.entity.Location;

public interface UserProfileService {
    UserProfileResponse updateProfile(String userId, UserProfileUpdateRequest request);

    UserProfileResponse updateMyProfile(UserProfileUpdateRequest request);

    UserProfileResponse getProfileByUserId(String userId);

    UserProfileResponse getMyProfile();

    void internalCreateProfile(String userId, Location location, UserCreationRequest request);
}
