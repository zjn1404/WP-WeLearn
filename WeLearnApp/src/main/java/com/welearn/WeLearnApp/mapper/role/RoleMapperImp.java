package com.welearn.WeLearnApp.mapper.role;

import com.welearn.WeLearnApp.dto.request.role.RoleCreationRequest;
import com.welearn.WeLearnApp.dto.response.RoleResponse;
import com.welearn.WeLearnApp.entity.Role;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import org.springframework.stereotype.Component;

@Component
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class RoleMapperImp implements RoleMapper{
    @Override
    public Role toRole(RoleCreationRequest request) {
        return Role.builder()
                .name(request.getName())
                .build();
    }

    @Override
    public RoleResponse toRoleResponse(Role role) {
        return RoleResponse.builder()
                .name(role.getName())
                .build();
    }
}
