package com.welearn.WeLearnApp.service.user;

import com.welearn.WeLearnApp.dto.request.user.UserCreationRequest;
import com.welearn.WeLearnApp.dto.request.user.UserUpdateRequest;
import com.welearn.WeLearnApp.dto.response.UserResponse;
import com.welearn.WeLearnApp.entity.Location;
import com.welearn.WeLearnApp.entity.Role;
import com.welearn.WeLearnApp.entity.User;
import com.welearn.WeLearnApp.entity.UserProfile;
import com.welearn.WeLearnApp.exception.AppException;
import com.welearn.WeLearnApp.exception.ErrorCode;
import com.welearn.WeLearnApp.mapper.location.LocationMapper;
import com.welearn.WeLearnApp.mapper.role.RoleMapper;
import com.welearn.WeLearnApp.mapper.user.UserMapper;
import com.welearn.WeLearnApp.mapper.userprofile.UserProfileMapper;
import com.welearn.WeLearnApp.repository.LocationRepository;
import com.welearn.WeLearnApp.repository.RoleRepository;
import com.welearn.WeLearnApp.repository.UserProfileRepository;
import com.welearn.WeLearnApp.repository.UserRepository;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class UserServiceImp implements UserService{

    UserRepository userRepository;
    UserProfileRepository userProfileRepository;
    LocationRepository locationRepository;
    RoleRepository roleRepository;

    UserMapper userMapper;
    UserProfileMapper userProfileMapper;
    LocationMapper locationMapper;
    RoleMapper roleMapper;

    @Override
    public UserResponse createUser(UserCreationRequest request) {

        if (userRepository.existsByUsername(request.getUsername())) {
            throw new AppException(ErrorCode.USER_EXISTED);
        }

        User user = userMapper.toUser(request);
        Role role = roleRepository.findById("ROLE_USER")
                .orElseThrow(() -> new AppException(ErrorCode.ROLE_NOT_FOUND));
        user.setRole(role);

        userRepository.save(user);

        UserProfile userProfile = userProfileMapper.toUserProfile(request);
        userProfile.setId(user.getId());
        userProfileRepository.save(userProfile);

        Location location = locationMapper.toLocation(request);
        locationRepository.save(location);

        return buildUserResponse(user);
    }

    @Override
    public UserResponse getUserById(String id) {
        User user = userRepository.findById(id)
                .orElseThrow(() -> new AppException(ErrorCode.USER_NOT_FOUND));

        return buildUserResponse(user);
    }

    @Override
    public UserResponse updateUser(String id, UserUpdateRequest request) {
        User user = userRepository.findById(id)
                .orElseThrow(() -> new AppException(ErrorCode.USER_NOT_FOUND));

        userMapper.updateUser(user, request);
        userRepository.saveAndFlush(user);

        return buildUserResponse(user);
    }

    @Override
    public void deleteUser(String id) {
        User user = userRepository.findById(id)
                .orElseThrow(() -> new AppException(ErrorCode.USER_NOT_FOUND));

        userRepository.delete(user);
    }

    private UserResponse buildUserResponse(User user) {
        UserResponse userResponse = userMapper.toUserResponse(user);
        userResponse.setRole(roleMapper.toRoleResponse(user.getRole()));

        return userResponse;
    }
}
