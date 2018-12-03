using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListPaymentService.Models
{
    public class ListPaymentResponse
    {
        public string MoreRows { get; set; }
        public string LastRowRepos { get; set; }
        public string NumberOfPayments { get; set; }
        public string Returkode { get; set; }
        public string StatusCode { get; set; }
        public string ReasonCode { get; set; }
        public string Returtekst { get; set; }
        public Payments payments { get; set; }
    }

    public class Payments
    {
        public List<Payment> paymentrow { get; set; }
    }
}