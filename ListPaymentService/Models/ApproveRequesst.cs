using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListPaymentService.Models
{
    public class ApproveRequesst
    {
        public string AFTLNR { get; set; }
        public string BRGNR { get; set; }
        public string LANDEKODE { get; set; }
        public string SPROG { get; set; }
        public string BULKREFC { get; set; }
        public string INTEKSTBRUGER { get; set; }
        public string LDKD_BRUGER { get; set; }
        public string SPKD_BRUGER { get; set; }
        public string TZID_BRUGER { get; set; }
        public string KT_DROPDOWN_SPECIEL { get; set; }
        public string RATEEXPIRYTIMESTAMP { get; set; }
        public string RATECNTROLTIMESTAMP { get; set; }
        public ApprovePaymentList Payments { get; set; }
    }

    public class ApprovePaymentList
    {
        public List<PaymentRow> PaymentRow { get; set; }
    }
}