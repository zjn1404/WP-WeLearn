CREATE DATABASE `welearn_db` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_bin */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `welearn_db`;

create table `role` (
	`name` varchar(50) primary key
);

create table `user` (
	id varchar(50) primary key,
    username varchar(50),
    `password` varchar(150),
    email varchar(50),
    role_name varchar(50),
    constraint fk_user_role foreign key(role_name) references `role`(`name`)
);

create table location (
	id varchar(50) primary key,
    city varchar(50),
    district varchar(50),
    street varchar(50)
);

create table tutor (
	id varchar(50) primary key,
    degree varchar(50),
    `description` text,
    constraint fk_tutor_user foreign key(id) references `user`(id)
);

create table grade (
	id int primary key
);

-- create table grade_tutor (
-- 	grade_id int,
--     tutor_id varchar(50),
--     constraint fk_grade_tutor_grade foreign key(grade_id) references grade(id),
--     constraint fk_grade_tutor_tutor foreign key(tutor_id) references tutor(id),
--     constraint pk_grade_tutor primary key(grade_id, tutor_id)
-- );

create table user_profile (
	id varchar(50) primary key,
    first_name varchar(50),
    last_name varchar(50),
    dob date,
    phone_number char(10),
    location_id varchar(50),
    avatar_url varchar(250),
    constraint fk_user_profile_location foreign key(location_id) references location(id),
    constraint fk_user_profile_user foreign key(id) references `user`(id)
);

create table evaluation (
	id varchar(50) primary key,
    student_id varchar(50),
    tutor_id varchar(50),
    star int,
    comment text,
    constraint fk_evaluation_student foreign key(student_id) references user_profile(id),
	constraint fk_evaluation_tutor foreign key(tutor_id) references user_profile(id)
);

ALTER TABLE evaluation 
ADD CONSTRAINT check_star 
CHECK (star BETWEEN 1 AND 5);

create table verification_code (
	`code` varchar(50) primary key,
    user_id varchar(50),
    expiration_time datetime,
    constraint fk_verification_code_user foreign key(user_id) references `user`(id)
);

CREATE TABLE `invalidated_token` (
                                     `ac_id` varchar(255) COLLATE utf8mb4_bin NOT NULL,
                                     `rf_id` varchar(255) COLLATE utf8mb4_bin DEFAULT NULL,
                                     `expiration_time` datetime DEFAULT NULL,
                                     PRIMARY KEY (`ac_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

create table `order` (
	id varchar(50) primary key,
    order_time datetime,
    student_id varchar(50),
    tutor_id varchar(50),
    constraint fk_order_student foreign key(student_id) references user_profile(id),
	constraint fk_order_tutor foreign key(tutor_id) references user_profile(id)
);

create table order_detail (
	order_id varchar(50),
    learning_session_id varchar(50),
    constraint pk_order_detail primary key(order_id, learning_session_id),
    constraint fk_order_detail_order foreign key(order_id) references `order`(id)
);

create table `subject` (
	`name` varchar(50) primary key
);

create table subject_tutor (
	subject_name varchar(50),
    tutor_id varchar(50),
    constraint fk_subject_tutor_subject foreign key(subject_name) references `subject`(`name`),
    constraint fk_subject_tutor_tutor foreign key(tutor_id) references tutor(id),
    constraint pk_subject_tutor primary key(subject_name, tutor_id)
);

create table learning_method (
	`name` varchar(50) primary key
);

create table learning_method_tutor (
	learning_method_name varchar(50),
    tutor_id varchar(50),
    constraint fk_learning_method_tutor_learning_method foreign key(learning_method_name) references learning_method(`name`),
    constraint fk_learning_method_tutor_tutor foreign key(tutor_id) references tutor(id),
    constraint pk_learning_method_tutor primary key(learning_method_name, tutor_id)
);

CREATE TABLE `learning_session` (
  `id` varchar(50) COLLATE utf8mb4_bin NOT NULL,
  `tutor_id` varchar(50) COLLATE utf8mb4_bin NOT NULL,
  `start_time` date NOT NULL,
  `duration` mediumtext COLLATE utf8mb4_bin NOT NULL,
  `grade_id` double NOT NULL,
  `subject_name` varchar(50) COLLATE utf8mb4_bin NOT NULL,
  `learning_method_name` varchar(50) COLLATE utf8mb4_bin NOT NULL,
  `tuition` double NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_learning_session_grade` (`grade_id`),
  KEY `fk_learning_session_learning_method` (`learning_method_name`),
  KEY `fk_learning_session_subject` (`subject_name`),
  KEY `fk_learning_session_user_profile` (`tutor_id`),
  CONSTRAINT `fk_learning_session_learning_method` FOREIGN KEY (`learning_method_name`) REFERENCES `learning_method` (`name`),
  CONSTRAINT `fk_learning_session_subject` FOREIGN KEY (`subject_name`) REFERENCES `subject` (`name`),
  CONSTRAINT `fk_learning_session_user_profile` FOREIGN KEY (`tutor_id`) REFERENCES `user_profile` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;

