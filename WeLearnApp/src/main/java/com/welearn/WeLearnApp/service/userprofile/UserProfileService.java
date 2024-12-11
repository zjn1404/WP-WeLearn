package com.welearn.WeLearnApp.service.userprofile;

import com.welearn.WeLearnApp.dto.request.user.UserCreationRequest;
import com.welearn.WeLearnApp.dto.request.userprofile.TutorFilterRequest;
import com.welearn.WeLearnApp.dto.request.userprofile.UserProfileUpdateRequest;
import com.welearn.WeLearnApp.dto.response.PageResponse;
import com.welearn.WeLearnApp.dto.response.UserProfileResponse;
import com.welearn.WeLearnApp.entity.Location;

import java.util.List;

public interface UserProfileService {
    UserProfileResponse updateProfile(String userId, UserProfileUpdateRequest request);

    UserProfileResponse updateMyProfile(UserProfileUpdateRequest request);

    UserProfileResponse getProfileByUserId(String userId);

    UserProfileResponse getMyProfile();

    List<UserProfileResponse> getTopThreeTutorsByGradeAndSubject(int grade, String subject);

    PageResponse<UserProfileResponse> getAllProfiles(int page, int size);

    PageResponse<UserProfileResponse> getAllTutorProfiles(int page, int size);

    PageResponse<UserProfileResponse> searchTutorProfiles(String keyword, int page, int size);

    PageResponse<UserProfileResponse> filterProfiles(TutorFilterRequest request, int page, int size);

    void internalCreateProfile(String userId, Location location, UserCreationRequest request);
}
