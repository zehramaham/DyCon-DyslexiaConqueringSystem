using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebDevFinal.Models;

namespace WebDevFinal.Controllers
{
    public class StudentSchoolController : Controller
    {
        private DCSEntities db = new DCSEntities();

        // GET: StudentSchool
        public ActionResult Index()
        {
            var studentSchools = db.StudentSchools.Include(s => s.StudentPersonal);
            var schoolID = (from s in db.Schools
                            where s.emailID == User.Identity.Name
                            select s.idSchool).First();
            var final = (from f in studentSchools
                         where f.StudentPersonal.School_idSchool == schoolID
                         select f);
            return View(final.ToList());
        }

        // GET: StudentSchool/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentSchool studentSchool = db.StudentSchools.Find(id);
            if (studentSchool == null)
            {
                return HttpNotFound();
            }
            return View(studentSchool);
        }

        // GET: StudentSchool/Create
        public ActionResult Create()
        {
            var schoolID = (from s in db.Schools
                            where s.emailID == User.Identity.Name
                            select s.idSchool).First();
            var final = (from f in db.StudentPersonals
                         where f.School_idSchool == schoolID
                         select f);
            ViewBag.StudentPersonal_idStudent = new SelectList(final, "idStudent", "name");
            return View();
        }

        // POST: StudentSchool/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentPersonal_idStudent,month_2,reading_decoding,reading_comprehension,reading_rate_fluency,spelling_accuracy,mathematics_computation,mathematics_concepts,handwriting,writing_rate,punctuation_grammer,ability_to_express_thoughts_through_writing,gross_motor_skills,fine_motor_skills")] StudentSchool studentSchool)
        {
            if (ModelState.IsValid)
            {
                db.StudentSchools.Add(studentSchool);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentPersonal_idStudent = new SelectList(db.StudentPersonals, "idStudent", "name", studentSchool.StudentPersonal_idStudent);
            return View(studentSchool);
        }

        // GET: StudentSchool/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentSchool studentSchool = db.StudentSchools.Find(id);
            if (studentSchool == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentPersonal_idStudent = new SelectList(db.StudentPersonals, "idStudent", "name", studentSchool.StudentPersonal_idStudent);
            return View(studentSchool);
        }

        // POST: StudentSchool/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentPersonal_idStudent,month_2,reading_decoding,reading_comprehension,reading_rate_fluency,spelling_accuracy,mathematics_computation,mathematics_concepts,handwriting,writing_rate,punctuation_grammer,ability_to_express_thoughts_through_writing,gross_motor_skills,fine_motor_skills")] StudentSchool studentSchool)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentSchool).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentPersonal_idStudent = new SelectList(db.StudentPersonals, "idStudent", "name", studentSchool.StudentPersonal_idStudent);
            return View(studentSchool);
        }

        // GET: StudentSchool/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentSchool studentSchool = db.StudentSchools.Find(id);
            if (studentSchool == null)
            {
                return HttpNotFound();
            }
            return View(studentSchool);
        }

        // POST: StudentSchool/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentSchool studentSchool = db.StudentSchools.Find(id);
            db.StudentSchools.Remove(studentSchool);
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
