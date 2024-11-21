package com.welearn.WeLearnApp.controller;

import com.welearn.WeLearnApp.dto.request.evaluation.EvaluationRequest;
import com.welearn.WeLearnApp.dto.response.ApiResponse;
import com.welearn.WeLearnApp.dto.response.EvaluationResponse;
import com.welearn.WeLearnApp.dto.response.PageResponse;
import com.welearn.WeLearnApp.service.evaluation.EvaluationService;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/evaluate")
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class EvaluationController {

    EvaluationService evaluationService;

    @PostMapping
    public ApiResponse<EvaluationResponse> evaluateTutor(@RequestBody EvaluationRequest request) {
        return ApiResponse.<EvaluationResponse>builder()
                .data(evaluationService.evaluateTutor(request))
                .build();
    }

    @GetMapping
    public ApiResponse<PageResponse<EvaluationResponse>> getAllEvaluations(
            @RequestParam(value = "page", required = false, defaultValue = "1") int page,
            @RequestParam(value = "size", required = false, defaultValue = "5") int size
    ) {
        return ApiResponse.<PageResponse<EvaluationResponse>>builder()
                .data(evaluationService.getAllEvaluations(page, size))
                .build();
    }

    @GetMapping("/tutor")
    public ApiResponse<PageResponse<EvaluationResponse>> getAllEvaluationByTutorId(
            @RequestParam("tutorId") String tutorId,
            @RequestParam(value = "page", required = false, defaultValue = "1") int page,
            @RequestParam(value = "size", required = false, defaultValue = "5") int size
    ) {
        return ApiResponse.<PageResponse<EvaluationResponse>>builder()
                .data(evaluationService.getEvaluationsByTutorId(tutorId, page, size))
                .build();
    }
}
