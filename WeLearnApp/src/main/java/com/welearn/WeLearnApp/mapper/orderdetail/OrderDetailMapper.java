package com.welearn.WeLearnApp.mapper.orderdetail;

import com.welearn.WeLearnApp.dto.response.OrderDetailResponse;
import com.welearn.WeLearnApp.entity.OrderDetail;

public interface OrderDetailMapper {
    OrderDetailResponse toOrderDetailResponse(OrderDetail orderDetail);
}
