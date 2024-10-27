package com.welearn.WeLearnApp.repository;

import com.welearn.WeLearnApp.entity.Location;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface LocationRepository extends JpaRepository<Location, String> {

    @Query("SELECT CASE WHEN COUNT(l) > 0 THEN TRUE ELSE FALSE END FROM Location l WHERE l.city = ?1 AND l.district = ?2 AND l.street = ?3")
    boolean existsByCityAndDistrictAndStreet(String city, String district, String street);

    @Query("SELECT l FROM Location l WHERE l.city = ?1 AND l.district = ?2 AND l.street = ?3")
    Optional<Location> findByCityAndDistrictAndStreet(String city, String district, String street);
}
