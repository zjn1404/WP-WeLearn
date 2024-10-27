package com.welearn.WeLearnApp.enums;

import lombok.Getter;

@Getter
public enum Role {
    ADMIN("ADMIN"),
    USER("USER"),
    TUTOR("TUTOR"),
    ;

    final String name;

    Role(String name) {
        this.name = name;
    }
}
