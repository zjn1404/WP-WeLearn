package com.welearn.WeLearnApp;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.scheduling.annotation.EnableScheduling;

@SpringBootApplication
@EnableScheduling
public class WeLearnAppApplication {

	public static void main(String[] args) {
		SpringApplication.run(WeLearnAppApplication.class, args);
	}

}
