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
    public class StudentHistoryController : Controller
    {
        private DCSEntities db = new DCSEntities();

        // GET: StudentHistory
        public ActionResult Index()
        {
            var studentHistories = db.StudentHistories.Include(s => s.StudentPersonal);
            var schoolID = (from s in db.Schools
                            where s.emailID == User.Identity.Name
                            select s.idSchool).First();
            var final = (from f in studentHistories
                         where f.StudentPersonal.School_idSchool == schoolID
                         select f);
            return View(final.ToList());
        }

        // GET: StudentHistory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentHistory studentHistory = db.StudentHistories.Find(id);
            if (studentHistory == null)
            {
                return HttpNotFound();
            }
            return View(studentHistory);
        }

        // GET: StudentHistory/Create
        public ActionResult Create()
        {
            var schoolID = (from s in db.Schools
                            where s.emailID == User.Identity.Name
                            select s.idSchool).First();
            var final = (from f in db.StudentPersonals
                         where f.School_idSchool == schoolID
                         select f);
            ViewBag.StudentPersonal_idStudent = new SelectList(final, "idStudent", "name");

            var teachfinal = (from f in db.Teachers
                              where f.School_idSchool == schoolID
                              select f);
            ViewBag.Teacher_idTeacher = new SelectList(teachfinal, "idTeacher", "name");
            return View();
        }

        // POST: StudentHistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentPersonal_idStudent,Teacher_idTeacher,trouble_learning_new_material,little_desire_to_master_new_skills,unable_to_tell_time,cannot_repeat_info,trouble_multithinking,trouble_following_multistep_directions,difficulty_copying,difficulty_orienting_self,poor_spatial_judgement,confuses_directions,mixes_up_lower_and_upper_cases,reverses_letters_and_numbers,trouble_ordering_words_and_events,mispronounciation,trouble_verbally_expressing_thoughts,little_to_no_connection_to_what_others_are_discussing")] StudentHistory studentHistory)
        {
            if (ModelState.IsValid)
            {
                db.StudentHistories.Add(studentHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StudentPersonal_idStudent = new SelectList(db.StudentPersonals, "idStudent", "name", studentHistory.StudentPersonal_idStudent);
            ViewBag.Teacher_idTeacher = new SelectList(db.Teachers, "idTeacher", "name", studentHistory.Teacher_idTeacher);
            return View(studentHistory);
        }

        // GET: StudentHistory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentHistory studentHistory = db.StudentHistories.Find(id);
            if (studentHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.StudentPersonal_idStudent = new SelectList(db.StudentPersonals, "idStudent", "name", studentHistory.StudentPersonal_idStudent);
            return View(studentHistory);
        }

        // POST: StudentHistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentPersonal_idStudent,Teacher_idTeacher,trouble_learning_new_material,little_desire_to_master_new_skills,unable_to_tell_time,cannot_repeat_info,trouble_multithinking,trouble_following_multistep_directions,difficulty_copying,difficulty_orienting_self,poor_spatial_judgement,confuses_directions,mixes_up_lower_and_upper_cases,reverses_letters_and_numbers,trouble_ordering_words_and_events,mispronounciation,trouble_verbally_expressing_thoughts,little_to_no_connection_to_what_others_are_discussing")] StudentHistory studentHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StudentPersonal_idStudent = new SelectList(db.StudentPersonals, "idStudent", "name", studentHistory.StudentPersonal_idStudent);
            return View(studentHistory);
        }

        // GET: StudentHistory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentHistory studentHistory = db.StudentHistories.Find(id);
            if (studentHistory == null)
            {
                return HttpNotFound();
            }
            return View(studentHistory);
        }

        // POST: StudentHistory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentHistory studentHistory = db.StudentHistories.Find(id);
            db.StudentHistories.Remove(studentHistory);
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
