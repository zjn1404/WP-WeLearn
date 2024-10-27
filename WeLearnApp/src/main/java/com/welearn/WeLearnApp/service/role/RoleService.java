package com.welearn.WeLearnApp.service.role;

import com.welearn.WeLearnApp.dto.request.role.RoleCreationRequest;
import com.welearn.WeLearnApp.dto.response.RoleResponse;

import java.util.List;

public interface RoleService {
    RoleResponse createRole(RoleCreationRequest request);

    List<RoleResponse> getAllRoles();

    void deleteRole(String roleName);
}
