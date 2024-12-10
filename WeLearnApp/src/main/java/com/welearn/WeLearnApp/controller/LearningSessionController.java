package com.welearn.WeLearnApp.controller;

import com.welearn.WeLearnApp.dto.request.learningsession.LearningSessionCreationRequest;
import com.welearn.WeLearnApp.dto.response.ApiResponse;
import com.welearn.WeLearnApp.dto.response.LearningSessionResponse;
import com.welearn.WeLearnApp.dto.response.PageResponse;
import com.welearn.WeLearnApp.service.learningsession.LearningSessionService;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.experimental.NonFinal;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/learning-session")
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class LearningSessionController {

    @NonFinal
    @Value("${message.controller.learning-session.delete-success}")
    String DELETE_SUCCESS_MESSAGE;

    LearningSessionService learningSessionService;

    @PostMapping
    public ApiResponse<LearningSessionResponse> createLearningSession(@RequestBody LearningSessionCreationRequest request) {
        return ApiResponse.<LearningSessionResponse>builder()
                .data(learningSessionService.createLearningSession(request))
                .build();
    }

    @GetMapping("/{id}")
    public ApiResponse<LearningSessionResponse> getLearningSession(@PathVariable("id") String id) {
        return ApiResponse.<LearningSessionResponse>builder()
                .data(learningSessionService.getLearningSession(id))
                .build();
    }

    @GetMapping
    public ApiResponse<PageResponse<LearningSessionResponse>> getAllLearningSessions(@RequestParam("page") int page,
                                                                                    @RequestParam("size") int size) {
        return ApiResponse.<PageResponse<LearningSessionResponse>>builder()
                .data(learningSessionService.getLearningSessions(page, size))
                .build();
    }

    @GetMapping("/my-session")
    public ApiResponse<List<LearningSessionResponse>> getMyLearningSessions() {
        return ApiResponse.<List<LearningSessionResponse>>builder()
                .data(learningSessionService.getMyLearningSessions())
                .build();
    }

    @DeleteMapping("/{id}")
    public ApiResponse<Void> deleteLearningSession(@PathVariable("id") String id) {
        learningSessionService.deleteLearningSession(id);
        return ApiResponse.<Void>builder()
                .message(DELETE_SUCCESS_MESSAGE)
                .build();
    }
}
