package com.welearn.WeLearnApp.configuration;

import com.fasterxml.jackson.databind.ObjectMapper;
import com.welearn.WeLearnApp.dto.response.ApiResponse;
import com.welearn.WeLearnApp.exception.ErrorCode;
import jakarta.servlet.FilterChain;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.MediaType;
import org.springframework.stereotype.Component;
import org.springframework.web.filter.OncePerRequestFilter;

import java.io.IOException;

@Component
@Slf4j
public class AfterBearerTokenAuthenticationExceptionHandler extends OncePerRequestFilter {

    @Override
    protected void doFilterInternal(HttpServletRequest request,
                                    HttpServletResponse response,
                                    FilterChain filterChain) throws IOException {
        try {
            filterChain.doFilter(request, response);
        } catch (Exception e) {
            ErrorCode errorCode = ErrorCode.UNAUTHENTICATED;
            response.setStatus(errorCode.getStatus().value());
            response.setContentType(MediaType.APPLICATION_JSON_VALUE);

            ApiResponse<?> apiResponse = ApiResponse.builder()
                    .code(errorCode.getCode())
                    .message(errorCode.getMessage())
                    .build();

            ObjectMapper objectMapper = new ObjectMapper();

            response.getWriter().write(objectMapper.writeValueAsString(apiResponse));

            response.getWriter().flush();
        }
    }
}
