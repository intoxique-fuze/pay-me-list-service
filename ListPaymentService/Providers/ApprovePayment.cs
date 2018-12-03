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
    public class ApprovePayment
    {
        public bool ValidateResponseData(string stringResponse)
        {
            if (string.IsNullOrWhiteSpace(stringResponse)) return false;
            if (stringResponse == "{}") return false;
            return true;
        }

        public async Task<ApproveResponse> ApprovePayments(ApproveRequesst payment)
        {
            HttpResponseMessage response = null;
            string responseJson = null;

            ApproveResponse res = new ApproveResponse();
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
                    string requestBody = JsonConvert.SerializeObject(payment);
                    //string requestBody = JsonConvert.SerializeObject(new ApproveRequesst
                    //{                        
                    //    BRGNR = payment.BRGNR,
                    //    AFTLNR = payment.AFTLNR,
                    //    LDKD_BRUGER = payment.LDKD_BRUGER,
                    //    SPROG = payment.SPROG,
                    //    LANDEKODE = payment.LANDEKODE,
                    //    BULKREFC = payment.BULKREFC,
                    //    INTEKSTBRUGER = payment.INTEKSTBRUGER,
                    //    SPKD_BRUGER = payment.SPKD_BRUGER,
                    //    TXIPERFROM = payment.TXIPERFROM.Substring(0, 8),
                    //    TXIPERTO = payment.TXIPERTO.Substring(0, 8),
                    //    cboSelSortCrit = payment.cboSelSortCrit,
                    //    CBOSELPAGESIZE = payment.CBOSELPAGESIZE,
                    //    BULKREFLBD = payment.BULKREFLBD,
                    //    ISBIGCUSTOMER = payment.ISBIGCUSTOMER,
                    //    txiAmountFrom = payment.txiAmountFrom
                    //});

                    client.BaseAddress = new Uri("https://10.14.30.183:8600/mobilebusiness/sb/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("X-DB-LSID", "");
                    client.DefaultRequestHeaders.Add("X-DB-CorrelId", "MOBANK3"); //This will only work in Test-env - makes it possible to fetch data without valid LSID.. BUT ONLY IN TEST :)
                    client.DefaultRequestHeaders.Add("X-IBM-Client-Id", "5487284e-9bca-4ca7-9d7b-b37f542fc62c");
                    client.DefaultRequestHeaders.Add("X-IBM-Client-Secret", "kL4iV5qL4hQ6iV8xX1pM0mU0sP3mG4bD0yV7sN7nT5hV8iX4yO");

                    HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "F9ApprovePaymentServiceV00/get");
                    req.Content = new StringContent(
                        requestBody,
                        Encoding.UTF8,
                        "application/json");

                    response = await client.SendAsync(req);
                    responseJson = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode && ValidateResponseData(responseJson))
                    {
                        var tempResp = responseJson.Replace("{}", "\"\"");
                        res = JsonConvert.DeserializeObject<ApproveResponse>(tempResp);                        
                    }
                    else
                    {
                        res = new ApproveResponse()
                        {
                            Returkode = "0",
                            StatusCode = "",
                            ReasonCode = "",
                            Returtekst = "ISLAY error",
                            Fejlfelt = ""
                        };
                    }

                    return res;
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