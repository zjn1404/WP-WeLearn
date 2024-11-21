package com.welearn.WeLearnApp.mapper.evaluation;

import com.welearn.WeLearnApp.dto.request.evaluation.EvaluationRequest;
import com.welearn.WeLearnApp.dto.response.EvaluationResponse;
import com.welearn.WeLearnApp.entity.Evaluation;
import org.springframework.stereotype.Component;

@Component
public class EvaluationMapperImp implements EvaluationMapper {

    @Override
    public Evaluation toEvaluation(EvaluationRequest request) {
        return Evaluation.builder()
                .star(request.getStar())
                .comment(request.getComment())
                .build();
    }

    @Override
    public EvaluationResponse toEvaluationResponse(Evaluation evaluation) {
        return EvaluationResponse.builder()
                .star(evaluation.getStar())
                .comment(evaluation.getComment())
                .tutorId(evaluation.getTutor().getId())
                .studentId(evaluation.getStudent().getId())
                .studentName(String.format("%s %s", evaluation.getStudent().getFirstName(),
                        evaluation.getStudent().getLastName()))
                .tutorName(String.format("%s %s", evaluation.getTutor().getFirstName(),
                        evaluation.getTutor().getLastName()))
                .build();
    }
}
