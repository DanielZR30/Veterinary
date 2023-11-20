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
using System.Web.Http.Description;
using Veterinary.Models;

namespace Veterinary.Controllers
{
    public class InvoicesController : ApiController
    {
        private VeterinaryEntities db = new VeterinaryEntities();
        private IInvoiceService _invoiceService = new InvoiceService(db);

        // GET: api/Invoices
        public IQueryable<Invoice> GetInvoice()
        {
            return db.Invoice;
        }

        // GET: api/Invoices/5
        [ResponseType(typeof(Invoice))]
        public async Task<IHttpActionResult> GetInvoice(Guid id)
        {
            Invoice invoice = await db.Invoice.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(invoice);
        }

        // PUT: api/Invoices/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInvoice(Guid id, Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != invoice.IDInvoice)
            {
                return BadRequest();
            }

            db.Entry(invoice).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Invoices
        [ResponseType(typeof(Invoice))]
        public async Task<IHttpActionResult> PostInvoice(Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Invoice.Add(invoice);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InvoiceExists(invoice.IDInvoice))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = invoice.IDInvoice }, invoice);
        }

        // DELETE: api/Invoices/5
        [ResponseType(typeof(Invoice))]
        public async Task<IHttpActionResult> DeleteInvoice(Guid id)
        {
            Invoice invoice = await db.Invoice.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            db.Invoice.Remove(invoice);
            await db.SaveChangesAsync();

            return Ok(invoice);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InvoiceExists(Guid id)
        {
            return db.Invoice.Count(e => e.IDInvoice == id) > 0;
        }
    }
}