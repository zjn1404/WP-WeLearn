package com.welearn.WeLearnApp.service.evaluation;

import com.welearn.WeLearnApp.dto.request.evaluation.EvaluationRequest;
import com.welearn.WeLearnApp.dto.response.EvaluationResponse;
import com.welearn.WeLearnApp.dto.response.PageResponse;
import com.welearn.WeLearnApp.entity.Evaluation;
import com.welearn.WeLearnApp.entity.UserProfile;
import com.welearn.WeLearnApp.exception.AppException;
import com.welearn.WeLearnApp.exception.ErrorCode;
import com.welearn.WeLearnApp.mapper.evaluation.EvaluationMapper;
import com.welearn.WeLearnApp.repository.EvaluationRepository;
import com.welearn.WeLearnApp.repository.UserProfileRepository;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.security.core.Authentication;
import org.springframework.security.core.context.SecurityContextHolder;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class EvaluationServiceImp implements EvaluationService {

    EvaluationRepository evaluationRepository;
    UserProfileRepository userProfileRepository;

    EvaluationMapper evaluationMapper;

    @Override
    public EvaluationResponse evaluateTutor(EvaluationRequest request) {
        Authentication authentication = SecurityContextHolder.getContext().getAuthentication();

        UserProfile student = userProfileRepository.findById(authentication.getName())
                .orElseThrow(() -> new AppException(ErrorCode.USER_PROFILE_NOT_FOUND));

        UserProfile tutor = userProfileRepository.findById(request.getTutorId())
                .orElseThrow(() -> new AppException(ErrorCode.USER_PROFILE_NOT_FOUND));

        if (request.getStar() == null || request.getStar() <= 0) {
            throw new AppException(ErrorCode.INVALID_RATING);
        }

        if (request.getComment() == null || request.getComment().isEmpty()) {
            throw new AppException(ErrorCode.INVALID_COMMENT);
        }

        Evaluation evaluation = evaluationMapper.toEvaluation(request);
        evaluation.setStudent(student);
        evaluation.setTutor(tutor);

        return evaluationMapper.toEvaluationResponse(evaluationRepository.save(evaluation));
    }

    @Override
    public PageResponse<EvaluationResponse> getAllEvaluations(int page, int size) {
        Pageable pageable = PageRequest.of(page - 1, size);

        Page<Evaluation> evaluations = evaluationRepository.findAll(pageable);

        return buildPageEvaluationResponse(evaluations, page, size);
    }

    @Override
    public PageResponse<EvaluationResponse> getEvaluationsByTutorId(String tutorId, int page, int size) {
        UserProfile tutor = userProfileRepository.findById(tutorId)
                .orElseThrow(() -> new AppException(ErrorCode.TUTOR_NOT_FOUND));

        Pageable pageable = PageRequest.of(page - 1, size);

        Page<Evaluation> evaluations = evaluationRepository.findAllByTutor(tutor, pageable);

        return buildPageEvaluationResponse(evaluations, page, size);
    }

    private PageResponse<EvaluationResponse> buildPageEvaluationResponse(Page<Evaluation> evaluations, int page, int size) {
        List<EvaluationResponse> evaluationResponses = evaluations.getContent().stream()
                .map(evaluationMapper::toEvaluationResponse)
                .toList();

        return PageResponse.<EvaluationResponse>builder()
                .totalPage(evaluations.getTotalPages())
                .elementPerPage(size)
                .totalElement(evaluations.getTotalElements())
                .currentPage(page)
                .data(evaluationResponses)
                .build();
    }
}
