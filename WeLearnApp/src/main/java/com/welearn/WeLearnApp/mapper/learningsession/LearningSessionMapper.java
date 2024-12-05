package com.welearn.WeLearnApp.mapper.learningsession;

import com.welearn.WeLearnApp.dto.request.learningsession.LearningSessionCreationRequest;
import com.welearn.WeLearnApp.dto.response.LearningSessionResponse;
import com.welearn.WeLearnApp.entity.LearningSession;

public interface LearningSessionMapper {
    LearningSession toLearningSession(LearningSessionCreationRequest request);
    LearningSessionResponse toLearningSessionResponse(LearningSession learningSession);
}
