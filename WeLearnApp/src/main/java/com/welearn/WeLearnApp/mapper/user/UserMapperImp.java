package com.welearn.WeLearnApp.mapper.user;

import com.welearn.WeLearnApp.dto.request.user.UserCreationRequest;
import com.welearn.WeLearnApp.dto.request.user.UserUpdateRequest;
import com.welearn.WeLearnApp.dto.response.UserResponse;
import com.welearn.WeLearnApp.entity.User;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Component;

@Component
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class UserMapperImp implements UserMapper{

    PasswordEncoder passwordEncoder;

    @Override
    public User toUser(UserCreationRequest request) {
        return User.builder()
                .username(request.getUsername())
                .password(passwordEncoder.encode(request.getPassword()))
                .email(request.getEmail())
                .build();
    }

    @Override
    public void updateUser(User user, UserUpdateRequest request) {
        if (request.getEmail() != null) {
            user.setEmail(request.getEmail());
        }
    }

    @Override
    public UserResponse toUserResponse(User user) {
        return UserResponse.builder()
                .id(user.getId())
                .username(user.getUsername())
                .email(user.getEmail())
                .build();
    }
}
