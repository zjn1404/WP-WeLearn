# WP-WeLearn

An application that connects students and tutors, allowing students to search for and hire tutors by the hour. The classes offered range from grade 1 to grade 12, covering both natural sciences and social sciences in accordance with the curriculum standards of the Ministry of Education and Training of Vietnam.

## Table of Contents
- [Tech Stack](#tech-stack)
- [Milestone 1](#amilestone-1)
  - [Features](#i-features)
  - [UI/UX](#ii-uiux)
  - [Design Pattern / Architecture](#iii-design-pattern--architecture)
  - [Advanced topics](#iv-advanced-topics)
  - [Teamwork - Git flow](#v-teamwork---git-flow)
  - [Quality assurance](#vi-quality-assurance)
  - [Member Validation](#vii-member-evaluation)
- [Milestone 2](#b-milestone-2)
  - [Features](#i-features-1)
  - [UI/UX](#ii-uiux-1)
  - [Design Pattern / Architecture](#iii-design-pattern--architecture-1)
  - [Avanced topics](#iv-advanced-topics-1)
  - [Teamwork - Git flow](#v-teamwork---git-flow-1)
  - [Quality assurance](#vi-quality-assurance-1)
  - [Member Validation](#vii-member-evaluation-1)
- [Milestone 3](#c-milestone-3)
  - [Features](#i-features-2)
  - [UI/UX](#ii-uiux-2)
  - [Design Pattern / Architecture](#iii-design-pattern--architecture-2)
  - [Avanced topics](#iv-advanced-topics-2)
  - [Teamwork - Git flow](#v-teamwork---git-flow-2)
  - [Quality assurance](#vi-quality-assurance-2)
  - [Member Validation](#vii-member-evaluation-2)
- [Running Instruction](#c-running-instruction)

## Tech Stack
- **Languages**: C#, Java
- **Frameworks & Libraries**: WinUI, Spring Boot
- **Database**: MySQL
- **Tools**: Git, GitHub, Postman, Visual Studio, IntelliJ IDEA
- **API Document**: APIDog
# A.Milestone 1

## I. Features
- Registration
  - Registration Workflow
  ![registration-workflow](https://res.cloudinary.com/dlksshukq/image/upload/v1730888141/Milestone%201/kek7dvxck7as6monsvnc.png)
- Login
  - Login Workflow
  ![login-workflow](https://res.cloudinary.com/dlksshukq/image/upload/v1730888180/Milestone%201/n2xyljye2cszxltwjlxe.png)
- Logout
  - Logout Workflow
  ![logout-workflow](https://res.cloudinary.com/duf2t1pkp/image/upload/v1730891454/0cfb09b7-f947-413f-8aa4-b105ce700ea8.png)
- Database <br/>
  ![database](https://res.cloudinary.com/dlksshukq/image/upload/v1730896213/Milestone%201/zlvgdwzhhjw6jljym1bq.png)
- Send verification via email
- Verify account
- Update user profile
- Tutor Session Creation
## II. UI/UX
- Use WinUI 3 Gallery
- Error Notification
- Some images:
  - User Registration Page:
    ![user-registration-page](https://res.cloudinary.com/dlksshukq/image/upload/v1730885942/Milestone%201/skhyxblbaqo44ljleimm.png)
  - Tutor Registration Page:
    ![tutor-registration-page](https://res.cloudinary.com/dlksshukq/image/upload/v1730886111/Milestone%201/sfucvaj1l0ygemrdy7bf.png)
  - User Login Page:
    ![user-login-page](https://res.cloudinary.com/dlksshukq/image/upload/v1730885835/Milestone%201/tsvrcvp7za946tuyc82y.png)
  - Tutor Login Page:
    ![tutor-login-page](https://res.cloudinary.com/dlksshukq/image/upload/v1730886036/Milestone%201/zjbj2ke979rctx1nup4m.png)
  - Verification Page:
    ![verification-page](https://res.cloudinary.com/dlksshukq/image/upload/v1730894171/Milestone%201/hivvhussyvyead9azgxm.png)
  - Progress Spinner: <br />
    ![progress-spinner](https://res.cloudinary.com/dlksshukq/image/upload/v1730886419/Milestone%201/myhc3tf7rjurouxzmug6.png)
  - Displayed Tutor Page:
    ![displayed-tutor-page](https://res.cloudinary.com/dlksshukq/image/upload/v1730886690/Milestone%201/d1nsixdauyflte0d1kox.png)
  - Update Profile Page:
    ![profile-page](https://res.cloudinary.com/dlksshukq/image/upload/v1730889454/Milestone%201/qjzu6uwttiwr1rqh3xh0.png)
  - Session Creation Page:
    ![session-creation-page](https://res.cloudinary.com/dlksshukq/image/upload/v1730887355/Milestone%201/bt4lmjbyu5nstleedzar.png)
  - Session List Page:
    ![session-list-page](https://res.cloudinary.com/dlksshukq/image/upload/v1730889565/Milestone%201/w4inidsmwhm0bljsfn6c.png)
- Why we have 2 pages for logging in and registration
  - Role-Specific User Experience
  - Facilitates Future Role-Specific Features
## III. Design Pattern / Architecture
### 1. Architecture:
- 3-Layer:
  - Definition:
    - Presentation Layer: Handles user interface and interaction.
    - Business Logic Layer: Handles the core logic and rules of the application (service or domain layer).
    - Data Layer: Manages data access and storage (database).
  - Why we use it?
    - Scalability
    - Maintainability
    - Testing
- Client-Server:
  - Definition:
    - Client: interact with server and displaying the results to the user.
    - Server: interact with database, handle logics and return responses to clients.
  - Why we use it?
    - Scalability
    - Maintainability
    - Testing
    - Parallel Development
### 2. Design patterns:
- MVVM pattern:
  - Definition:
    - ViewModel: implements properties and commands to which the view can data bind to, and notifies the view of any state changes through change notification events.
    - Model: represents the app's domain model, which usually includes a data model along with business and validation logic.
    - View: defines the structure, layout, and appearance of what the user sees on screen.
  - Why we use it?
    - Easy Maintainability
    - Enhance Scalability Easily
- Singleton design pattern
  - Why we use it?
    - Efficient Use of Memory
    - Simplify Global Access
- Dependency injection
  - Why we use it?
    - Loose Module Dependency
    - Enhance Maintainability
## IV. Advanced topics
### 1. Client Server Architecture
- Develop a server to manage most of the resources and services to be consumed by the client.
- Client is responsible for interacting with the server and displaying information to users.
- Pros: 
  - Parallel development
  - Scalability
  - Improved Security
  - Support Multi-User Environments
- Cons:
  - Front-end Integration depends on server
  - Performance Bottlenecks
### 2. JWT Authentication
- JWT, which stands for JSON Web Token, is an open standard for securely sharing JSON data between parties.
- How it works?
  - Step 1: Client sends authentication request
  - Step 2: Server receives request and validates the provided information
  - Step 3: Server generates access token and refresh token and sends to client
- Pros: 
  - Stateless Authentication
  - Decentralized Authentication
  - Supports Token Expiration enhances security
- Cons:
  - Security Risks if Misconfigured
  - Exposed Token can cause security issues
### 3. Call API
- Use REST API 
### 4. Send Email
- Use Brevo as third party service to send email
- Why don't we create own sending mail service?
  - Reduce server's workload
  - Deliverability Challenges (Spam/Blacklist)
### 5. Upload Image to Cloud Storage on Client
- Pros:
  - Reduce server's workload
  - Reduce database storage's cost
- Cons:
  - Security issues
### 6. Progress Spinner
- Handle clicking spam problem on client
- Increase user's experience
### 7. Pass Data Between Pages
- Use Navigate to pass data between pages
- [Reference](https://learn.microsoft.com/en-us/windows/apps/design/basics/navigate-between-two-pages?tabs=wasdk&fbclid=IwY2xjawGYIBZleHRuA2FlbQIxMAABHUv_rX64M7iGCReAlfGzOEBHA2mGSfeRzsQpBfXSLCKOCF5aU3BSugicDA_aem_Mof7L_hYGH2L8e7eckZB-A#4-pass-information-between-pages)
### 8. Error Code Standard
| HTTP Status           | Error Side (Developer, Client, Undefined) | Error Code |
|-----------------------|-------------------------------------------|------------|
| Internal Server Error | Undefined                                 | 9999       |
| Internal Server Error | Developer                                 | 1xxx       |
| Bad Request           | Client                                    | 2xxx       |
| Unauthorized          | Client                                    | 3xxx       |
| Forbidden             | Client                                    | 4xxx       |
| Not Found             | Client                                    | 5xxx       |
## V. Teamwork - Git flow
- Use github Projects Tool for managing team
- Github username:
    - zjn1404: Nguyễn Quốc Tường
    - FATU29: Phan Tấn Phát
    - Shungisme: Vòng Sau Hùng
- Use Git feature branch workflow
  - Git Roadmap:
  ![git-roadmap](https://res.cloudinary.com/dlksshukq/image/upload/v1730888727/Milestone%201/odwgbnbs0lu6a2d9pqjw.png)
  - Git Contribution Tracking
  ![git-contribute](https://res.cloudinary.com/dlksshukq/image/upload/v1730889269/Milestone%201/embqv4g2u64ydrhlguaa.png)
  - Git Branching
  ![git-branching](https://res.cloudinary.com/dlksshukq/image/upload/v1730889369/Milestone%201/aym1n6fg3mspvbsfljeo.png)
- Use OneDrive for sharing documents
  ![one-drive](https://res.cloudinary.com/duf2t1pkp/image/upload/v1730895761/8571ea91-f7ed-4bbc-bb19-e32db1b74d97.png)
- Use Google Meet for planning
- Use Messenger for discussion
## VI. Quality assurance
- Manual testing for each feature
- Test on both server and client
- [Test Documentation](https://studenthcmusedu-my.sharepoint.com/:x:/g/personal/22120413_student_hcmus_edu_vn/EcE-oijGE4VDnLwWLsgeUPgBTezcQoKNgbB-F4P8m_w2kw?e=JqaGqE)

## VII. Member Evaluation

Below is the table evaluating team members based on the tasks assigned to them.

| Member Name       | Task                          | Completion Status | Contribution (%) |
|--------------------|-------------------------------|-------------------|------------------|
| Nguyễn Quốc Tường          | Create Database | Completed         | 100%             |
| Nguyễn Quốc Tường          | Implement register API | Completed         | 100%             |
| Nguyễn Quốc Tường          | Implement authentication API | Completed         | 100%             |
| Nguyễn Quốc Tường          | Implement send verification email API | Completed         | 100%             |
| Nguyễn Quốc Tường          | Implement crud user profile API | Completed         | 100%             |
| Nguyễn Quốc Tường          | Implement crud tutor API | Completed         | 100%             |
| Nguyễn Quốc Tường          | Implement crud learning session API | Completed         | 100%             |
| Nguyễn Quốc Tường          | Implement tutor learning session creation page | Completed         | 100%             |
| Phan Tấn Phát        | Implement login page            | Completed       | 100%              |
| Phan Tấn Phát        | Implement register page            | Completed       | 100%              |
| Phan Tấn Phát        | Implement verification page            | Completed       | 100%              |
| Vòng Sau Hùng        | Implement dashboard         | Completed         | 100%             |
| Vòng Sau Hùng        | Implement log out feature on Client         | Completed         | 100%             |
| Vòng Sau Hùng        | Implement homepage on Client         | Completed         | 100%             |

# B. Milestone 2
## I. Features
- Search Tutor By Name
- Filter Tutor By Location, Grade, Subject
![search-tutor](https://res.cloudinary.com/daartoyul/image/upload/v1733907954/WP/MS2/Flow/w87jh8855vxx1sbb2yk1.png)
- Evaluate Tutor
![evaluate-tutor](https://res.cloudinary.com/daartoyul/image/upload/v1733907954/WP/MS2/Flow/keg83jjvyv0tgy0gv2vu.png)
- Pay For Learning Session
![payment](https://res.cloudinary.com/daartoyul/image/upload/v1733907954/WP/MS2/Flow/ovvhgfkxivsheexmwxem.png)
## II. UI/UX
- Use WinUI 3 Gallery
- Error Notification
- Some Images:
  - Search And Filter Tutor Page
  ![search-filter-page](https://res.cloudinary.com/dlksshukq/image/upload/v1733914224/Milestone2/bappi7ljpgegtjiwnfa5.png)
  - Tutor Detail Information Page
  ![tutor-detail-page](https://res.cloudinary.com/dlksshukq/image/upload/v1733914322/Milestone2/pevroa89bbl31mohw3lz.png)
  - Tutor Evaluation Page
  ![tutor-evaluation-page](https://res.cloudinary.com/dlksshukq/image/upload/v1733914412/Milestone2/dqffm5mwqr0olrqmpfap.png)
  - Payment Page
  ![payment-page](https://res.cloudinary.com/dlksshukq/image/upload/v1733914467/Milestone2/cglfbyowa23i4o5azry2.png)
## III. Design Pattern / Architecture
- Mapper Pattern
  - Definition: This is a design pattern that separates the in-memory objects from the database. Its responsibility is to transfer data between the two and also to isolate them from each other.
  - Why we use it?
    - Promote the Single Responsibility Principal
    - Improve Separation of Concerns
## IV. Advanced Topics
1. VNPAY Integration
- Use VNPAY as third party service to pay for learning session
- Why don't we create own sending mail service?
  - Reduce server's workload
  - Enhance security
2. RelayCommand
- Use RelayCommand for pagination
- Why we use it for pagination?
  - Decoupling UI from Logic
  - Improve Testability
  - Enhance Reusability
  - Adherence to MVVM Principles
3. Mapper Pattern
- Use to transfer request into entity and transfer entity into response
4. Direct From Windows Application To Web Application
- Use Windows.System.Launcher.LaunchUriAsync(uri); to direct to VNPAY page
## V. Teamwork - Git flow
- Use github Projects Tool for managing team
- Github username:
    - zjn1404: Nguyễn Quốc Tường
    - FATU29: Phan Tấn Phát
    - Shungisme: Vòng Sau Hùng
- Use Git feature branch workflow
  - Git Roadmap:
    ![milestone2-roadmap](https://res.cloudinary.com/daartoyul/image/upload/v1733911162/WP/MS2/GitFlow/hn79kuyleflipry5zu1y.png)
  - Git Contribution Tracking:
    ![git-contribution-tracking](https://res.cloudinary.com/daartoyul/image/upload/v1733911206/WP/MS2/GitFlow/napnp9tzwictyw7ferx1.png)
  - Git Branching:
    ![git-branching](https://res.cloudinary.com/daartoyul/image/upload/v1733911767/WP/MS2/GitFlow/uwoypbbmbvpuhy12ppos.png)
  - Git Log:
    - ![git-log1](https://res.cloudinary.com/daartoyul/image/upload/v1733926448/WP/MS2/GitLog/twa0b9rtlpugy8tuexah.png)
    - ![git-log2](https://res.cloudinary.com/daartoyul/image/upload/v1733926448/WP/MS2/GitLog/diorbyb7kqzcyr2jubgw.png)
    - ![git-log3](https://res.cloudinary.com/daartoyul/image/upload/v1733926448/WP/MS2/GitLog/glmasoh2cq7nsdtszctv.png)
    - ![git-log4](https://res.cloudinary.com/daartoyul/image/upload/v1733926448/WP/MS2/GitLog/kal9gjifv6qsxqxu7l4g.png)
- Use OneDrive for Sharing Document:
![one-drive](https://res.cloudinary.com/daartoyul/image/upload/v1733911777/WP/MS2/GitFlow/vsib3wf0xvnezewixih9.png)
- Use Google Meet for planning
- Use Messenger for discussion
## VI. Quality assurance
- Manual testing for each feature
- Test on both server and client
- [Test Documentation](https://studenthcmusedu-my.sharepoint.com/:x:/g/personal/22120118_student_hcmus_edu_vn/EVQFri5ZUatKunFpOr82-uoBhm6j60kEJ7BJR0I2CfpRdA?e=GOSXaw)

## VII. Member Evaluation

Below is the table evaluating team members based on the tasks assigned to them.

| Member Name       | Task                          | Completion Status | Contribution (%) |
|--------------------|-------------------------------|-------------------|------------------|
| Nguyễn Quốc Tường          | Implement tutor filtering and searching API | Completed         | 100%             |
| Nguyễn Quốc Tường          | Implement tutor evaluation API | Completed         | 100%             |
| Nguyễn Quốc Tường          | Implement VNPAY integration on Server| Completed         | 100%             |
| Nguyễn Quốc Tường          | Implement tutor evaluation feature on Client | Completed         | 100%             |
| Nguyễn Quốc Tường          | Implement all tutor's evaluations page | Completed         | 100%             |
| Phan Tấn Phát        | Implement tutor searching and filtering feature on Client            | Completed       | 100%              |
| Phan Tấn Phát        | Implement tutor detail page            | Completed       | 100%              |
| Phan Tấn Phát        | Decorate UI            | Completed       | 100%              |
| Vòng Sau Hùng        | Implement session payment feature on Client         | Completed         | 100%             |
| Vòng Sau Hùng        | Implement tutor's achievement update feature on Client         | Completed         | 100%             |


**Key:**
- **Completion Status:** Indicates whether the task is *Not Started*, *In Progress*, or *Completed*.
- **Contribution (%):** Reflects the member's individual contribution to the task.
# C. Milestone 3
## I. Features
- Refresh token
![refresh-token](https://res.cloudinary.com/daartoyul/image/upload/v1735893277/refresh_token_diagram_vg1uvq.png)
- Joining Learning Session Room (Video Call)
![joining-session-room](https://res.cloudinary.com/daartoyul/image/upload/v1735893277/Joining_Learning_Session_Room_jmv42w.png)
- Reminder Email: The server will check every 10 minutes and send a reminder email if any learning sessions are scheduled to begin within the next 30 minutes.
- Expiration Token Cleanup: The server will clean up expiration tokens in database every 24 hours.
- Expiration Learning Session, Order, Order Details Clean Up: The server will clean up the learning sessions, order, order details in database every 1 year.
## II. UI/UX
- Redecorate all pages of the application
  - Login
  ![login-page](https://res.cloudinary.com/dleqlbrku/image/upload/v1736153669/b5097e09-63f5-439b-b88c-d9003c9dd149.png)
  - Registration
  ![registration-page](https://res.cloudinary.com/dleqlbrku/image/upload/v1736153708/51d34fda-7a27-4a9d-b3f4-0130bec6c1c3.png)
  - Account Verification
  ![account-verification](https://res.cloudinary.com/dleqlbrku/image/upload/v1736154476/bee63831-e671-4b76-b769-9ba6a00b64c4.png)
  - Homepage
  ![homepage](https://res.cloudinary.com/dleqlbrku/image/upload/v1736153751/312ce187-fe86-4af2-acd0-eb923f857d5f.png)
  - My Ordered Session
  ![my-ordered-session1](https://res.cloudinary.com/dleqlbrku/image/upload/v1736153800/e6dbf822-85e1-412e-9bf9-5fcb8a8de0d7.png)
  ![my-ordered-session2](https://res.cloudinary.com/dleqlbrku/image/upload/v1736153832/3cb9528f-e3d3-40c4-bcb3-e9d40625077c.png)
  - Sessions
  ![sessions](https://res.cloudinary.com/dleqlbrku/image/upload/v1736153873/7266cd1a-adb4-42e3-a9c6-4c9c546662fd.png)
  - Session Purchasing
  ![session-purchasing1](https://res.cloudinary.com/dleqlbrku/image/upload/v1736153908/814bb740-5e4c-4612-a42b-8b13c7b75960.png)
  ![session-purchasing2](https://res.cloudinary.com/dleqlbrku/image/upload/v1736153935/cba30c51-e285-42d1-a94d-ab823648c8f5.png)
  - Tutors
  ![tutors](https://res.cloudinary.com/dleqlbrku/image/upload/v1736153986/1e0ced98-e6ee-43df-9ae3-126860ece1f4.png)
  - Tutor Detail
  ![tutor-detail1](https://res.cloudinary.com/dleqlbrku/image/upload/v1736154021/21adc811-f7d2-40b4-98e5-9c81076c438d.png)
  ![tutor-detail2](https://res.cloudinary.com/dleqlbrku/image/upload/v1736154052/86d5f035-f697-43e6-aecc-0a59a8902174.png)
  - Reviews
  ![reviews](https://res.cloudinary.com/dleqlbrku/image/upload/v1736154077/06584579-10a1-41c4-98f0-b4c55235f014.png)
  - UserProfile
  ![user-profile1](https://res.cloudinary.com/dleqlbrku/image/upload/v1736154110/4e26d949-9668-4baa-b014-9e6a47662388.png)
  ![user-profile2](https://res.cloudinary.com/dleqlbrku/image/upload/v1736154147/a2548d98-36a2-4380-9824-79a5f4dbfc75.png)
  - Session Creation
  ![session-creation](https://res.cloudinary.com/dleqlbrku/image/upload/v1736154268/4b897aa5-05f7-4cea-b983-b4427f25bc18.png)
  - Session Room
  ![session-room1](https://res.cloudinary.com/dleqlbrku/image/upload/v1736154544/83ffba72-bffe-477a-9f12-5c444c050a1f.png)
  ![session-room2](https://res.cloudinary.com/dleqlbrku/image/upload/v1736154588/a8fde672-f455-490f-bb20-4231c9af56c0.png)
## III. Design Pattern / Architecture
- Strategy Pattern:
  - This pattern is used to build emails, as we have two types of emails: verification email and reminder email. Each type has its own structure and logic, which are encapsulated in separate classes for better flexibility and maintainability.
  - Why we use this pattern?
    - Separation of Concerns
    - Flexibility
    - Reusability
    - Scalability
## IV. Advanced Topics
1. Refresh Token
- Enhance user's experience
- Provides a secure way to handle session expiration without exposing sensitive credentials.
- Supports long-lived sessions while maintaining security best practices.
2. Expiration Token Cleanup:
- Improved Security
- Database Efficiency
3. Joining Learning Session Room (Video Call):
- Integrate with ZegoCloud that supports conference room with video call, screen sharing, chat box and limit participants.
## V. Teamwork - Git flow
- Use github Projects Tool for managing team
- Github username:
    - zjn1404: Nguyễn Quốc Tường
    - FATU29: Phan Tấn Phát
    - Shungisme: Vòng Sau Hùng
- Use Git feature branch workflow
  - Git Roadmap:
    ![milestone3-roadmap1](https://res.cloudinary.com/daartoyul/image/upload/v1735893911/git_roadmap_1_tqolrw.png)
    ![milestone3-roadmap2](https://res.cloudinary.com/daartoyul/image/upload/v1735893911/git_roadmap_2_ecwkg1.png)
  - Git Contribution Tracking:
    ![git-contribution-tracking](https://res.cloudinary.com/daartoyul/image/upload/v1735894086/Git_Contribution_Tracking_mrbwax.png)
  - Git Branching:
    ![git-branching](https://res.cloudinary.com/daartoyul/image/upload/v1735893764/git_branch_ms3_wp_htaf4z.png)
  - Git Log:
    ![git-log1](https://res.cloudinary.com/daartoyul/image/upload/v1735893552/git_log_1_k709ur.png)
    ![git-log2](https://res.cloudinary.com/daartoyul/image/upload/v1735893553/git_log_2_arfua8.png)
- Use OneDrive for Sharing Document:
![one-drive](https://res.cloudinary.com/daartoyul/image/upload/v1735894631/onedrive_ms3_n9pmzo.png)
- Use Google Meet for planning
- Use Messenger for discussion
## VI. Quality assurance
- Manual testing for each feature
- Test on both server and client
- [Test Documentation](https://studenthcmusedu-my.sharepoint.com/:x:/g/personal/22120118_student_hcmus_edu_vn/EX8t0AOUamtDgLuvklOzzq4BATYWCMNJBF-IUHVtNFcKvg?e=pIk1xp)

## VII. Member Evaluation

Below is the table evaluating team members based on the tasks assigned to them.

| Member Name       | Task                          | Completion Status | Contribution (%) |
|--------------------|-------------------------------|-------------------|------------------|
| Nguyễn Quốc Tường          | Integrate User's Ordered Sessions Page | Completed         | 100%             |
| Nguyễn Quốc Tường          | Integrate Tutor's Session Homepage | Completed         | 100%             |
| Nguyễn Quốc Tường          | Implement Reminder Email On Server | Completed         | 100%             |
| Nguyễn Quốc Tường          | Implement Refresh Token On Server | Completed         | 100%             |
| Nguyễn Quốc Tường          | Implement Joining Sessions Room On Server | Completed         | 100%             |
| Nguyễn Quốc Tường          | Implement Expiration Token Clean Up | Completed         | 100%             |
| Nguyễn Quốc Tường          | Implement Sessions, Orders, Order Details Clean Up | Completed         | 100%             |
| Phan Tấn Phát        | Integrate User's Session Homepage | Completed       | 100%              |
| Phan Tấn Phát        | Implement Refresh Token On Client | Completed       | 100%              |
| Vòng Sau Hùng        | Integrate Joining Session Room On Client         | Completed         | 100%             |
| Vòng Sau Hùng        | Redecorate Full UI         | Completed         | 100%             |

**Key:**
- **Completion Status:** Indicates whether the task is *Not Started*, *In Progress*, or *Completed*.
- **Contribution (%):** Reflects the member's individual contribution to the task.

# C. Running Instruction
### 1. Migration
- Step 1: Open folder milestone1
- Step 2: Open folder migration
- Step 3: Open terminal in this folder
- Step 4: Use this command to download knex, dotenv, tedious package
```
npm install --save knex dotenv tedious
```
- Step 5: Use this command to download mysql image to docker
```
docker pull mysql:8.0.40-debian
```
- Step 6: Use this command to run docker image
```
docker run --name mysql -p 3306:3306 -e MYSQL_ROOT_PASSWORD=root -d mysql:8.0.40-debian
```
- Step 7: Create database with name "welearn_db"
- Step 8: Migrate database with command
```
knex migrate:latest
```
- Step 9: Insert seed into database
```
knex seed:run
```
### 2. Run Server
- Step 1: Download server via [server](https://drive.google.com/file/d/1fXFNHVGJNhFoweUc60I_nmvpL3gQ2eBs/view?usp=sharing)
- Step 2: Open terminal
- Step 3: Run server with command
```
java -jar WeLearnApp-0.0.1-SNAPSHOT.jar
```
