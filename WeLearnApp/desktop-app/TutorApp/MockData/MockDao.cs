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
    }
}
