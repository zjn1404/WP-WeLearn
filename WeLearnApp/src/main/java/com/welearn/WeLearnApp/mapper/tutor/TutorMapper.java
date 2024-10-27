package com.welearn.WeLearnApp.mapper.tutor;

import com.welearn.WeLearnApp.dto.request.tutor.TutorUpdateRequest;
import com.welearn.WeLearnApp.dto.response.TutorResponse;
import com.welearn.WeLearnApp.entity.Tutor;

public interface TutorMapper {
    void updateTutor(Tutor tutor, TutorUpdateRequest request);

    TutorResponse toTutorResponse(Tutor tutor);
}
