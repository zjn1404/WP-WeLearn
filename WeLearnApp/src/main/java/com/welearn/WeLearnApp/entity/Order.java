package com.welearn.WeLearnApp.entity;

import jakarta.persistence.*;
import lombok.*;
import lombok.experimental.FieldDefaults;

import java.time.LocalDateTime;

@Entity
@Table(name = "`order`")
@Getter
@Setter
@Builder
@AllArgsConstructor
@NoArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class Order {
    @Id
    @Column(name = "id")
    String id;

    @Column(name = "order_time")
    LocalDateTime orderTime;

    @OneToOne(fetch = FetchType.LAZY, cascade = {
            CascadeType.DETACH,
            CascadeType.MERGE,
            CascadeType.PERSIST,
            CascadeType.REFRESH
    })
    UserProfile student;

    @OneToOne(fetch = FetchType.LAZY, cascade = {
            CascadeType.DETACH,
            CascadeType.MERGE,
            CascadeType.PERSIST,
            CascadeType.REFRESH
    })
    UserProfile tutor;

    @OneToOne(mappedBy = "order", cascade = CascadeType.ALL)
    @PrimaryKeyJoinColumn
    OrderDetail orderDetail;
}
