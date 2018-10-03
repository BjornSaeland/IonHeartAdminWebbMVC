using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using IonHeartMVC.App_Data;

namespace IonHeartMVC.Controllers
{
    public class PatientDataController : Controller
    {
        private HHDBEntities db = new HHDBEntities();

        // GET: PatientData
        public ActionResult Index()
        {
            return View(db.PatientData.ToList());
        }

        // GET: PatientData/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientData patientData = db.PatientData.Find(id);
            if (patientData == null)
            {
                return HttpNotFound();
            }
            return View(patientData);
        }

        // GET: PatientData/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PatientData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,CalendarDate,Heartrate,HeartrateComment,Weight,WeightComment,Length,LengthComment,WellBeing,WellBeingComment,Notes,BreathFrequency,BreathFrequencyComment,EKG,FluidBody,FluidLungs,BloodPressure,BloodOxygen")] PatientData patientData)
        {
            if (ModelState.IsValid)
            {
                patientData.CalendarDate = DateTime.UtcNow;
                db.PatientData.Add(patientData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(patientData);
        }

        // GET: PatientData/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientData patientData = db.PatientData.Find(id);
            if (patientData == null)
            {
                return HttpNotFound();
            }
            return View(patientData);
        }

        // POST: PatientData/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,CalendarDate,Heartrate,HeartrateComment,Weight,WeightComment,Length,LengthComment,WellBeing,WellBeingComment,Notes,BreathFrequency,BreathFrequencyComment,EKG,FluidBody,FluidLungs,BloodPressure,BloodOxygen")] PatientData patientData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patientData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patientData);
        }

        // GET: PatientData/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientData patientData = db.PatientData.Find(id);
            if (patientData == null)
            {
                return HttpNotFound();
            }
            return View(patientData);
        }

        // POST: PatientData/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PatientData patientData = db.PatientData.Find(id);
            db.PatientData.Remove(patientData);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
