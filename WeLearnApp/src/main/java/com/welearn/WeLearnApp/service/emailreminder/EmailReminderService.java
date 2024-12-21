package com.welearn.WeLearnApp.service.emailreminder;

import com.welearn.WeLearnApp.dto.request.mail.MailRequest;
import com.welearn.WeLearnApp.dto.request.mail.mailparam.Recipient;
import com.welearn.WeLearnApp.dto.request.mail.mailparam.Sender;
import com.welearn.WeLearnApp.entity.Order;
import com.welearn.WeLearnApp.repository.OrderRepository;
import com.welearn.WeLearnApp.repository.httpclient.MailClient;
import com.welearn.WeLearnApp.utils.EmailBuilder;
import jakarta.transaction.Transactional;
import lombok.AccessLevel;
import lombok.RequiredArgsConstructor;
import lombok.experimental.FieldDefaults;
import lombok.experimental.NonFinal;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.beans.factory.annotation.Qualifier;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Service;

import java.time.Duration;
import java.time.Instant;
import java.time.LocalDateTime;
import java.time.temporal.ChronoUnit;
import java.util.List;

@Service
@FieldDefaults(level = AccessLevel.PRIVATE, makeFinal = true)
public class EmailReminderService {

    @NonFinal
    @Value("${mail.api-key}")
    String API_KEY;

    @NonFinal
    @Value("${mail.sender.email}")
    String SENDER_EMAIL;

    @NonFinal
    @Value("${mail.sender.name}")
    String SENDER_NAME;

    OrderRepository orderRepository;
    MailClient mailClient;

    @Qualifier("reminderEmailBuilder")
    EmailBuilder emailBuilder;

    @Autowired
    public EmailReminderService(OrderRepository orderRepository,
                                MailClient mailClient,
                                @Qualifier("reminderEmailBuilder") EmailBuilder emailBuilder) {
        this.orderRepository = orderRepository;
        this.mailClient = mailClient;
        this.emailBuilder = emailBuilder;
    }

    @Scheduled(fixedDelay = 600000) // ms
    @Transactional
    public void sendEmailReminder() {
        List<Order> upcomingLearningSessions = orderRepository.findAllUpcomingLearningSessions(LocalDateTime.now());
        Sender sender = Sender.builder()
                .name(SENDER_NAME)
                .email(SENDER_EMAIL)
                .build();

        for (Order order : upcomingLearningSessions) {
            long remainingMinutes = Duration.between(LocalDateTime.now(), order.getOrderDetail()
                    .getLearningSession().getStartTime()).toMinutes();

            String mailContent = emailBuilder.buildEmail(order.getStudent().getUser())
                    .replace("{{time}}", remainingMinutes + " minutes");
            Recipient recipient = Recipient.builder()
                    .userId(order.getStudent().getUser().getId())
                    .name(order.getStudent().getUser().getUsername())
                    .email(order.getStudent().getUser().getEmail())
                    .build();


            mailClient.sendEmail(API_KEY, MailRequest.builder()
                    .to(List.of(recipient))
                    .sender(sender)
                    .htmlContent(mailContent)
                    .subject("[WeLearn] Upcoming Learning Session")
                    .build());
        }
    }


}
