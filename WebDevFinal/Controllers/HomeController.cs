using WebDevFinal.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace WebDevProject.Controllers
{

    public class HomeController : Controller
    {
        private DCSEntities db = new DCSEntities();
        [Route("Homepage")]
        [Route("Home/Index")]
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }


        [Route("Hasan")]
        [Route("Home/Contact")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Route("Maham")]
        [Route("Home/Guide")]
        public ActionResult Guide()
        {
            ViewBag.Message = "Your CareTaker page.";

            return View();
        }

        [Authorize(Roles = "Organization")]
        public ActionResult Organization()
        {
            var schoolID = (from s in db.Schools
                           where s.emailID == User.Identity.Name
                           select s.idSchool).First();
            var final = (from f in db.StudentPersonals
                         where f.School_idSchool == schoolID
                         select f);
            var gradesCount = (from s in final
                              orderby s.grade
                              group s by s.grade into grp
                              select new { key = grp.Key, cnt = grp.Count() }).ToList();

            List<DataPoint> dataPoints = new List<DataPoint>();
            //var dataPoints =[];
            for (int i = 0; i < gradesCount.Count(); i++)
            {
                dataPoints.Add(new DataPoint((int)gradesCount[i].key.GetValueOrDefault(), (int)gradesCount[i].cnt));
                //Console.log(gradesCount[i]);
            }

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return View();

            /*try
            {
                var gradesCount = from s in db.StudentPersonals
                             orderby s.grade
                             group s by s.grade into grp
                             select new { key = grp.Key, cnt = grp.Count() };
                
                ViewBag.DataPoints = JsonConvert.SerializeObject(gradesCount, _jsonSetting);

                return View();
            }
            catch (System.Data.Entity.Core.EntityException)
            {
                return View("Error");
            }
            catch (System.Data.SqlClient.SqlException)
            {
                return View("Error");
            }*/
        }
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            {
                var final = from t in db.Teachers
                            join s in db.Schools on t.School_idSchool equals s.idSchool
                            select new { School = s.nameSchool, Teacher = t.name };
                var TeachCount = (from s in final
                                   orderby s.School
                                   group s by s.School into grp
                                   select new { key = grp.Key, cnt = grp.Count() }).ToList();

                List<DataPoint2> dataPoints = new List<DataPoint2>();
                //var dataPoints =[];
                for (int i = 0; i < TeachCount.Count(); i++)
                {
                    dataPoints.Add(new DataPoint2(TeachCount[i].key, (int)TeachCount[i].cnt));
                    //Console.log(gradesCount[i]);
                }

                ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);


                var studfinal = from s in db.StudentPersonals
                                join sc in db.Schools on s.School_idSchool equals sc.idSchool
                                select new { School = sc.nameSchool, Student = s.name };
                var StudCount = (from s in studfinal
                                  orderby s.School
                                  group s by s.School into grp
                                  select new { key = grp.Key, cnt = grp.Count() }).ToList();

                List<DataPoint2> dataPoints2 = new List<DataPoint2>();
                //var dataPoints =[];
                for (int i = 0; i < TeachCount.Count(); i++)
                {
                    dataPoints2.Add(new DataPoint2(StudCount[i].key, (int)StudCount[i].cnt));
                    //Console.log(gradesCount[i]);
                }

                ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);
                Console.WriteLine(ViewBag.DataPoints2);


                return View(db.Schools.ToList());
                /*List<DataPoint> dataPoints = new List<DataPoint>{
                new DataPoint(10, 22),
                new DataPoint(20, 36),
                new DataPoint(30, 42),
                new DataPoint(40, 51),
                new DataPoint(50, 46),
            };

                ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints); // AJAX se json ka taalluq hy
                ViewBag.Message = "Roles.";*/

                //return View();
            }


        }
    }
}