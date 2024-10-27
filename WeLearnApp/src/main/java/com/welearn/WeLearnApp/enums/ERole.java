package com.welearn.WeLearnApp.enums;

import lombok.Getter;

@Getter
public enum ERole {
    ADMIN("ADMIN"),
    USER("USER"),
    TUTOR("TUTOR"),
    ;

    final String name;

    ERole(String name) {
        this.name = name;
    }
}
