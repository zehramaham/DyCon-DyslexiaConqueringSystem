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
    public class StudentClassroomController : Controller
    {
        private DCSEntities db = new DCSEntities();

        // GET: StudentClassrooms
        public ActionResult Index()
        {
            var studentClassrooms = db.StudentClassrooms.Include(s => s.StudentPersonal);
            var schoolID = (from s in db.Schools
                            where s.emailID == User.Identity.Name
                            select s.idSchool).First();
            var final = (from f in studentClassrooms
                         where f.StudentPersonal.School_idSchool == schoolID
                         select f);
            return View(final.ToList());
        }

        // GET: StudentClassrooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentClassroom studentClassroom = db.StudentClassrooms.Find(id);
            if (studentClassroom == null)
            {
                return HttpNotFound();
            }
            return View(studentClassroom);
        }

        // GET: StudentClassrooms/Create
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

        // POST: StudentClassrooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentPersonal_idStudent,month_2,understanding_verbal_instruction,classroom_assignment_completion,organizational_skills,getting_hw_to_and_from_school,hw_completion,relationship_with_peers,following_directions,disrupting_class,verbal_participation_in_class")] StudentClassroom studentClassroom)
        {
            if (ModelState.IsValid)
            {
                db.StudentClassrooms.Add(studentClassroom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentPersonal_idStudent = new SelectList(db.StudentPersonals, "idStudent", "name", studentClassroom.StudentPersonal_idStudent);
            return View(studentClassroom);
        }

        // GET: StudentClassrooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentClassroom studentClassroom = db.StudentClassrooms.Find(id);
            if (studentClassroom == null)
            {
                return HttpNotFound();
            }
            var schoolID = (from s in db.Schools
                            where s.emailID == User.Identity.Name
                            select s.idSchool).First();
            var final = (from f in db.StudentPersonals
                         where f.School_idSchool == schoolID
                         select f);
            ViewBag.StudentPersonal_idStudent = new SelectList(final, "idStudent", "name", studentClassroom.StudentPersonal_idStudent);
            return View(studentClassroom);
        }

        // POST: StudentClassrooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentPersonal_idStudent,month_2,understanding_verbal_instruction,classroom_assignment_completion,organizational_skills,getting_hw_to_and_from_school,hw_completion,relationship_with_peers,following_directions,disrupting_class,verbal_participation_in_class")] StudentClassroom studentClassroom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentClassroom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentPersonal_idStudent = new SelectList(db.StudentPersonals, "idStudent", "name", studentClassroom.StudentPersonal_idStudent);
            return View(studentClassroom);
        }

        // GET: StudentClassrooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentClassroom studentClassroom = db.StudentClassrooms.Find(id);
            if (studentClassroom == null)
            {
                return HttpNotFound();
            }
            return View(studentClassroom);
        }

        // POST: StudentClassrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentClassroom studentClassroom = db.StudentClassrooms.Find(id);
            db.StudentClassrooms.Remove(studentClassroom);
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
