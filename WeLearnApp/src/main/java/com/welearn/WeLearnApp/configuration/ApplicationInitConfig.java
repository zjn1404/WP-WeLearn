package com.welearn.WeLearnApp.configuration;

import com.welearn.WeLearnApp.entity.Role;
import com.welearn.WeLearnApp.entity.User;
import com.welearn.WeLearnApp.enums.ERole;
import com.welearn.WeLearnApp.repository.RoleRepository;
import com.welearn.WeLearnApp.repository.UserRepository;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.experimental.NonFinal;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.boot.ApplicationRunner;
import org.springframework.boot.autoconfigure.condition.ConditionalOnProperty;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.crypto.password.PasswordEncoder;

@Configuration
@RequiredArgsConstructor
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class ApplicationInitConfig {

    @Value("${application-init.admin.username}")
    @NonFinal
    String ADMIN_USERNAME;

    @Value("${application-init.admin.password}")
    @NonFinal
    String ADMIN_PASSWORD;

    @Value("${application-init.admin.email}")
    @NonFinal
    String ADMIN_EMAIL;

    UserRepository userRepository;
    RoleRepository roleRepository;

    PasswordEncoder passwordEncoder;

    @Bean
    @ConditionalOnProperty(
            prefix = "spring",
            value = "datasource.driverClassName",
            havingValue = "com.mysql.cj.jdbc.Driver")
    public ApplicationRunner applicationRunner() {
        return args -> {
            if (!userRepository.existsByUsername(ADMIN_USERNAME)) {

                Role role = roleRepository.save(Role.builder().name(ERole.ADMIN.getName()).build());
                roleRepository.save(Role.builder().name(ERole.USER.getName()).build());
                roleRepository.save(Role.builder().name(ERole.TUTOR.getName()).build());

                userRepository.save(User.builder()
                        .username(ADMIN_USERNAME)
                        .password(passwordEncoder.encode(ADMIN_PASSWORD))
                        .email(ADMIN_EMAIL)
                        .role(role)
                        .build());
            }
        };
    }
}
