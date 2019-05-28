using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
using Dapper;
using Printers.ViewModels;
using Printers.ViewModels.Buildings;

namespace Printers.Controllers
{
    public class BuildingsController : Controller
    { 
        string constr = ConfigurationManager.ConnectionStrings["DC"].ConnectionString;

        public ActionResult BuildingsList()
        {
            var buildings = new List<BuildingViewModel>();

            using (IDbConnection db = new SqlConnection(constr))
            {
                buildings = db.Query<BuildingViewModel>("SELECT ID, Building " +
                    "FROM Buildings " +
                    "WHERE IsDeleted = 0 " +
                    "ORDER BY ID").ToList();
            }
            return PartialView("_BuildingsList", buildings);
        }

        [HttpGet]
        public ActionResult Create(int? ID = null)
        {
            var buildingcreate = new BuildingViewModel();

            if (ID != null)
            {
                using (IDbConnection db = new SqlConnection(constr))
                {
                    buildingcreate = db.Query<BuildingViewModel>($"select * FROM dbo.Buildings WHERE ID = {ID}").FirstOrDefault();
                }
            }

            ViewBag.Title = "Кабинеты";
            ViewBag.Message = "Добавление нового здания";

            return View(buildingcreate);
        }

        [HttpPost]
        public ActionResult Create(BuildingViewModel model)
        {
            using (IDbConnection db = new SqlConnection(constr))
            {
                string query;
                if (model.ID != 0)
                {
                    query = $"UPDATE dbo.Buildings set Building = '{model.Building}' where id = {model.ID}";
                }
                else
                {
                    query = $"insert into dbo.Buildings ([Building]) values('{model.Building}')";
                }

                db.Query<BuildingViewModel>(query);
            }

            return RedirectToAction("Index", "Cabinets");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var buildingDelete = new BuildingViewModel();
            using (IDbConnection db = new SqlConnection(constr))
            {
                buildingDelete = db.Query<BuildingViewModel>($"select * FROM dbo.Buildings WHERE ID = {id}").FirstOrDefault();
            }
            return View(buildingDelete);
        }

        [HttpPost]
        public ActionResult Delete(BuildingViewModel model)
        {
            using (IDbConnection db = new SqlConnection(constr))
            {
                db.Query($"update dbo.Buildings set IsDeleted = 1 where id = {model.ID}");
            }
            return RedirectToAction("Index", "Cabinets");
        }
    }
}