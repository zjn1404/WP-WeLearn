package com.welearn.WeLearnApp.service.learningsession;

import com.welearn.WeLearnApp.dto.request.learningsession.LearningSessionCreationRequest;
import com.welearn.WeLearnApp.dto.response.LearningSessionResponse;
import com.welearn.WeLearnApp.dto.response.PageResponse;

import java.util.List;

public interface LearningSessionService {
    LearningSessionResponse createLearningSession(LearningSessionCreationRequest request);
    LearningSessionResponse getLearningSession(String sessionId);
    PageResponse<LearningSessionResponse> getLearningSessions(int page, int size);
    List<LearningSessionResponse> getMyLearningSessions();
    void deleteLearningSession(String sessionId);
}
