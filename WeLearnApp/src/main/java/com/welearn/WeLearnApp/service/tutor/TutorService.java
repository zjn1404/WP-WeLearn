package com.welearn.WeLearnApp.service.tutor;

import com.welearn.WeLearnApp.dto.request.tutor.TutorUpdateRequest;
import com.welearn.WeLearnApp.dto.response.TutorResponse;

public interface TutorService {
    TutorResponse getTutorInfoById(String tutorId);

    TutorResponse updateTutorInfo(TutorUpdateRequest request);
}
