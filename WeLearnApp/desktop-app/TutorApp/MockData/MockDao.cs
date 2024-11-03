using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.MockData.Tutors;
using TutorApp.Models;

namespace TutorApp.MockData 
{
    public class MockDao : IDAO
    {
        public List<Tutor> GetTutors()
        {
            List<Tutor> tutors = new List<Tutor>()
            {
                new()
                {
                    Id = "1",
                    FirstName = "Phat",
                    LastName = "Phan Tan",
                    DateOfBirth = "1/1/2004",
                    LocationId = "1",
                },
                new()
                {
                    Id = "2",
                    FirstName = "Tuong",
                    LastName = "Nguyen Quoc",
                    DateOfBirth = "1/1/2004",
                    LocationId = "2",
                },
                new()
                {
                    Id = "3",
                    FirstName = "Hung",
                    LastName = "Vong Sau",
                    DateOfBirth = "1/1/2004",
                    LocationId = "3",
                }
            };
            return tutors;
        }

        public List<LearningSession> GetAllLearningSessions()
        {
           List <LearningSession> LearningSessionList = new List<LearningSession>{
                new()
                {
                    Id = "1",
                    TutorId = "1",
                    StartTime = "01/01/2000",
                    Duration = 60,
                    GradeId = 1,
                    SubjectName = "Math",
                    LearningMethodName = "Online"
                },
                new()
                {
                    Id = "2",
                    TutorId = "2",
                    StartTime = "01/01/2000",
                    Duration = 60,
                    GradeId = 2,
                    SubjectName = "Math",
                    LearningMethodName = "Offline"
                },
                new()
                {
                    Id = "3",
                    TutorId = "3",
                    StartTime = "01/01/2000",
                    Duration = 60,
                    GradeId = 3,
                    SubjectName = "Computer Science",
                    LearningMethodName = "Online"
                }
            };
            return LearningSessionList;
        }


    }
}
