package com.welearn.WeLearnApp.mapper.order;

import com.welearn.WeLearnApp.dto.request.order.OrderCreationRequest;
import com.welearn.WeLearnApp.dto.response.OrderDetailResponse;
import com.welearn.WeLearnApp.dto.response.OrderResponse;
import com.welearn.WeLearnApp.entity.Order;
import com.welearn.WeLearnApp.entity.UserProfile;
import com.welearn.WeLearnApp.mapper.orderdetail.OrderDetailMapper;
import com.welearn.WeLearnApp.mapper.tutor.TutorMapper;
import com.welearn.WeLearnApp.mapper.userprofile.UserProfileMapper;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import org.springframework.stereotype.Component;

@Component
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class OrderMapperImp implements OrderMapper {

    UserProfileMapper userProfileMapper;

    OrderDetailMapper orderDetailMapper;

    @Override
    public OrderResponse toOrderResponse(Order order) {
        return OrderResponse.builder()
                .id(order.getId())
                .orderTime(order.getOrderTime())
                .studentId(order.getStudent().getId())
                .tutor(userProfileMapper.toUserProfileResponse(order.getTutor()))
                .orderDetail(orderDetailMapper.toOrderDetailResponse(order.getOrderDetail()))
                .build();
    }
}
