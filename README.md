# WP-WeLearn

An application that connects students and tutors, allowing students to search for and hire tutors by the hour. The classes offered range from grade 1 to grade 12, covering both natural sciences and social sciences in accordance with the curriculum standards of the Ministry of Education and Training of Vietnam.

## Table of Contents
- [Tech Stack](#i-tech-stack)
- [Features](#ii-features)
- [UI/UX](#iii-uiux)
- [Design patterns / architecture](#iv-design-patterns--architecture)
- [Advanced topics](#v-advanced-topics)
- [Teamwork - Git flow](#vi-teamwork---git-flow)
- [Quality assurance](#vii-quality-assurance)
- [Running Instruction](#viii-running-instruction)

## I. Tech Stack
- **Languages**: C#, Java
- **Frameworks & Libraries**: WinUI, Spring Boot
- **Database**: MySQL
- **Tools**: Git, GitHub, Postman, Visual Studio, IntelliJ IDEA
- **API Document**: APIDog
## II. Features
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
## III. UI/UX
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
## IV. Design patterns / architecture
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
## V. Advanced topics
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
## VI. Teamwork - Git flow
- Use github Projects Tool for managing team
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
## VII. Quality assurance
- Manual testing for each feature
- Test on both server and client
- [Test Documentation](https://studenthcmusedu-my.sharepoint.com/:x:/g/personal/22120413_student_hcmus_edu_vn/EcE-oijGE4VDnLwWLsgeUPgBTezcQoKNgbB-F4P8m_w2kw?e=JqaGqE)
## VIII. Running Instruction
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
- Step 1: Download server via [server](https://studenthcmusedu-my.sharepoint.com/:f:/g/personal/22120413_student_hcmus_edu_vn/EqophLSQYoZBg5hopkUMLtYBjPwofKhu1d0oAyMnJXeLYg?e=Xy7AeE)
- Step 2: Open terminal
- Step 3: Run server with command
```
java -jar WeLearnApp-0.0.1-SNAPSHOT.jar
```
