/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.up = async function (knex) {
  await knex.raw(`
    CREATE TABLE \`role\` (
      \`name\` varchar(50) PRIMARY KEY
    );`);

  await knex.raw(`
    CREATE TABLE \`user\` (
      id varchar(50) PRIMARY KEY,
      \`username\` varchar(50) UNIQUE,
      \`password\` varchar(150),
      email varchar(50) UNIQUE,
      role_name varchar(50),
      CONSTRAINT fk_user_role FOREIGN KEY(role_name) REFERENCES \`role\`(\`name\`)
    );
    `);

  await knex.raw(`
    CREATE TABLE location (
      id varchar(50) PRIMARY KEY,
      city varchar(50),
      district varchar(50),
      street varchar(50)
    );`);

  await knex.raw(` 
    CREATE TABLE tutor (
      id varchar(50) PRIMARY KEY,
      degree varchar(50),
      \`description\` TEXT,
      CONSTRAINT fk_tutor_user FOREIGN KEY(id) REFERENCES \`user\`(id)
    );`);

  await knex.raw(`
    CREATE TABLE grade (
      id INT PRIMARY KEY
    );`);

  await knex.raw(`
    CREATE TABLE grade_tutor (
      grade_id INT,
      tutor_id varchar(50),
      CONSTRAINT fk_grade_tutor_grade FOREIGN KEY(grade_id) REFERENCES grade(id),
      CONSTRAINT fk_grade_tutor_tutor FOREIGN KEY(tutor_id) REFERENCES tutor(id),
      CONSTRAINT pk_grade_tutor PRIMARY KEY(grade_id, tutor_id)
    );`);

  await knex.raw(`
    CREATE TABLE user_profile (
      id varchar(50) PRIMARY KEY,
      first_name varchar(50),
      last_name varchar(50),
      dob DATE,
      phone_number char(10),
      location_id varchar(50),
      avatar_url varchar(250),
      CONSTRAINT fk_user_profile_location FOREIGN KEY(location_id) REFERENCES location(id),
      CONSTRAINT fk_user_profile_user FOREIGN KEY(id) REFERENCES \`user\`(id)
    );`);

  await knex.raw(`
    CREATE TABLE evaluation (
      id varchar(50) PRIMARY KEY,
      student_id varchar(50),
      tutor_id varchar(50),
      star INT,
      comment TEXT,
      CONSTRAINT fk_evaluation_student FOREIGN KEY(student_id) REFERENCES user_profile(id),
      CONSTRAINT fk_evaluation_tutor FOREIGN KEY(tutor_id) REFERENCES user_profile(id),
      CONSTRAINT check_star CHECK (star BETWEEN 1 AND 5)
    );`);

  await knex.raw(`
    CREATE TABLE verification_code (
      \`code\` varchar(50) PRIMARY KEY,
      user_id varchar(50),
      expiration_time DATETIME,
      CONSTRAINT fk_verification_code_user FOREIGN KEY(user_id) REFERENCES \`user\`(id)
    );`);

  await knex.raw(`
    CREATE TABLE \`invalidated_token\` (
      \`ac_id\` varchar(255) COLLATE utf8mb4_bin NOT NULL,
      \`rf_id\` varchar(255) COLLATE utf8mb4_bin DEFAULT NULL,
      \`expiration_time\` DATETIME DEFAULT NULL,
      PRIMARY KEY (\`ac_id\`)
    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_bin;`);

  await knex.raw(`
    CREATE TABLE \`order\` (
      id varchar(50) PRIMARY KEY,
      order_time DATETIME,
      student_id varchar(50),
      tutor_id varchar(50),
      CONSTRAINT fk_order_student FOREIGN KEY(student_id) REFERENCES user_profile(id),
      CONSTRAINT fk_order_tutor FOREIGN KEY(tutor_id) REFERENCES user_profile(id)
    );`);

  await knex.raw(`
    CREATE TABLE order_detail (
      order_id varchar(50),
      learning_session_id varchar(50),
      CONSTRAINT pk_order_detail PRIMARY KEY(order_id, learning_session_id),
      CONSTRAINT fk_order_detail_order FOREIGN KEY(order_id) REFERENCES \`order\`(id)
    );`);

  await knex.raw(`
    CREATE TABLE \`subject\` (
      \`name\` varchar(50) PRIMARY KEY
    );`);

  await knex.raw(`
    CREATE TABLE subject_tutor (
      subject_name varchar(50),
      tutor_id varchar(50),
      CONSTRAINT fk_subject_tutor_subject FOREIGN KEY(subject_name) REFERENCES \`subject\`(\`name\`),
      CONSTRAINT fk_subject_tutor_tutor FOREIGN KEY(tutor_id) REFERENCES tutor(id),
      CONSTRAINT pk_subject_tutor PRIMARY KEY(subject_name, tutor_id)
    );`);

  await knex.raw(`
    CREATE TABLE learning_method (
      \`name\` varchar(50) PRIMARY KEY
    );`);

  await knex.raw(`
    CREATE TABLE learning_method_tutor (
      learning_method_name varchar(50),
      tutor_id varchar(50),
      CONSTRAINT fk_learning_method_tutor_learning_method FOREIGN KEY(learning_method_name) REFERENCES learning_method(\`name\`),
      CONSTRAINT fk_learning_method_tutor_tutor FOREIGN KEY(tutor_id) REFERENCES tutor(id),
      CONSTRAINT pk_learning_method_tutor PRIMARY KEY(learning_method_name, tutor_id)
    );`);

  await knex.raw(`
    CREATE TABLE learning_session (
      id varchar(50) PRIMARY KEY,
      tutor_id varchar(50) NOT NULL,
      start_time DATE NOT NULL,
      duration BIGINT NOT NULL,
      grade_id INT NOT NULL,
      tuition DOUBLE NOT NULL,
      subject_name varchar(50),
      learning_method_name varchar(50),
      CONSTRAINT fk_learning_session_user_profile FOREIGN KEY(tutor_id) REFERENCES user_profile(id),
      CONSTRAINT fk_learning_session_grade FOREIGN KEY(grade_id) REFERENCES grade(id),
      CONSTRAINT fk_learning_session_subject FOREIGN KEY(subject_name) REFERENCES \`subject\`(\`name\`),
      CONSTRAINT fk_learning_session_learning_method FOREIGN KEY(learning_method_name) REFERENCES learning_method(\`name\`)
    );
  `);
};

/**
 * @param { import("knex").Knex } knex
 * @returns { Promise<void> }
 */
exports.down = async function (knex) {
  await knex.raw(`
    DROP TABLE IF EXISTS learning_session;
    DROP TABLE IF EXISTS learning_method_tutor;
    DROP TABLE IF EXISTS learning_method;
    DROP TABLE IF EXISTS subject_tutor;
    DROP TABLE IF EXISTS \`subject\`;
    DROP TABLE IF EXISTS order_detail;
    DROP TABLE IF EXISTS \`order\`;
    DROP TABLE IF EXISTS invalidated_token;
    DROP TABLE IF EXISTS verification_code;
    DROP TABLE IF EXISTS evaluation;
    DROP TABLE IF EXISTS user_profile;
    DROP TABLE IF EXISTS grade_tutor;
    DROP TABLE IF EXISTS grade;
    DROP TABLE IF EXISTS tutor;
    DROP TABLE IF EXISTS location;
    DROP TABLE IF EXISTS \`user\`;
    DROP TABLE IF EXISTS \`role\`;
  `);
};
