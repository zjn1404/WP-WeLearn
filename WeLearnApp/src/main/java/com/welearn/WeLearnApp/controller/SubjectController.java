package com.welearn.WeLearnApp.controller;

import com.welearn.WeLearnApp.dto.response.ApiResponse;
import com.welearn.WeLearnApp.dto.response.SubjectResponse;
import com.welearn.WeLearnApp.service.subject.SubjectService;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;

@RestController
@RequestMapping("/subject")
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class SubjectController {

    SubjectService subjectService;

    @GetMapping("/all")
    public ApiResponse<List<SubjectResponse>> getAllSubjects() {
        return ApiResponse.<List<SubjectResponse>>builder()
                .data(subjectService.getAllSubjects())
                .build();
    }

}
