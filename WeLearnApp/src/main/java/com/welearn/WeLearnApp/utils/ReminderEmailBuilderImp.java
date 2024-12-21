package com.welearn.WeLearnApp.utils;

import com.welearn.WeLearnApp.entity.User;
import org.springframework.stereotype.Component;

@Component("reminderEmailBuilder")
public class ReminderEmailBuilderImp implements EmailBuilder{
    @Override
    public String buildEmail(User user) {
        String mailContent = "<p>Dear " + user.getUsername() + ",</p>";
        mailContent += "<p>Your learning sessions will start within {{time}}</p>";
        mailContent += "<p> Please be prepared and ready to learn! <p>";
        mailContent += "<p> Thank you! <br> The WeLearn Team <p>";

        return mailContent;
    }
}
