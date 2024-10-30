package com.welearn.WeLearnApp.controller;

import com.welearn.WeLearnApp.dto.request.user.UserCreationRequest;
import com.welearn.WeLearnApp.dto.request.user.UserUpdateRequest;
import com.welearn.WeLearnApp.dto.response.ApiResponse;
import com.welearn.WeLearnApp.dto.response.UserResponse;
import com.welearn.WeLearnApp.service.user.UserService;
import jakarta.validation.Valid;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.extern.slf4j.Slf4j;
import org.springframework.web.bind.annotation.*;

@Slf4j
@RestController
@RequestMapping("/user")
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class UserController {

    UserService userService;

    @PostMapping
    public ApiResponse<UserResponse> createUser(@RequestBody @Valid UserCreationRequest request) {
        log.info(request.toString());
        return ApiResponse.<UserResponse>builder()
                .data(userService.createUser(request))
                .build();
    }

    @GetMapping("/{id}")
    public ApiResponse<UserResponse> getUserById(@PathVariable String id) {
        return ApiResponse.<UserResponse>builder()
                .data(userService.getUserById(id))
                .build();
    }

    @GetMapping("/me")
    public ApiResponse<UserResponse> getMyAccount() {
        return ApiResponse.<UserResponse>builder()
                .data(userService.getMyAccount())
                .build();
    }

    @PatchMapping("/{id}")
    public ApiResponse<UserResponse> updateUser(@PathVariable String id, @RequestBody UserUpdateRequest request) {
        return ApiResponse.<UserResponse>builder()
                .data(userService.updateUser(id, request))
                .build();
    }

    @DeleteMapping("/{id}")
    void deleteUser(@PathVariable String id) {
        userService.deleteUser(id);
    }
}
