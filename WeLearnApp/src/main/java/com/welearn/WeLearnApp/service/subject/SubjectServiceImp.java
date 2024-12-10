package com.welearn.WeLearnApp.service.subject;

import com.welearn.WeLearnApp.dto.response.SubjectResponse;
import com.welearn.WeLearnApp.repository.SubjectRepository;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class SubjectServiceImp implements SubjectService{

    SubjectRepository subjectRepository;

    @Override
    public List<SubjectResponse> getAllSubjects() {
        return subjectRepository.findAll().stream()
                .map(subject -> SubjectResponse.builder()
                        .name(subject.getName())
                        .build())
                .toList();
    }
}
