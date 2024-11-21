package com.welearn.WeLearnApp.service.tutor;

import com.welearn.WeLearnApp.dto.request.tutor.TutorUpdateRequest;
import com.welearn.WeLearnApp.dto.response.TutorResponse;
import com.welearn.WeLearnApp.entity.Tutor;
import com.welearn.WeLearnApp.enums.ERole;
import com.welearn.WeLearnApp.exception.AppException;
import com.welearn.WeLearnApp.exception.ErrorCode;
import com.welearn.WeLearnApp.mapper.tutor.TutorMapper;
import com.welearn.WeLearnApp.repository.TutorRepository;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.extern.slf4j.Slf4j;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Service;

@Slf4j
@Service
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class TutorServiceImpl implements TutorService {

    TutorRepository tutorRepository;

    TutorMapper tutorMapper;

    @Override
    public TutorResponse getTutorInfoById(String tutorId) {
        Tutor tutor = tutorRepository.findById(tutorId).orElseThrow(() -> new AppException(ErrorCode.TUTOR_NOT_FOUND));

        return tutorMapper.toTutorResponse(tutor);
    }

    @Override
    public TutorResponse updateTutorInfo(TutorUpdateRequest request) {
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();

        if (authentication.getAuthorities().stream()
                .noneMatch(grantedAuthority -> grantedAuthority.getAuthority().equals(ERole.TUTOR.getName()))) {
            throw new AppException(ErrorCode.UNAUTHORIZED);
        }

        String tutorId = authentication.getName();

        Tutor tutor = tutorRepository.findById(tutorId).orElse(null);

        if (tutor == null) {
            tutor = tutorMapper.toTutor(request);
            tutor.setId(tutorId);
            tutorRepository.save(tutor);
        } else {
            tutorMapper.updateTutor(tutor, request);
        }

        return tutorMapper.toTutorResponse(tutor);

    }
}
