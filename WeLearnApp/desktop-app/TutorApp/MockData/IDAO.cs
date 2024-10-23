using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models;

namespace TutorApp.MockData.Tutors
{
    public interface IDAO
    {
        List<Tutor> GetTutors();
    }
}
