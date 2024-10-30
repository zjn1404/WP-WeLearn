package com.welearn.WeLearnApp.exception;

import com.welearn.WeLearnApp.dto.response.ApiResponse;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;

@Slf4j
@ControllerAdvice
public class GlobalExceptionHandler {

    @ExceptionHandler(Exception.class)
    public ResponseEntity<ApiResponse<Void>> handleException(Exception e) {
        ErrorCode errorCode = ErrorCode.UNCATEGORIZED_EXCEPTION;

        log.info(e.getMessage());

        return ResponseEntity.status(errorCode.getStatus())
                .body(ApiResponse.<Void>builder()
                        .code(ErrorCode.UNCATEGORIZED_EXCEPTION.getCode())
                        .message(ErrorCode.UNCATEGORIZED_EXCEPTION.getMessage())
                        .build());
    }

    @ExceptionHandler(AppException.class)
    public ResponseEntity<ApiResponse<Void>> handleAppException(AppException e) {
        return ResponseEntity.status(e.getErrorCode().getStatus())
                .body(ApiResponse.<Void>builder()
                        .code(e.getErrorCode().getCode())
                        .message(e.getMessage())
                        .build());
    }

    @ExceptionHandler(NotVerifiedException.class)
    public ResponseEntity<ApiResponse<String>> handleNotVerifiedException(NotVerifiedException e) {
        return ResponseEntity.status(e.getErrorCode().getStatus())
                .body(ApiResponse.<String>builder()
                        .code(e.getErrorCode().getCode())
                        .message(e.getMessage())
                        .data(e.getUserId())
                        .build());
    }

    @ExceptionHandler(MethodArgumentNotValidException.class)
    public ResponseEntity<ApiResponse<Void>> handleMethodArgumentNotValidException(MethodArgumentNotValidException e) {
        ErrorCode errorCode = ErrorCode.INVALID_KEY;

        try {
            String errorMsg = e.getFieldError().getDefaultMessage();
            errorCode = ErrorCode.valueOf(errorMsg);

        } catch (Exception ignored) {}

        return ResponseEntity.status(errorCode.getStatus())
                .body(ApiResponse.<Void>builder()
                        .code(errorCode.getCode())
                        .message(errorCode.getMessage())
                        .build());
    }

}
