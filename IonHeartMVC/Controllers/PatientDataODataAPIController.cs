using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using IonHeartMVC.App_Data;

namespace IonHeartMVC.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. 
    Merge these statements into the Register method of the WebApiConfig class as applicable. 
    Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using IonHeartMVC.App_Data;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<PatientData>("PatientDataAPI");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class PatientDataODataAPIController : ODataController
    {
        private HHDBEntities db = new HHDBEntities();

        // GET: odata/PatientDataAPI
        [EnableQuery]
        public IQueryable<PatientData> GetPatientDataAPI()
        {
            return db.PatientData;
        }

        // GET: odata/PatientDataAPI(5)
        [EnableQuery]
        public SingleResult<PatientData> GetPatientData([FromODataUri] int key)
        {
            return SingleResult.Create(db.PatientData.Where(patientData => patientData.UserId == key));
        }

        // PUT: odata/PatientDataAPI(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<PatientData> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PatientData patientData = db.PatientData.Find(key);
            if (patientData == null)
            {
                return NotFound();
            }

            patch.Put(patientData);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientDataExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(patientData);
        }

        // POST: odata/PatientDataAPI
        public IHttpActionResult Post(PatientData patientData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PatientData.Add(patientData);
            db.SaveChanges();

            return Created(patientData);
        }

        // PATCH: odata/PatientDataAPI(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<PatientData> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            PatientData patientData = db.PatientData.Find(key);
            if (patientData == null)
            {
                return NotFound();
            }

            patch.Patch(patientData);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientDataExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(patientData);
        }

        // DELETE: odata/PatientDataAPI(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            PatientData patientData = db.PatientData.Find(key);
            if (patientData == null)
            {
                return NotFound();
            }

            db.PatientData.Remove(patientData);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PatientDataExists(int key)
        {
            return db.PatientData.Count(e => e.UserId == key) > 0;
        }
    }
}
