package com.welearn.WeLearnApp.controller;

import com.welearn.WeLearnApp.dto.request.tutor.TutorUpdateRequest;
import com.welearn.WeLearnApp.dto.response.ApiResponse;
import com.welearn.WeLearnApp.dto.response.TutorResponse;
import com.welearn.WeLearnApp.service.tutor.TutorService;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/tutor")
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class TutorController {

        TutorService tutorService;

        @GetMapping("/{tutorId}")
        public ApiResponse<TutorResponse> getTutorInfoById(@PathVariable("tutorId") String tutorId) {
            return ApiResponse.<TutorResponse>builder()
                    .data(tutorService.getTutorInfoById(tutorId))
                    .build();
        }

        @PatchMapping
        public ApiResponse<TutorResponse> updateTutorInfo(@RequestBody TutorUpdateRequest request) {
            return ApiResponse.<TutorResponse>builder()
                    .data(tutorService.updateTutorInfo(request))
                    .build();
        }
}
