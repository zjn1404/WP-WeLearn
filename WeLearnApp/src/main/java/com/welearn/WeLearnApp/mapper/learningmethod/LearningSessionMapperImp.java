package com.welearn.WeLearnApp.mapper.learningmethod;

import com.welearn.WeLearnApp.dto.request.learningsession.LearningSessionCreationRequest;
import com.welearn.WeLearnApp.dto.response.LearningSessionResponse;
import com.welearn.WeLearnApp.entity.LearningSession;
import org.springframework.stereotype.Component;

@Component
public class LearningSessionMapperImp implements LearningSessionMapper {
    @Override
    public LearningSession toLearningSession(LearningSessionCreationRequest request) {
        return LearningSession.builder()
                .startTime(request.getStartTime())
                .duration(request.getDuration())
                .tuition(request.getTuition())
                .build();
    }

    @Override
    public LearningSessionResponse toLearningSessionResponse(LearningSession learningSession) {
        return LearningSessionResponse.builder()
                .id(learningSession.getId())
                .startTime(learningSession.getStartTime())
                .duration(learningSession.getDuration())
                .grade(learningSession.getGrade().getId())
                .subject(learningSession.getSubject().getName())
                .learningMethod(learningSession.getLearningMethod().getName())
                .tuition(learningSession.getTuition())
                .build();
    }
}
