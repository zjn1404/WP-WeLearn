package com.welearn.WeLearnApp.service.tutor;

import com.welearn.WeLearnApp.dto.request.tutor.TutorUpdateRequest;
import com.welearn.WeLearnApp.dto.response.TutorResponse;
import com.welearn.WeLearnApp.entity.Tutor;
import com.welearn.WeLearnApp.mapper.tutor.TutorMapper;
import com.welearn.WeLearnApp.repository.TutorRepository;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class TutorServiceImpl implements TutorService {

    TutorRepository tutorRepository;

    TutorMapper tutorMapper;

    @Override
    public TutorResponse getTutorInfoById(String tutorId) {
        Tutor tutor = tutorRepository.findById(tutorId).orElse(null);

        return tutorMapper.toTutorResponse(tutor);
    }

    @Override
    public TutorResponse updateTutorInfo(String tutorId, TutorUpdateRequest request) {
        Tutor tutor = tutorRepository.findById(tutorId).orElse(null);

        tutorMapper.updateTutor(tutor, request);

        return tutorMapper.toTutorResponse(tutor);

    }
}
