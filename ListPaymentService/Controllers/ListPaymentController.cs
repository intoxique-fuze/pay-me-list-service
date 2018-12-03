using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using ListPaymentService.Models;
using ListPaymentService.Providers;
using System.Threading.Tasks;
using System.Net;
using System.Web.Http.Cors;

namespace ListPaymentService.Controllers
{    
    // GET: ListPayment
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ListPaymentController : ApiController
    {
        ListPaymentResponse payments = null;
        [System.Web.Http.HttpPost]
        public async Task<IHttpActionResult> MakePayment([FromBody] ListPaymentRequest payment)
        {
            try
            {
                PaymentService client = new PaymentService();
                payments = await client.MakePayment(payment);
            }
            catch (Exception)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
            return Ok(payments);
        }
    }    
}