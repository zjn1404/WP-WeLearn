package com.welearn.WeLearnApp.controller;

import com.welearn.WeLearnApp.dto.request.userprofile.TutorFilterRequest;
import com.welearn.WeLearnApp.dto.request.userprofile.UserProfileUpdateRequest;
import com.welearn.WeLearnApp.dto.response.ApiResponse;
import com.welearn.WeLearnApp.dto.response.PageResponse;
import com.welearn.WeLearnApp.dto.response.UserProfileResponse;
import com.welearn.WeLearnApp.service.userprofile.UserProfileService;
import jakarta.validation.Valid;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/user-profile")
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class UserProfileController {
    UserProfileService userProfileService;

    @GetMapping("/{userId}")
    public ApiResponse<UserProfileResponse> getProfileByUserId(@PathVariable("userId") String userId) {
        return ApiResponse.<UserProfileResponse>builder()
                .data(userProfileService.getProfileByUserId(userId))
                .build();
    }

    @GetMapping
    public ApiResponse<PageResponse<UserProfileResponse>> getAllProfiles(@RequestParam(value = "page", required = false, defaultValue = "1") int page,
                                                                         @RequestParam(value = "size", required = false, defaultValue = "10") int size) {
        return ApiResponse.<PageResponse<UserProfileResponse>>builder()
                .data(userProfileService.getAllProfiles(page, size))
                .build();
    }

    @GetMapping("/me")
    public ApiResponse<UserProfileResponse> getMyProfile() {
        return ApiResponse.<UserProfileResponse>builder()
                .data(userProfileService.getMyProfile())
                .build();
    }

    @GetMapping("/search")
    public ApiResponse<PageResponse<UserProfileResponse>> searchProfiles(@RequestParam("firstName") String firstName,
                                                                        @RequestParam("lastName") String lastName,
                                                                        @RequestParam(value = "page", required = false, defaultValue = "1")  int page,
                                                                        @RequestParam(value = "size", required = false, defaultValue = "10") int size) {


        return ApiResponse.<PageResponse<UserProfileResponse>>builder()
                .data(userProfileService.searchProfiles(firstName, lastName, page, size))
                .build();
    }

    @GetMapping("/filter")
    public ApiResponse<PageResponse<UserProfileResponse>> filterProfiles(@RequestParam(value = "page", required = false, defaultValue = "1") int page,
                                                                        @RequestParam(value = "size", required = false, defaultValue = "10") int size,
                                                                         @RequestBody @Valid TutorFilterRequest request) {
        return ApiResponse.<PageResponse<UserProfileResponse>>builder()
                .data(userProfileService.filterProfiles(request, page, size))
                .build();
    }

    @PatchMapping("/{userId}")
    public ApiResponse<UserProfileResponse> updateProfile(@PathVariable("userId") String userId,
                                                          @RequestBody @Valid UserProfileUpdateRequest request) {
        return ApiResponse.<UserProfileResponse>builder()
                .data(userProfileService.updateProfile(userId, request))
                .build();
    }

    @PatchMapping("/me")
    public ApiResponse<UserProfileResponse> updateMyProfile(@RequestBody @Valid UserProfileUpdateRequest request) {
        return ApiResponse.<UserProfileResponse>builder()
                .data(userProfileService.updateMyProfile(request))
                .build();
    }
}
