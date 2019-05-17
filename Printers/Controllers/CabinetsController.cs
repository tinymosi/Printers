using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Dapper;
using Printers.Models.ListModels;
using Printers.Services;

namespace Printers.Controllers
{
    public class CabinetsController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["DC"].ConnectionString;

        ListsService listsService = new ListsService();

        // Cabinets
        public ActionResult Index()
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

        //GET: CabinetCreate
        [HttpGet]
        public ActionResult Create()
        {
            var model = new CabinetsList()
            {
                Cabinets = new Cabinets(),
                GetBuildings = listsService.GetBuildings(),
                //Number
            };
            ViewBag.Title = "Кабинеты";
            ViewBag.Message = "Добавление нового кабинета";

            return View();
        }

        //POST: CabinetCreate
        [HttpPost]
        public ActionResult Create(CabinetsList model)
        {
            try
            {
                AddCabinet(model.Cabinets);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }

        }

        //SEND: CabinetCreate
        public void AddCabinet(Cabinets model)
        {
            using (IDbConnection db = new SqlConnection(constr))
            {
                model = db.Query<Cabinets>($"insert into dbo.Cabinets (BuildingID, Number) values ({model.BuildingID}, {model.Number})").FirstOrDefault();
            }
            return;
        }
    }
}