package com.welearn.WeLearnApp.mapper.role;

import com.welearn.WeLearnApp.dto.request.role.RoleCreationRequest;
import com.welearn.WeLearnApp.dto.response.RoleResponse;
import com.welearn.WeLearnApp.entity.Role;

public interface RoleMapper {
    Role toRole(RoleCreationRequest request);

    RoleResponse toRoleResponse(Role role);
}
