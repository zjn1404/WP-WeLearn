package com.welearn.WeLearnApp.service.user;

import com.welearn.WeLearnApp.dto.request.user.UserCreationRequest;
import com.welearn.WeLearnApp.dto.request.user.UserUpdateRequest;
import com.welearn.WeLearnApp.dto.response.UserResponse;

public interface UserService {
    UserResponse createUser(UserCreationRequest request);

    UserResponse getUserById(String id);

    UserResponse updateUser(String id, UserUpdateRequest request);

    void deleteUser(String id);
}
