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
using IonHeartMVC.App_Data;

namespace IonHeartMVC.Controllers
{
    public class PatientDataAPIController : ApiController
    {
        private HHDBEntities db = new HHDBEntities();

        // GET: api/PatientDataAPI
        public IQueryable<PatientData> GetPatientData()
        {
            return db.PatientData;
        }

        // GET: api/PatientDataAPI/5
        [ResponseType(typeof(PatientData))]
        public IHttpActionResult GetPatientData(int id)
        {
            PatientData patientData = db.PatientData.Find(id);
            if (patientData == null)
            {
                return NotFound();
            }

            return Ok(patientData);
        }

        // PUT: api/PatientDataAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPatientData(int id, PatientData patientData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patientData.UserId)
            {
                return BadRequest();
            }

            db.Entry(patientData).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientDataExists(id))
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

        // POST: api/PatientDataAPI
        [ResponseType(typeof(PatientData))]
        public IHttpActionResult PostPatientData(PatientData patientData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PatientData.Add(patientData);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = patientData.UserId }, patientData);
        }

        // DELETE: api/PatientDataAPI/5
        [ResponseType(typeof(PatientData))]
        public IHttpActionResult DeletePatientData(int id)
        {
            PatientData patientData = db.PatientData.Find(id);
            if (patientData == null)
            {
                return NotFound();
            }

            db.PatientData.Remove(patientData);
            db.SaveChanges();

            return Ok(patientData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PatientDataExists(int id)
        {
            return db.PatientData.Count(e => e.UserId == id) > 0;
        }
    }
}