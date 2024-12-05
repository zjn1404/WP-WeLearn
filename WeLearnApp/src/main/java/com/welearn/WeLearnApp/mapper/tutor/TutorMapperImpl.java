package com.welearn.WeLearnApp.mapper.tutor;

import com.welearn.WeLearnApp.dto.request.tutor.TutorUpdateRequest;
import com.welearn.WeLearnApp.dto.response.TutorResponse;
import com.welearn.WeLearnApp.entity.Tutor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.stereotype.Component;

@Slf4j
@Component
public class TutorMapperImpl implements TutorMapper {
    @Override
    public Tutor toTutor(TutorUpdateRequest request) {
        return Tutor.builder()
                .degree(request.getDegree())
                .description(request.getDescription())
                .build();
    }

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
