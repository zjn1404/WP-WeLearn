package com.welearn.WeLearnApp.mapper.evaluation;

import com.welearn.WeLearnApp.dto.request.evaluation.EvaluationRequest;
import com.welearn.WeLearnApp.dto.response.EvaluationResponse;
import com.welearn.WeLearnApp.entity.Evaluation;

public interface EvaluationMapper {
    Evaluation toEvaluation(EvaluationRequest request);
    EvaluationResponse toEvaluationResponse(Evaluation evaluation);
}
