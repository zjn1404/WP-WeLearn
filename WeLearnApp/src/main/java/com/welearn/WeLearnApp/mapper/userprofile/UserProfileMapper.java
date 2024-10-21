package com.welearn.WeLearnApp.mapper.userprofile;

import com.welearn.WeLearnApp.dto.request.userprofile.UserProfileCreationRequest;
import com.welearn.WeLearnApp.dto.response.UserProfileResponse;
import com.welearn.WeLearnApp.entity.UserProfile;

public interface UserProfileMapper {
    UserProfile toUserProfile(UserProfileCreationRequest request);

    void updateUserProfile(UserProfile userProfile, UserProfileCreationRequest request);

    UserProfileResponse toUserProfileResponse(UserProfile userProfile);
}
