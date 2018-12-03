using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListPaymentService.Models
{
    public class PaymentRow
    {
        public string RFNRAR { get; set; }
        public string LAESTIMESTAMP { get; set; }
        public string BTTY { get; set; }
        public string TILIDENT { get; set; }
        public string BELOEB { get; set; }
        public Int32 AntalDec { get; set; }
        public string VALUTAKODE { get; set; }
    }
}