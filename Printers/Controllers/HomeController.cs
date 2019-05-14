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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Movement()
        {
            var movement = new List<MovementView>();

            using (IDbConnection db = new SqlConnection(constr))
            {
                movement = db.Query<MovementView>("select * from dbo.MovementView order by id desc").ToList();
            }
            ViewBag.Title = "Движение техники";
            ViewBag.Message = "История джижения техники. Где сейчас находится принтер, кто и когда его туда перенес.";

            return View(movement);
        }

        // Cabinets
        public ActionResult Cabinets()
        {
            var cabinets = new List<CabinetsView>();

            using (IDbConnection db = new SqlConnection(constr))
            {
                cabinets = db.Query<CabinetsView>("SELECT * FROM dbo.CabinetsView ORDER BY ID").ToList();
            }
            ViewBag.Title = "Кабинеты";
            ViewBag.Message = "Справочник кабинетов организации.";

            return View(cabinets);
        }

        
    }
}