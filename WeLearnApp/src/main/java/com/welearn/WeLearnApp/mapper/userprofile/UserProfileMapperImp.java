package com.welearn.WeLearnApp.mapper.userprofile;

import com.welearn.WeLearnApp.dto.request.user.UserCreationRequest;
import com.welearn.WeLearnApp.dto.request.userprofile.UserProfileCreationRequest;
import com.welearn.WeLearnApp.dto.request.userprofile.UserProfileUpdateRequest;
import com.welearn.WeLearnApp.dto.response.UserProfileResponse;
import com.welearn.WeLearnApp.entity.UserProfile;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import org.springframework.stereotype.Component;

@Component
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class UserProfileMapperImp implements UserProfileMapper {

    @Override
    public UserProfile toUserProfile(UserProfileCreationRequest request) {
        return UserProfile.builder()
                .firstName(request.getFirstName())
                .lastName(request.getLastName())
                .dob(request.getDob())
                .build();
    }

    @Override
    public UserProfile toUserProfile(UserCreationRequest request) {
        return UserProfile.builder()
                .firstName(request.getFirstName())
                .lastName(request.getLastName())
                .dob(request.getDob())
                .build();
    }

    @Override
    public void updateUserProfile(UserProfile userProfile, UserProfileUpdateRequest request) {
        if (request.getFirstName() != null) {
            userProfile.setFirstName(request.getFirstName());
        }
        if (request.getLastName() != null) {
            userProfile.setLastName(request.getLastName());
        }
        if (request.getDob() != null) {
            userProfile.setDob(request.getDob());
        }
    }

    @Override
    public UserProfileResponse toUserProfileResponse(UserProfile userProfile) {
        return UserProfileResponse.builder()
                .firstName(userProfile.getFirstName())
                .lastName(userProfile.getLastName())
                .dob(userProfile.getDob())
                .build();
    }
}
