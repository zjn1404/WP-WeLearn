using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Models;
using TutorApp.Models.ForAPI.Response;
using TutorApp.Models.ForAPI.Request;

namespace TutorApp.Services.Interfaces.ForAPI
{
    public interface IPaymentService
    {
        Task<string> CreatePayment(string amount, string learningSessionId, string token);
    }
}
