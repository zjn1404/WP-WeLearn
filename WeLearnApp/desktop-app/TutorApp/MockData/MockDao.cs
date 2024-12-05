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
    

        public List<LearningSession> GetAllLearningSessions()
        {
            List<LearningSession> LearningSessionList = new List<LearningSession>{
                new()
                {
                    Id = "1",
                    TutorId = "1",
                    StartTime = new DateTime(2000, 1, 1),
                    Duration = 60,
                    GradeId = 1,
                    SubjectName = "Math",
                    Tuition = 100,
                    LearningMethodName = "Online"
                },
                new()
                {
                    Id = "2",
                    TutorId = "2",
                    StartTime = new DateTime(2000, 1, 1),
                    Duration = 60,
                    GradeId = 2,
                    SubjectName = "Math",
                    Tuition = 10000,
                    LearningMethodName = "Offline"
                },
                new()
                {
                    Id = "3",
                    TutorId = "3",
                    StartTime = new DateTime(2000, 1, 1),
                    Duration = 60,
                    GradeId = 3,
                    SubjectName = "Computer Science",
                    Tuition = 10000000,
                    LearningMethodName = "Online"
                }
            };
            return LearningSessionList;
        }


    }
}
