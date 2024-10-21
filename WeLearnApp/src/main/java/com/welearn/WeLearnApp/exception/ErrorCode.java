package com.welearn.WeLearnApp.exception;

import lombok.*;
import lombok.experimental.FieldDefaults;
import org.springframework.http.HttpStatus;

@Getter
@AllArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public enum ErrorCode {
    //    Internal Server Error Undefined	9999
    UNCATEGORIZED_EXCEPTION(9999, HttpStatus.INTERNAL_SERVER_ERROR, "Uncategorized Error"),
    //    Internal Server Error	Developer error 1xxx
    INVALID_KEY(1001, HttpStatus.INTERNAL_SERVER_ERROR, "Invalid Key"),


    ;

    final Integer code;
    final HttpStatus status;
    final String message;
}
