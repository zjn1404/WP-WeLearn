using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Models.ForAPI.Request
{
    public class PaymentRequest
    {
        public string amount { get; set; }
        public string bankCode { get; set; }
    }
}
