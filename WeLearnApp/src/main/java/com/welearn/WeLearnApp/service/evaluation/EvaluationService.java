package com.welearn.WeLearnApp.service.evaluation;

import com.welearn.WeLearnApp.dto.request.evaluation.EvaluationRequest;
import com.welearn.WeLearnApp.dto.response.EvaluationResponse;
import com.welearn.WeLearnApp.dto.response.PageResponse;

public interface EvaluationService {
    EvaluationResponse evaluateTutor(EvaluationRequest request);

    PageResponse<EvaluationResponse> getAllEvaluations(int page, int size);

    PageResponse<EvaluationResponse> getEvaluationsByTutorId(String tutorId, int page, int size);
}
