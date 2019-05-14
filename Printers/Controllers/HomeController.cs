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


        // Performers
        public ActionResult Performers()
        {
            var performers = new List<Performers>();
            using (IDbConnection db = new SqlConnection(constr))
            {
                performers = db.Query<Performers>("select * from dbo.Performers order by id").ToList();
            }
            ViewBag.Title = "Сотрудниик";
            ViewBag.Message = "Список ответственных сотрудников организации.";

            return View(performers);
        }
    }
}