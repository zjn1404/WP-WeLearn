package com.welearn.WeLearnApp.mapper.user;

import com.welearn.WeLearnApp.dto.request.user.UserCreationRequest;
import com.welearn.WeLearnApp.dto.request.user.UserUpdateRequest;
import com.welearn.WeLearnApp.dto.response.UserResponse;
import com.welearn.WeLearnApp.entity.User;

public interface UserMapper {
    User toUser(UserCreationRequest request);

    void updateUser(User user, UserUpdateRequest request);

    UserResponse toUserResponse(User user);
}
