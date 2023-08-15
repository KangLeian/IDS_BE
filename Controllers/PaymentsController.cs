using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiPayment.Data;
using WebApiPayment.Models;

namespace WebApiPayment.Controllers
{
    public class PaymentsController : ApiController
    {
        private WebApiPaymentContext _db = new WebApiPaymentContext();

        [HttpGet]
        [Route("api")]
        public IHttpActionResult Index()
        {
            List<payment> paymentList = _db.payments.ToList();
            return Ok(paymentList);
        }

        [HttpGet]
        [Route("api/{id}")]
        public IHttpActionResult findById(int id)
        {
            payment aPayment = _db.payments.Where(x => x.id == id).FirstOrDefault();
            return Ok(aPayment);
        }

        [HttpGet]
        [Route("api/status/{name}")]
        public IHttpActionResult findByStatusId(string name)
        {
            List<payment> aPayment = _db.payments.Include(x => x.stat).Where(x => x.stat.name == name).ToList();
            return Ok(aPayment);
        }
    }
}
