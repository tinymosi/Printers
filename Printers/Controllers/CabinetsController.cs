using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Dapper;
using Printers.Models.ListModels;
using Printers.Services;
using Printers.ViewModels;
using Printers.ViewModels.Cabinets;

namespace Printers.Controllers
{
    public class CabinetsController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["DC"].ConnectionString;

        ListsService listsService = new ListsService();

        // Cabinets
        public ActionResult Index()
        {
            ViewBag.Title = "Кабинеты";
            ViewBag.Message = "Справочник кабинетов организации.";
            return View();
        }

        public ActionResult CabinetsList()
        {
            var cabinets = new List<ListItemViewModel>();

            using (IDbConnection db = new SqlConnection(constr))
            {
                cabinets = db.Query<ListItemViewModel>("SELECT Cabinets.ID, Buildings.Building, Cabinets.Number " +
                    "FROM Buildings INNER JOIN Cabinets ON Buildings.ID = Cabinets.BuildingID " +
                    "WHERE Cabinets.IsDeleted = 0 " +
                    "ORDER BY ID").ToList();
            }

            return PartialView("_CabinetsList", cabinets);
        }

        //GET: CabinetCreate
        [HttpGet]
        public ActionResult Create(int? ID = null)
        {
            var model = new CreateViewModel();

            using (IDbConnection db = new SqlConnection(constr))
            {
                if (ID != null)
                {
                    model = db.Query<CreateViewModel>($"select * FROM dbo.Cabinets WHERE ID = {ID}").FirstOrDefault();
                }
                model.GetBuildings = listsService.GetBuildings();
            };

            ViewBag.Title = "Кабинеты";
            ViewBag.Message = "Добавление нового кабинета";

            return View(model);
        }

        //POST: CabinetCreate
        [HttpPost]
        public ActionResult Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AddCabinet(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "При сохранении кабинета произошла ошибка");
                }
            }
            model.GetBuildings = listsService.GetBuildings();
            return View(model);

        }

        //SEND: CabinetCreate
        public void AddCabinet(CreateViewModel model)
        {
            using (IDbConnection db = new SqlConnection(constr))
            {
                string query;
                if (model.ID != 0)
                {
                    query = $"UPDATE dbo.Cabinets set BuildingID = '{model.BuildingID}', Number = '{model.Number}' where id = {model.ID}";
                }
                else
                {
                    query = $"insert into dbo.Cabinets (BuildingID, Number) values ({model.BuildingID}, '{model.Number}')";
                }
                db.Query(query);
            }
            return;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var buildingDelete = new ListItemViewModel();
            using (IDbConnection db = new SqlConnection(constr))
            {
                buildingDelete = db.Query<ListItemViewModel>($"SELECT Cabinets.ID, Buildings.Building, Cabinets.Number " +
                    "FROM Buildings INNER JOIN Cabinets ON Buildings.ID = Cabinets.BuildingID " +
                    $"WHERE cabinets.ID = {id}").FirstOrDefault();
            }
            return View(buildingDelete);
        }

        [HttpPost]
        public ActionResult Delete(ListItemViewModel model)
        {
            using (IDbConnection db = new SqlConnection(constr))
            {
                db.Query($"update dbo.Cabinets set IsDeleted = 1 where id = {model.ID}");
            }
            return RedirectToAction("Index", "Cabinets");
        }

        [HttpPost]
        public ActionResult GetCabinets(int? id)
        {
            var model = listsService.GetCabinets(id);
            return Json(model);
        }
    }
}