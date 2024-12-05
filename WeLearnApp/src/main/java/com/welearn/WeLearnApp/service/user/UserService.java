package com.welearn.WeLearnApp.service.user;

import com.nimbusds.jose.JOSEException;
import com.welearn.WeLearnApp.dto.request.user.UserCreationRequest;
import com.welearn.WeLearnApp.dto.request.user.UserUpdateRequest;
import com.welearn.WeLearnApp.dto.request.userprofile.UpdateUnverifiedEmailRequest;
import com.welearn.WeLearnApp.dto.response.UpdateUnverifiedEmailInvitationResponse;
import com.welearn.WeLearnApp.dto.response.UserResponse;

import java.text.ParseException;

public interface UserService {
    UserResponse createUser(UserCreationRequest request);

    UserResponse getUserById(String id);

    UserResponse getMyAccount();

    UserResponse updateUser(String id, UserUpdateRequest request);

    UpdateUnverifiedEmailInvitationResponse acceptUpdateUnverifiedEmailInvitation(String userId);

    void updateUnverifiedEmail(UpdateUnverifiedEmailRequest request) throws JOSEException, ParseException;

    void deleteUser(String id);
}
