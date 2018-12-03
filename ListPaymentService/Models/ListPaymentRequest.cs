using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListPaymentService.Models
{
    public class ListPaymentRequest
    {
        public string LSID { get; set; }
        public string BRGNR { get; set; }
        public string AFTLNR { get; set; }
        public string VIEWUSER { get; set; }
        public string LDKD_BRUGER { get; set; }
        public string SPROG { get; set; }
        public string TIMEZONEID { get; set; }
        public string ACTION { get; set; }
        public string CBOSELFOLDER { get; set; }
        public string CBOSELPERIOD { get; set; }
        public string OPTPERIODTP { get; set; }
        public string CBOSELPAYSTAT { get; set; }
        public string TXIPERFROM { get; set; }     
        public string TXIPERTO { get; set; }
        public string cboSelSortCrit { get; set; }
        public string CBOSELPAGESIZE { get; set; }
        public string BULKREFLBD { get; set; }
        public string ISBIGCUSTOMER { get; set; }
        public string txiAmountFrom { get; set; }
        public string txiAmountFromDec { get; set; }
        public string txiAmountTo { get; set; }
        public string txiAmountToDec { get; set; }
        public string cboCurrency { get; set; }
        public string cboCreateUsr { get; set; }
        public string cboApproveUsr { get; set; }
        public string cboDebAccount { get; set; }
        public string txiTextStr { get; set; }
        public string txiBeneNameStr { get; set; }
        public string txiBeneAccStr { get; set; }
        public string cboPaymType { get; set; }
        public string txiRfNr { get; set; }
        public string cboRfnrTp { get; set; }
        public string txiSenderID { get; set; }
        public string chkBulkDebit { get; set; }
        public string chkXtraFee { get; set; }
    }
}