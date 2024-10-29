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

create table grade_tutor (
	grade_id int,
    tutor_id varchar(50),
    constraint fk_grade_tutor_grade foreign key(grade_id) references grade(id),
    constraint fk_grade_tutor_tutor foreign key(tutor_id) references tutor(id),
    constraint pk_grade_tutor primary key(grade_id, tutor_id)
);

create table user_profile (
	id varchar(50) primary key,
    first_name varchar(50),
    last_name varchar(50),
    dob date,
    location_id varchar(50),
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
    constraint fk_verification_code_user foreign key(user_id) references `user`(id)
);

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

create table learning_session (
	id varchar(50) primary key,
    tutor_id varchar(50),
    start_time date,
    duration long,
    grade_id int,
    subject_name varchar(50),
    learning_method_name varchar(50),
    constraint fk_learning_session_tutor foreign key(tutor_id) references tutor(id),
    constraint fk_learning_session_grade foreign key(grade_id) references grade(id),
    constraint fk_learning_session_subject foreign key(subject_name) references `subject`(`name`),
	constraint fk_learning_session_learning_method foreign key(learning_method_name) references learning_method(name)
);
