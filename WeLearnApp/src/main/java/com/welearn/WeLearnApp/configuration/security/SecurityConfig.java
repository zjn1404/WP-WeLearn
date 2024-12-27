package com.welearn.WeLearnApp.configuration.security;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.http.HttpMethod;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configurers.AbstractHttpConfigurer;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.security.oauth2.server.resource.authentication.JwtAuthenticationConverter;
import org.springframework.security.oauth2.server.resource.authentication.JwtGrantedAuthoritiesConverter;
import org.springframework.security.web.SecurityFilterChain;
import org.springframework.security.web.authentication.logout.LogoutFilter;
import org.springframework.web.cors.CorsConfiguration;
import org.springframework.web.cors.UrlBasedCorsConfigurationSource;
import org.springframework.web.filter.CorsFilter;

@Configuration
public class SecurityConfig {

    private static final String[] PUBLIC_GET_ENDPOINTS = {
            "/auth/refresh",
            "/user/unverified-email-invitation",
            "/payment/vnp/callback",
            "/subject/all",
            "/script.js",
            "/chat-room.html/**",
            "/session-room/**"
    };

    private static final String[] PUBLIC_POST_ENDPOINTS = {
            "/user",
            "/auth/authenticate",
            "/auth/logout",
            "/verification-code/**",
    };

    private static final String[] PUBLIC_PATCH_ENDPOINTS = {
            "/user/unverified-email",
    };

    @Bean
    public SecurityFilterChain securityFilterChain(HttpSecurity http,
                                                   CustomJwtDecoder decoder,
                                                    AfterBearerTokenAuthenticationExceptionHandler exceptionHandler
    ) throws Exception {

        http.addFilterBefore(exceptionHandler, LogoutFilter.class);

        http.authorizeHttpRequests(config -> config.requestMatchers(PUBLIC_GET_ENDPOINTS)
                .permitAll()
                .requestMatchers(PUBLIC_POST_ENDPOINTS)
                .permitAll()
                .requestMatchers(PUBLIC_PATCH_ENDPOINTS)
                .permitAll()
                .requestMatchers(HttpMethod.OPTIONS, "/**")
                .permitAll()
                .anyRequest()
                .authenticated());

        http.oauth2ResourceServer(config -> {
            config.jwt(jwt -> {jwt
                    .jwtAuthenticationConverter(jwtAuthenticationConverter())
                    .decoder(decoder);
            }).authenticationEntryPoint(new JwtAuthenticationEntrypoint());
        });

        http.csrf(AbstractHttpConfigurer::disable);

        return http.build();
    }

    @Bean
    public CorsFilter corsFilter() {
        CorsConfiguration config = new CorsConfiguration();

        config.addAllowedOrigin("http://localhost:3000");
        config.addAllowedMethod("*");
        config.addAllowedHeader("*");

        UrlBasedCorsConfigurationSource source = new UrlBasedCorsConfigurationSource();
        source.registerCorsConfiguration("/**", config);

        return new CorsFilter(source);
    }

    @Bean
    public JwtAuthenticationConverter jwtAuthenticationConverter() {
        JwtGrantedAuthoritiesConverter converter = new JwtGrantedAuthoritiesConverter();
        converter.setAuthorityPrefix("");

        JwtAuthenticationConverter jwtConverter = new JwtAuthenticationConverter();
        jwtConverter.setJwtGrantedAuthoritiesConverter(converter);

        return jwtConverter;
    }

    @Bean
    public PasswordEncoder passwordEncoder() {
        return new BCryptPasswordEncoder(12);
    }
}
