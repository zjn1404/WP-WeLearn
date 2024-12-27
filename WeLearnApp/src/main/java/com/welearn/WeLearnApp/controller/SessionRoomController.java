package com.welearn.WeLearnApp.controller;

import com.welearn.WeLearnApp.dto.response.ApiResponse;
import com.welearn.WeLearnApp.entity.UserProfile;
import com.welearn.WeLearnApp.exception.AppException;
import com.welearn.WeLearnApp.exception.ErrorCode;
import com.welearn.WeLearnApp.repository.UserProfileRepository;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.experimental.NonFinal;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;
import org.yaml.snakeyaml.util.UriEncoder;

import java.util.UUID;

@Slf4j
@RestController
@RequestMapping("/session-room")
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class SessionRoomController {

    UserProfileRepository userProfileRepository;

    @GetMapping("/{id}")
    public ApiResponse<String> joinLearningSession(@PathVariable("id") String id) {
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();
        UserProfile userProfile = userProfileRepository.findById(authentication.getName())
                .orElseThrow(() -> new AppException(ErrorCode.USER_PROFILE_NOT_FOUND));

        String username = UriEncoder.encode(userProfile.getFirstName() + " " + userProfile.getLastName());


        String redirectUrl = String.format("http://localhost:8080/api/chat-room.html?roomID=%s&username=%s&userID=%s",
                id, username, userProfile.getId());

        return ApiResponse.<String>builder()
                .data(redirectUrl)
                .build();
    }
}