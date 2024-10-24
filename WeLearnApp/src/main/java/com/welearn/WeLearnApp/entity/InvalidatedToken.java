package com.welearn.WeLearnApp.entity;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import lombok.*;
import lombok.experimental.FieldDefaults;

import java.time.LocalDateTime;

@Table(name = "invalidated_token")
@Entity
@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE)
public class InvalidatedToken {

    @Id
    @Column(name = "ac_id")
    String acId;

    @Column(name = "rf_id")
    String rfId;

    @Column(name = "expiration_time")
    LocalDateTime expirationTime;
}
