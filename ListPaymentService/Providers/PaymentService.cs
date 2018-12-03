using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListPaymentService.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ListPaymentService.Providers
{
    public class PaymentService
    {
        public bool ValidateResponseData(string stringResponse)
        {
            if (string.IsNullOrWhiteSpace(stringResponse)) return false;
            if (stringResponse == "{}") return false;
            return true;
        }

        public async Task<ListPaymentResponse> MakePayment(ListPaymentRequest payment)
        {
            HttpResponseMessage response = null;
            string responseJson = null;
            
            ListPaymentResponse payments = new ListPaymentResponse();
            using (HttpClient client = new HttpClient())
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback +=
                    (se, cert, chain, sslerror) =>
                    {
                        return true;
                    };
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;

                try
                {
                    string requestBody = JsonConvert.SerializeObject(new ListPaymentRequest
                    {
                        LSID = payment.LSID,
                        BRGNR = payment.BRGNR,
                        AFTLNR = payment.AFTLNR,
                        VIEWUSER = payment.VIEWUSER,
                        LDKD_BRUGER = payment.LDKD_BRUGER,
                        SPROG = payment.SPROG,
                        TIMEZONEID = payment.TIMEZONEID,
                        ACTION = payment.ACTION,
                        CBOSELFOLDER = payment.CBOSELFOLDER,
                        CBOSELPERIOD = payment.CBOSELPERIOD,
                        OPTPERIODTP = payment.OPTPERIODTP,
                        CBOSELPAYSTAT = payment.CBOSELPAYSTAT,
                        TXIPERFROM = payment.TXIPERFROM.Substring(0,8),
                        TXIPERTO = payment.TXIPERTO.Substring(0, 8),
                        cboSelSortCrit = payment.cboSelSortCrit,
                        CBOSELPAGESIZE = payment.CBOSELPAGESIZE,
                        BULKREFLBD = payment.BULKREFLBD,
                        ISBIGCUSTOMER = payment.ISBIGCUSTOMER,
                        txiAmountFrom = payment.txiAmountFrom,
                        txiAmountFromDec = payment.txiAmountFromDec,
                        txiAmountTo = payment.txiAmountTo,
                        txiAmountToDec = payment.txiAmountToDec,
                        cboCurrency = payment.cboCurrency,
                        cboCreateUsr = payment.cboCreateUsr,
                        cboApproveUsr = payment.cboApproveUsr,
                        cboDebAccount = payment.cboDebAccount,
                        txiTextStr = payment.txiTextStr,
                        txiBeneNameStr = payment.txiBeneNameStr,
                        txiBeneAccStr = payment.txiBeneAccStr,
                        cboPaymType = payment.cboPaymType,
                        txiRfNr = payment.txiRfNr,
                        cboRfnrTp = payment.cboRfnrTp,
                        txiSenderID = payment.txiSenderID,
                        chkBulkDebit = payment.chkBulkDebit,
                        chkXtraFee = payment.chkXtraFee
                    });

                    client.BaseAddress = new Uri("https://10.14.30.183:8600/mobilebusiness/sb/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("X-DB-LSID", "");
                    client.DefaultRequestHeaders.Add("X-DB-CorrelId", "MOBANK3"); //This will only work in Test-env - makes it possible to fetch data without valid LSID.. BUT ONLY IN TEST :)
                    client.DefaultRequestHeaders.Add("X-IBM-Client-Id", "5487284e-9bca-4ca7-9d7b-b37f542fc62c");
                    client.DefaultRequestHeaders.Add("X-IBM-Client-Secret", "kL4iV5qL4hQ6iV8xX1pM0mU0sP3mG4bD0yV7sN7nT5hV8iX4yO");

                    HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "BNF9PListServiceV00/get");
                    req.Content = new StringContent(
                        requestBody,
                        Encoding.UTF8,
                        "application/json");

                    response = await client.SendAsync(req);
                    responseJson = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode && ValidateResponseData(responseJson))
                    {
                        //if (JsonConvert.DeserializeObject<dynamic>(responseJson).NumberOfTransactions > "0")
                        //{
                        //Json deserializer throws when {} orccurs. That is why we replace all {} with empty string. 
                        var tempResp = responseJson.Replace("{}", "\"\"");
                        payments = JsonConvert.DeserializeObject<ListPaymentResponse>(tempResp);
                        for (var i = 0; i < payments.payments.paymentrow.Count(); i++)
                        {
                            payments.payments.paymentrow[i].txoAmount = payments.payments.paymentrow[i].txoAmount.TrimStart('0');
                            string dec = payments.payments.paymentrow[i].txoAmount.Substring(payments.payments.paymentrow[i].txoAmount.Length - 2);
                            string mantisa = payments.payments.paymentrow[i].txoAmount.Substring(0, payments.payments.paymentrow[i].txoAmount.Length - 2);
                            payments.payments.paymentrow[i].txoAmount = mantisa + "," + dec;
                        }
                        //}
                        //else
                        //{
                        //    res = new Response
                        //    {
                        //        ReturnText = "No Transactions",
                        //        ReturnCode = "0",
                        //        FailureId = "0000000"
                        //    };
                        //}
                    }
                    else
                    {
                        payments = new ListPaymentResponse()
                        {
                            MoreRows = "N",
                            LastRowRepos = "",
                            NumberOfPayments = "0",
                            Returkode = "0",
                            StatusCode = "",
                            ReasonCode = "",
                            Returtekst = "ISLAY error"
                        };
                    }

                    return payments;
                }
                catch (Exception ex)
                {
                    #region ErrorLogging

                    #endregion
                    return null;
                }
            }
        }
    }
}