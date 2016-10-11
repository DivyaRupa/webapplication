using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ClientIP.Models;
using System.Web;

namespace ClientIP.APIControllers
{
    public class IPAddressAPIController : ApiController
    {
        private IPAddressEntities db = new IPAddressEntities();

        // GET: api/IPAddressAPI
        public IQueryable<ClientIP.Models.IPAddress> GetIPAddresses()
        {
            HttpRequestMessage httpRequestMessage = HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
            string striPAddress1 = HttpRequestMessageExtensions.GetClientIpAddress(httpRequestMessage);

            //string striPAddress = HttpContext.Current.Request.UserHostAddress;
            ClientIP.Models.IPAddress iPAddress = new Models.IPAddress();
            iPAddress.IPAdd = striPAddress1;
            iPAddress.IsActive = true;
            db.IPAddresses.Add(iPAddress);
            db.SaveChanges();
            return db.IPAddresses;
        }

        // GET: api/IPAddressAPI/5
        [ResponseType(typeof(ClientIP.Models.IPAddress))]
        public IHttpActionResult GetIPAddress(int id)
        {
            ClientIP.Models.IPAddress iPAddress = db.IPAddresses.Find(id);
            if (iPAddress == null)
            {
                return NotFound();
            }

            return Ok(iPAddress);
        }

        // PUT: api/IPAddressAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutIPAddress()
        {

            HttpRequestMessage httpRequestMessage = HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
            string striPAddress1 = HttpRequestMessageExtensions.GetClientIpAddress(httpRequestMessage);

            //string striPAddress = HttpContext.Current.Request.UserHostAddress;
            ClientIP.Models.IPAddress iPAddress = new Models.IPAddress();
            iPAddress.IPAdd = striPAddress1;
            iPAddress.IsActive = false;

            //List<ClientIP.Models.IPAddress> _lstiPAddress = db.IPAddresses;
            var _lstiPAddress = db.IPAddresses
                    .Where(b => b.IPAdd == striPAddress1);

            db.Entry(_lstiPAddress).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
               throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/IPAddressAPI
        [ResponseType(typeof(ClientIP.Models.IPAddress))]
        public IHttpActionResult PostIPAddress(ClientIP.Models.IPAddress iPAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.IPAddresses.Add(iPAddress);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = iPAddress.Id }, iPAddress);
        }

        // DELETE: api/IPAddressAPI/5
        [ResponseType(typeof(ClientIP.Models.IPAddress))]
        public IHttpActionResult DeleteIPAddress(int id)
        {
            ClientIP.Models.IPAddress iPAddress = db.IPAddresses.Find(id);
            if (iPAddress == null)
            {
                return NotFound();
            }

            db.IPAddresses.Remove(iPAddress);
            db.SaveChanges();

            return Ok(iPAddress);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IPAddressExists(int id)
        {
            return db.IPAddresses.Count(e => e.Id == id) > 0;
        }
    }
}