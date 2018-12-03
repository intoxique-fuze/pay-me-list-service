using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListPaymentService.Models
{
    public class Payment
    {
        public string txoAmount { get; set; }
        public string txoCurrency { get; set; }
        public string txoEkspDt { get; set; }
        public string txoAfsKto { get; set; }
        public string txoPosttxt { get; set; }
        public string txoTilNvn { get; set; }
        public string txoTilKto { get; set; }
        public string txoBTTY { get; set; }
        public string txoBTTYTT { get; set; }
        public string icoBTST { get; set; }
        public string txoBTST { get; set; }
        public string txoBTSTTT { get; set; }
        public string txoFee { get; set; }
        public string txhRfnrar { get; set; }
        public string txhOprettid { get; set; }
        public string txoDecimals { get; set; }
        public string paymentTypeValue { get; set; }
        public string paymentStatusValue { get; set; }
    }
}