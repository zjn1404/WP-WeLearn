package com.welearn.WeLearnApp.mapper.order;

import com.welearn.WeLearnApp.dto.response.OrderResponse;
import com.welearn.WeLearnApp.entity.Order;

public interface OrderMapper {

    OrderResponse toOrderResponse(Order order);

}
