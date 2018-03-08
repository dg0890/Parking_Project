using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContosoUniversity.DAL;
using ContosoUniversity.Models;
using System.Data.Entity.Infrastructure;

namespace ContosoUniversity.Controllers
{
    public class ParkingController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Parking
        public async Task<ActionResult> Index()
        {
            return View(await db.ParkingLot.ToListAsync());
        }

        // GET: Parking/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parking parking = await db.ParkingLot.FindAsync(id);
            if (parking == null)
            {
                return HttpNotFound();
            }
            return View(parking);
        }

        // GET: Parking/Create
        public ActionResult Create()
        {
            PopulateOccupantsDropDownList();
            return View();
        }

        // POST: Course/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ParkingID")] Parking parking)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    db.ParkingLot.Add(parking);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateOccupantsDropDownList(parking.OccupantID);
            return View(parking);
        }

        // GET: Parking/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parking parking = await db.ParkingLot.FindAsync(id);
            if (parking == null)
            {
                return HttpNotFound();
            }
            PopulateOccupantsDropDownList(parking.OccupantID);
            return View(parking);
        }

        // POST: Parking/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var parkingToUpdate = db.ParkingLot.Find(id);
            if (TryUpdateModel(parkingToUpdate, "", new string[] { "ParkingID", "OccupantID" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateOccupantsDropDownList(parkingToUpdate.OccupantID);
            return View(parkingToUpdate);
        }


        private void PopulateOccupantsDropDownList(object selectedOccupant = null)
        {
            var occupantsQuery = from d in db.People
                                orderby d.ID
                                select d;
            ViewBag.OccupantID = new SelectList(occupantsQuery, "ID", "FullName", selectedOccupant);
        }

        // GET: Parking/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parking parking = await db.ParkingLot.FindAsync(id);
            if (parking == null)
            {
                return HttpNotFound();
            }
            return View(parking);
        }

        // POST: Parking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Parking parking = await db.ParkingLot.FindAsync(id);
            db.ParkingLot.Remove(parking);
            await db.SaveChangesAsync();
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
