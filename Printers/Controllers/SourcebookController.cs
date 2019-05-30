using Dapper;
using Printers.Services;
using Printers.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Printers.Controllers
{
    public class SourcebookController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["DC"].ConnectionString;

        ListsService listsService = new ListsService();

        // Main page of sourebook
        public ActionResult Index()
        {
            ViewBag.Title = "Справочник";
            ViewBag.Message = "Справочник наименований техники предприятия.";
            return View();
        }

        // Partial view of brands
        public ActionResult Brands()
        {
            var brands = new List<PrintBrands>();

            using (IDbConnection db = new SqlConnection(constr))
            {
                brands = db.Query<PrintBrands>("SELECT ID, PrinterBrand FROM dbo.PrintBrands WHERE IsDeleted = 0 ORDER BY ID desc").ToList();
            }
            return PartialView("_Brands", brands);
        }

        // Create new brand if passed id = 0
        // Edit existing brand if passed id !=0
        [HttpGet]
        public ActionResult CreateBrand(int? ID = null)
        {
            var brandcreate = new PrintBrands();

            if (ID != null)
            {
                using (IDbConnection db = new SqlConnection(constr))
                {
                    brandcreate = db.Query<PrintBrands>($"select * FROM dbo.PrintBrands WHERE ID = {ID}").FirstOrDefault();
                }
            }

            ViewBag.Title = "Бренды";
            ViewBag.Message = "Добавление нового бренда в справочник";

            return View(brandcreate);
        }

        [HttpPost]
        public ActionResult CreateBrand(PrintBrands model)
        {
            using (IDbConnection db = new SqlConnection(constr))
            {
                string query;
                if (model.ID != 0)
                {
                    query = $"UPDATE dbo.PrintBrands set PrinterBrand = '{model.PrinterBrand}' where id = {model.ID}";
                }
                else
                {
                    query = $"insert into dbo.PrintBrands ([PrinterBrand]) values('{model.PrinterBrand}')";
                }

                db.Query<PrintBrands>(query);
            }

            return RedirectToAction("Index");
        }

        // Deleting brand with passed id
        [HttpGet]
        public ActionResult DeleteBrand(int id)
        {
            var brandDelete = new PrintBrands();
            using (IDbConnection db = new SqlConnection(constr))
            {
                brandDelete = db.Query<PrintBrands>($"select * FROM dbo.PrintBrands WHERE ID = {id}").FirstOrDefault();
            }
            return View(brandDelete);
        }

        [HttpPost]
        public ActionResult DeleteBrand(PrintBrands model)
        {
            using (IDbConnection db = new SqlConnection(constr))
            {
                db.Query($"update dbo.PrintBrands set IsDeleted = 1 where id = {model.ID}");
            }
            return RedirectToAction("Index");
        }



        // Partial view of Models
        public ActionResult Models()
        {
            var models = new List<PrinterModelView>();

            using (IDbConnection db = new SqlConnection(constr))
            {
                models = db.Query<PrinterModelView>("SELECT dbo.PrintModels.ID, dbo.PrintBrands.PrinterBrand, dbo.PrintModels.PrinterModel " +
                    "FROM dbo.PrintBrands INNER JOIN " +
                         "dbo.PrintModels ON dbo.PrintBrands.ID = dbo.PrintModels.PrinterBrandID " +
                         "WHERE dbo.PrintModels.IsDeleted = 0 " +
                         "ORDER BY ID desc").ToList();
            }
            return PartialView("_Models", models);
        }

        //GET: CabinetCreate
        [HttpGet]
        public ActionResult CreateModel(int? ID = null)
        {
            var model = new CreateViewModel();

            using (IDbConnection db = new SqlConnection(constr))
            {
                if (ID != null)
                {
                    model = db.Query<CreateViewModel>($"select * FROM dbo.PrintModels WHERE ID = {ID}").FirstOrDefault();
                }
                model.GetPrintersBrands = listsService.GetPrintersBrands();
            };

            ViewBag.Title = "Кабинеты";
            ViewBag.Message = "Добавление нового кабинета";

            return View(model);
        }

        //POST: CabinetCreate
        [HttpPost]
        public ActionResult CreateModel(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    AddModel(model);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "При сохранении кабинета произошла ошибка");
                }
            }
            model.GetPrintersBrands = listsService.GetPrintersBrands();
            return View(model);

        }

        //SEND: CabinetCreate
        public void AddModel(CreateViewModel model)
        {
            using (IDbConnection db = new SqlConnection(constr))
            {
                string query;
                if (model.ID != 0)
                {
                    query = $"UPDATE dbo.PrintModels set PrinterBrandID = '{model.PrinterBrandID}', PrinterModel = '{model.PrinterModel}' where id = {model.ID}";
                }
                else
                {
                    query = $"insert into dbo.PrintModels (PrinterBrandID, PrinterModel) values ({model.PrinterBrandID}, '{model.PrinterModel}')";
                }
                db.Query(query);
            }
            return;
        }

        [HttpGet]
        public ActionResult DeleteModel(int id)
        {
            var buildingDelete = new PrinterModelView();
            using (IDbConnection db = new SqlConnection(constr))
            {
                buildingDelete = db.Query<PrinterModelView>($"SELECT dbo.PrintModels.ID, dbo.PrintBrands.PrinterBrand, dbo.PrintModels.PrinterModel " +
                    "FROM dbo.PrintBrands INNER JOIN " +
                         "dbo.PrintModels ON dbo.PrintBrands.ID = dbo.PrintModels.PrinterBrandID " +
                    $"WHERE PrintModels.ID = {id}").FirstOrDefault();
            }
            return View(buildingDelete);
        }

        [HttpPost]
        public ActionResult DeleteModel(PrinterModelView model)
        {
            using (IDbConnection db = new SqlConnection(constr))
            {
                db.Query($"update dbo.PrintModels set IsDeleted = 1 where id = {model.ID}");
            }
            return RedirectToAction("Index");
        }
    }
}