package com.welearn.WeLearnApp.utils;

import com.welearn.WeLearnApp.entity.User;

public interface EmailBuilder {
    String buildEmail(User user);
}
