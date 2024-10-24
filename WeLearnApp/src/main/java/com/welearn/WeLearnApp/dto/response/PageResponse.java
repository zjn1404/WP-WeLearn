package com.welearn.WeLearnApp.dto.response;

import lombok.*;
import lombok.experimental.FieldDefaults;

import java.util.List;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class PageResponse <T> {
    long currentPage;
    long elementPerPage;
    long totalPage;
    long totalElement;
    List<T> data;
}
