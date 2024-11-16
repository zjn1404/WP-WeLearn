package com.welearn.WeLearnApp.service.learningsession;

import com.welearn.WeLearnApp.dto.request.learningsession.LearningSessionCreationRequest;
import com.welearn.WeLearnApp.dto.response.LearningSessionResponse;
import com.welearn.WeLearnApp.dto.response.PageResponse;
import com.welearn.WeLearnApp.entity.*;
import com.welearn.WeLearnApp.enums.ERole;
import com.welearn.WeLearnApp.exception.AppException;
import com.welearn.WeLearnApp.exception.ErrorCode;
import com.welearn.WeLearnApp.mapper.learningmethod.LearningSessionMapper;
import com.welearn.WeLearnApp.mapper.userprofile.UserProfileMapper;
import com.welearn.WeLearnApp.repository.*;
import lombok.*;
import lombok.experimental.FieldDefaults;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@Getter
@Setter
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class LearningSessionServiceImp implements LearningSessionService {

    LearningSessionRepository learningSessionRepository;
    GradeRepository gradeRepository;
    SubjectRepository subjectRepository;
    LearningMethodRepository learningMethodRepository;

    UserProfileRepository userProfileRepository;

    LearningSessionMapper learningSessionMapper;
    UserProfileMapper userProfileMapper;

    @Override
    public LearningSessionResponse createLearningSession(LearningSessionCreationRequest request) {

        if (learningSessionRepository.existsByStartTime(request.getStartTime())) {
            throw new AppException(ErrorCode.LEARNING_SESSION_ALREADY_EXIST);
        }

        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();

        if (authentication.getAuthorities().stream()
                .noneMatch(authority -> authority.getAuthority().equals(ERole.TUTOR.getName()))) {
            throw new AppException(ErrorCode.UNAUTHORIZED);
        }

        UserProfile tutor = userProfileRepository.findById(authentication.getName())
                .orElseThrow(() -> new AppException(ErrorCode.USER_PROFILE_NOT_FOUND));

        Grade grade = gradeRepository.findById(request.getGrade())
                .orElseThrow(() -> new AppException(ErrorCode.GRADE_NOT_FOUND));

        Subject subject = subjectRepository.findById(request.getSubject())
                .orElseThrow(() -> new AppException(ErrorCode.SUBJECT_NOT_FOUND));

        LearningMethod learningMethod = learningMethodRepository
                .findById(request.getLearningMethod())
                .orElseThrow(() -> new AppException(ErrorCode.LEARNING_METHOD_NOT_FOUND));

        LearningSession learningSession = learningSessionMapper.toLearningSession(request);
        learningSession.setTutor(tutor);
        learningSession.setSubject(subject);
        learningSession.setGrade(grade);
        learningSession.setLearningMethod(learningMethod);

        learningSessionRepository.save(learningSession);

        return buildLearningSessionResponse(learningSession);
    }

    @Override
    public PageResponse<LearningSessionResponse> getLearningSessions(int page, int size) {
        Pageable pageable = PageRequest.of(page - 1, size);
        Page<LearningSession> learningSessions = learningSessionRepository.findAll(pageable);
        List<LearningSessionResponse> responses = learningSessions.map(this::buildLearningSessionResponse).getContent();

        return PageResponse.<LearningSessionResponse>builder()
                .currentPage(page)
                .elementPerPage(size)
                .totalElement(learningSessions.getTotalElements())
                .totalPage(learningSessions.getTotalPages())
                .data(responses)
                .build();
    }

    @Override
    public LearningSessionResponse getLearningSession(String sessionId) {
        return learningSessionRepository.findById(sessionId)
                .map(this::buildLearningSessionResponse)
                .orElseThrow(() -> new AppException(ErrorCode.LEARNING_SESSION_NOT_FOUND));
    }

    @Override
    public void deleteLearningSession(String sessionId) {
        learningSessionRepository.deleteById(sessionId);
    }

    private LearningSessionResponse buildLearningSessionResponse(LearningSession learningSession) {
        LearningSessionResponse response = learningSessionMapper.toLearningSessionResponse(learningSession);
        response.setTutor(userProfileMapper.toUserProfileResponse(learningSession.getTutor()));
        return response;
    }
}
