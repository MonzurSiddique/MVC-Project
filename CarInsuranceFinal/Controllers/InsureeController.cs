using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarInsuranceFinal.Models;

namespace CarInsuranceFinal.Controllers
{
    public class InsureeController : Controller
    {
        private InsuranceEntities db = new InsuranceEntities();


        // GET: Insuree
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // GET: GetQuote
        public ActionResult GetQuote()
        {
            return View();
        }

        // Action for calculating a quote based on the guidelines
        public ActionResult CalculateQuote(Insuree insuree)
        {
            if (!ModelState.IsValid)
            {
                return View("GetQuote", insuree);
            }
            if (insuree.DateOfBirth.Date == DateTime.MinValue || insuree.DateOfBirth.Date > DateTime.Today)
            {
                ViewBag.ErrorMsg = "Date of Birth is invalid";
                return View("GetQuote", insuree);
            }
            
            // Calculating the base quote
            decimal quote = 50; // Base quote

            // Adjusting the quote based on guidelines

            DateTime today = DateTime.Today;
            int age = today.Year - insuree.DateOfBirth.Year;

            // Checking if the birthday this year has already occurred
            if (insuree.DateOfBirth.Date > today.AddYears(-age)) age--;

            // Now using the calculated age in logic below
            if (age <= 18)                              //Age Logic
                quote += 100;
            else if (age >= 19 && age <= 25)
                quote += 50;
            else
                quote += 25;

            if (insuree.CarYear < 2000)                 //CarYear Logic
                quote += 25;

            if (insuree.CarYear > 2015)                 //CarYear Logic
                quote += 25;

            if (insuree.CarMake == "Porsche")           //CarMake Logic
            {
                quote += 25;
                if (insuree.CarModel == "911 Carrera")  //CarModel Logic
                    quote += 25;
            }

            quote += 10 * insuree.SpeedingTickets;

            if (insuree.DUI)
                quote *= 1.25m;                         // 25% increase for DUI

            if (insuree.CoverageType)
                quote *= 1.5m;                          // 50% increase for full coverage

            insuree.Quote = quote;


            if (ModelState.IsValid)
            {

                db.Insurees.Add(insuree);
                db.SaveChanges();
                return View("QuoteResult", insuree);
            }
            
            return View("GetQuote", insuree);
            
        }

        // GET: Insuree/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // GET: Insuree/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Insuree/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {

            if (ModelState.IsValid)
            {
                db.Insurees.Add(insuree);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(insuree);
        }

        // GET: Insuree/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,DUI,SpeedingTickets,CoverageType,Quote")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: Insuree/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null)
            {
                return HttpNotFound();
            }
            return View(insuree);
        }

        // POST: Insuree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuree insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
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
