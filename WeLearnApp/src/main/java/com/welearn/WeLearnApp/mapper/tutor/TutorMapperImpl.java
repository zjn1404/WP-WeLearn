package com.welearn.WeLearnApp.mapper.tutor;

import com.welearn.WeLearnApp.dto.request.tutor.TutorUpdateRequest;
import com.welearn.WeLearnApp.dto.response.TutorResponse;
import com.welearn.WeLearnApp.entity.Tutor;
import org.springframework.stereotype.Component;

@Component
public class TutorMapperImpl implements TutorMapper {
    @Override
    public void updateTutor(Tutor tutor, TutorUpdateRequest request) {
        if (request.getDegree() != null) {
            tutor.setDegree(request.getDegree());
        }

        if (request.getDescription() != null) {
            tutor.setDescription(request.getDescription());
        }
    }

    @Override
    public TutorResponse toTutorResponse(Tutor tutor) {
        return TutorResponse.builder()
                .degree(tutor.getDegree())
                .description(tutor.getDescription())
                .build();
    }
}
