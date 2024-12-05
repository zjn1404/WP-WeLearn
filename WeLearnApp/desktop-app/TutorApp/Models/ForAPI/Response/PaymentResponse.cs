using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI.Response
{
    public class PaymentResponse
    {
        public string vnpCode { get; set; }
        public string vnpMessage { get; set; }
        public string paymentUrl { get; set; }
    }
}
