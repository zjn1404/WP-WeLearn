package com.welearn.WeLearnApp.mapper.orderdetail;

import com.welearn.WeLearnApp.dto.response.OrderDetailResponse;
import com.welearn.WeLearnApp.entity.OrderDetail;
import com.welearn.WeLearnApp.mapper.learningsession.LearningSessionMapper;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import org.springframework.stereotype.Component;

@Component
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class OrderDetailMapperImp implements OrderDetailMapper {

    LearningSessionMapper learningSessionMapper;

    @Override
    public OrderDetailResponse toOrderDetailResponse(OrderDetail orderDetail) {
        return OrderDetailResponse.builder()
                .learningSession(learningSessionMapper.toLearningSessionResponse(orderDetail.getLearningSession()))
                .build();
    }
}
