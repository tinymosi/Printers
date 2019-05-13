using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;


namespace Printers.Controllers
{
    public class HomeController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["DC"].ConnectionString;

        public ActionResult Movement()
        {
            var movement = new List<MovementView>();

            using (IDbConnection db = new SqlConnection(constr))
            {
                movement = db.Query<MovementView>("select * from dbo.MovementView order by id desc").ToList();
            }
            ViewBag.Message = "История джижения техники. Где сейчас находится принтер, кто и когда его туда перенес.";

            return View(movement);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}