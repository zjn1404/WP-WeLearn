using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Services.Interfaces.ForAPI
{
    public interface IVideoCallService
    {
        Task<string> JoinRoom(string learningSessionId);
    }
}
