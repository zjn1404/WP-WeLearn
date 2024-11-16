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
    // User Input Error 2xxx
    INVALID_USERNAME(2001, HttpStatus.BAD_REQUEST, "Invalid username"),
    INVALID_PASSWORD(2002, HttpStatus.BAD_REQUEST, "Invalid password"),
    REQUIRED_EMAIL(2003, HttpStatus.BAD_REQUEST, "Email is required"),
    INVALID_VERIFICATION_CODE(2004, HttpStatus.BAD_REQUEST, "Invalid verification code"),
    INVALID_PHONE_NUMBER(2005, HttpStatus.BAD_REQUEST, "Phone number should have 10 digits"),
    TUTOR_NOT_MATCH(2006, HttpStatus.BAD_REQUEST, "Tutor not match"),
    INVALID_GRADE(2007, HttpStatus.BAD_REQUEST, "Grade should be between 1 and 12"),
    INVALID_TUITION(2008, HttpStatus.BAD_REQUEST, "Tuition should be greater than or equal to 0"),
    //    Existed Error 3xxx
    USER_EXISTED(3001, HttpStatus.BAD_REQUEST, "User existed"),
    USER_PROFILE_EXISTED(3002, HttpStatus.BAD_REQUEST, "User profile existed"),
    LEARNING_SESSION_ALREADY_EXIST(3003, HttpStatus.BAD_REQUEST, "Learning session already exist"),
    //    Not Found Error 4xxx
    USER_NOT_FOUND(4001, HttpStatus.NOT_FOUND, "User not found"),
    ROLE_NOT_FOUND(4002, HttpStatus.NOT_FOUND, "Role not found"),
    USER_PROFILE_NOT_FOUND(4003, HttpStatus.NOT_FOUND, "User profile not found"),
    LOCATION_NOT_FOUND(4004, HttpStatus.NOT_FOUND, "Location not found"),
    GRADE_NOT_FOUND(4005, HttpStatus.NOT_FOUND, "Grade not found"),
    SUBJECT_NOT_FOUND(4006, HttpStatus.NOT_FOUND, "Subject not found"),
    LEARNING_SESSION_NOT_FOUND(4007, HttpStatus.NOT_FOUND, "Learning session not found"),
    LEARNING_METHOD_NOT_FOUND(4008, HttpStatus.NOT_FOUND, "Learning method not found"),
    ORDER_NOT_FOUND(4009, HttpStatus.NOT_FOUND, "Order not found"),
    // Authentication Error 5xxx
    AUTHENTICATION_FAIL(5001, HttpStatus.UNAUTHORIZED, "Invalid username or password"),
    INVALID_TOKEN(5002, HttpStatus.UNAUTHORIZED, "Invalid token"),
    UNAUTHENTICATED(5003, HttpStatus.UNAUTHORIZED, "Unauthenticated"),
    NOT_VERIFIED(5004, HttpStatus.UNAUTHORIZED, "User not verified"),
    //    Authorization Error 6xxx
    UNAUTHORIZED(6001, HttpStatus.FORBIDDEN, "Unauthorized"),
    TUTOR_CANNOT_ORDER(6002, HttpStatus.FORBIDDEN, "Tutor cannot order"),
    ;

    final Integer code;
    final HttpStatus status;
    final String message;
}
