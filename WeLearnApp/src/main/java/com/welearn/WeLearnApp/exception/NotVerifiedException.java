package com.welearn.WeLearnApp.exception;

import lombok.*;
import lombok.experimental.FieldDefaults;

@Getter
@Setter
@FieldDefaults(level = AccessLevel.PRIVATE)
public class NotVerifiedException extends AppException{
    String userId;

    public NotVerifiedException(String userId) {
        super(ErrorCode.NOT_VERIFIED);
        this.userId = userId;
    }
}
