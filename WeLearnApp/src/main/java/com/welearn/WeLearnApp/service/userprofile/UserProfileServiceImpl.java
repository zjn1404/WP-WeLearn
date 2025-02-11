package com.welearn.WeLearnApp.service.userprofile;

import com.welearn.WeLearnApp.dto.request.user.UserCreationRequest;
import com.welearn.WeLearnApp.dto.request.userprofile.TutorFilterRequest;
import com.welearn.WeLearnApp.dto.request.userprofile.UserProfileUpdateRequest;
import com.welearn.WeLearnApp.dto.response.LocationResponse;
import com.welearn.WeLearnApp.dto.response.PageResponse;
import com.welearn.WeLearnApp.dto.response.UserProfileResponse;
import com.welearn.WeLearnApp.entity.Location;
import com.welearn.WeLearnApp.entity.UserProfile;
import com.welearn.WeLearnApp.enums.ERole;
import com.welearn.WeLearnApp.exception.AppException;
import com.welearn.WeLearnApp.exception.ErrorCode;
import com.welearn.WeLearnApp.mapper.location.LocationMapper;
import com.welearn.WeLearnApp.mapper.userprofile.UserProfileMapper;
import com.welearn.WeLearnApp.repository.UserProfileRepository;
import com.welearn.WeLearnApp.service.location.LocationService;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.extern.slf4j.Slf4j;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Objects;

@Slf4j
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

        userProfileRepository.save(userProfile);

        return buildUserProfileResponse(userProfile);
    }

    @Override
    public UserProfileResponse updateMyProfile(UserProfileUpdateRequest request) {
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();

        UserProfile userProfile = userProfileRepository.findById(authentication.getName())
                .orElseThrow(() -> new AppException(ErrorCode.USER_PROFILE_NOT_FOUND));

        userProfileMapper.updateUserProfile(userProfile, request);

        userProfile.setLocation(locationService.internalUpdateLocation(request));

        userProfileRepository.save(userProfile);

        return buildUserProfileResponse(userProfile);
    }

    @Override
    public UserProfileResponse getProfileByUserId(String userId) {
        UserProfile userProfile = userProfileRepository.findById(userId)
                .orElseThrow(() -> new AppException(ErrorCode.USER_PROFILE_NOT_FOUND));

        return buildUserProfileResponse(userProfile);
    }

    @Override
    public PageResponse<UserProfileResponse> getAllProfiles(int page, int size) {
        Pageable pageable = PageRequest.of(page - 1, size);

        Page<UserProfile> userProfiles = userProfileRepository.findAll(pageable);

        return buildPageUserProfileResponse(userProfiles);
    }

    @Override
    public PageResponse<UserProfileResponse> getAllTutorProfiles(int page, int size) {
        Pageable pageable = PageRequest.of(page - 1, size);

        Page<UserProfile> userProfiles = userProfileRepository.findAllByRole(ERole.TUTOR.getName(), pageable);

        return buildPageUserProfileResponse(userProfiles);
    }

    @Override
    public UserProfileResponse getMyProfile() {
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
        UserProfile userProfile = userProfileRepository.findById(Objects.requireNonNull(authentication.getName()))
                .orElseThrow(() -> new AppException(ErrorCode.USER_PROFILE_NOT_FOUND));

        return buildUserProfileResponse(userProfile);
    }

    @Override
    public List<UserProfileResponse> getTopThreeTutorsByGradeAndSubject(int grade, String subject) {
        List<UserProfile> userProfiles = userProfileRepository.findTopThreeTutorsByGradeAndSubject(grade, subject);

        return userProfiles.stream().map(this::buildUserProfileResponse).toList();
    }

    @Override
    public PageResponse<UserProfileResponse> searchTutorProfiles(String keyword, int page, int size) {
        Pageable pageable = PageRequest.of(page - 1, size);

        Page<UserProfile> userProfiles = userProfileRepository.findAllTutorProfileByNameContainingIgnoreCase(keyword, pageable);

        return buildPageUserProfileResponse(userProfiles);
    }

    @Override
    public PageResponse<UserProfileResponse> filterProfiles(TutorFilterRequest request, int page, int size) {
        Pageable pageable = PageRequest.of(page - 1, size);
        log.info("Filtering profiles with request: {}", request);
        Page<UserProfile> userProfiles = userProfileRepository.findAllByLocationAndGradeAndSubject(
                ERole.TUTOR.getName(),
                request.getCity(),
                request.getDistrict(),
                request.getStreet(),
                request.getGrade(),
                request.getSubject(),
                request.getLearningMethod(),
                request.getTuition()
                , pageable);
        log.info("Found {} profiles", userProfiles.getTotalElements());
        return buildPageUserProfileResponse(userProfiles);
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

    private PageResponse<UserProfileResponse> buildPageUserProfileResponse(Page<UserProfile> userProfiles) {
        List<UserProfileResponse> userProfileResponses = userProfiles.stream().map(profile -> {
            UserProfileResponse userProfileResponse = userProfileMapper.toUserProfileResponse(profile);
            userProfileResponse.setEmail(profile.getUser().getEmail());
            userProfileResponse.setLocation(locationMapper.toLocationResponse(profile.getLocation()));
            return userProfileResponse;
        }).toList();

        return PageResponse.<UserProfileResponse>builder()
                .currentPage(userProfiles.getNumber() + 1)
                .totalPage(userProfiles.getTotalPages())
                .totalElement(userProfiles.getTotalElements())
                .elementPerPage(userProfiles.getSize())
                .data(userProfileResponses)
                .build();
    }
}
