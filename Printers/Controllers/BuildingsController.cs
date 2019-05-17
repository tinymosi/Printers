using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Dapper;

namespace Printers.Controllers
{
    public class BuildingsController : Controller
    { 
        string constr = ConfigurationManager.ConnectionStrings["DC"].ConnectionString;

        [HttpGet]
        public ActionResult Create()
        {
            var buildingcreate = new Buildings();

            ViewBag.Title = "Кабинеты";
            ViewBag.Message = "Добавление нового здания";

            return View();
        }

        [HttpPost]
        public ActionResult Create(string Building)
        {
            var buildingcreate = new Performers();
            using (IDbConnection db = new SqlConnection(constr))
            {
                buildingcreate = db.Query<Performers>($"insert into dbo.Buildings ([Building]) values ('{Building}')").FirstOrDefault();
            }
            return RedirectToAction("Index", "Cabinets");
        }
    }
}