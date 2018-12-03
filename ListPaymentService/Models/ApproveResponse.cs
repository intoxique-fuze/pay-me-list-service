using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListPaymentService.Models
{
    public class ApproveResponse
    {
        public string Returkode { get; set; }
        public string StatusCode { get; set; }
        public string ReasonCode { get; set; }
        public string Returtekst { get; set; }
        public string Fejlfelt { get; set; }
    }
}