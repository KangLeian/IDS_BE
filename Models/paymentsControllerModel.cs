using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using WebApiPayment.Data;

namespace WebApiPayment.Models
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using WebApiPayment.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<payment>("payments");
    builder.EntitySet<stat>("stats"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class paymentsControllerModel : ODataController
    {
        private WebApiPaymentContext db = new WebApiPaymentContext();

        // GET: odata/payments
        [EnableQuery]
        public IQueryable<payment> Getpayments()
        {
            return db.payments;
        }

        // GET: odata/payments(5)
        [EnableQuery]
        public SingleResult<payment> Getpayment([FromODataUri] int key)
        {
            return SingleResult.Create(db.payments.Where(payment => payment.id == key));
        }

        // PUT: odata/payments(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<payment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            payment payment = await db.payments.FindAsync(key);
            if (payment == null)
            {
                return NotFound();
            }

            patch.Put(payment);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!paymentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(payment);
        }

        // POST: odata/payments
        public async Task<IHttpActionResult> Post(payment payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.payments.Add(payment);
            await db.SaveChangesAsync();

            return Created(payment);
        }

        // PATCH: odata/payments(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<payment> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            payment payment = await db.payments.FindAsync(key);
            if (payment == null)
            {
                return NotFound();
            }

            patch.Patch(payment);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!paymentExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(payment);
        }

        // DELETE: odata/payments(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            payment payment = await db.payments.FindAsync(key);
            if (payment == null)
            {
                return NotFound();
            }

            db.payments.Remove(payment);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/payments(5)/stat
        [EnableQuery]
        public SingleResult<stat> Getstat([FromODataUri] int key)
        {
            return SingleResult.Create(db.payments.Where(m => m.id == key).Select(m => m.stat));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool paymentExists(int key)
        {
            return db.payments.Count(e => e.id == key) > 0;
        }
    }
}
