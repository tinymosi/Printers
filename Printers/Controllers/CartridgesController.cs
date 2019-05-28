using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Printers.Services;
using Printers.ViewModels.Cartridges;
using System;

namespace Printers.Controllers
{
    public class CartridgesController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["DC"].ConnectionString;

        ListsService listsService = new ListsService();

        // Cartridges
        public ActionResult Index()
        {
            var cartridges = new List<CartridgesView>();

            using (IDbConnection db = new SqlConnection(constr))
            {
                cartridges = db.Query<CartridgesView>("SELECT dbo.Cartridges.ID, dbo.Cartridges.CartridgeModel, dbo.PrintBrands.PrinterBrand, dbo.PrintModels.PrinterModel, dbo.Cartridges.CartridgeColor " +
                        "FROM dbo.PrintModels INNER JOIN " +
                         "dbo.Cartridges ON dbo.PrintModels.ID = dbo.Cartridges.PrinterModelID INNER JOIN " +
                         "dbo.PrintBrands ON dbo.PrintModels.PrinterBrandID = dbo.PrintBrands.ID " +
                         "WHERE Cartridges.IsDeleted = 0 " +
                         "ORDER BY ID DESC").ToList();
            }
            ViewBag.Title = "Справочник подходящих расходников";
            //ViewBag.Message = "";

            return View(cartridges);
        }

        //GET: CabinetCreate
        [HttpGet]
        public ActionResult Create(int? id = null)
        {
            var model = new CreateViewModel();

            using (IDbConnection db = new SqlConnection(constr))
            {
                if (id != null)
                {
                    model = db.Query<CreateViewModel>($"select * FROM dbo.Cartridges WHERE ID = {id}").FirstOrDefault();
                }
                model.GetPrintersModels =  listsService.GetPrintersModels();
            };

            ViewBag.Title = "Картриджы";
            ViewBag.Message = "Добавление нового картриджа";

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
                    ModelState.AddModelError("", "При сохранении картриджа произошла ошибка");
                }
            }
            model.GetPrintersModels = listsService.GetPrintersModels();
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
                    query = $"UPDATE dbo.Cartridges set PrinterModelID = '{model.PrinterModelID}', CartridgeModel = '{model.CartridgeModel}', CartridgeColor = '{model.CartridgeColor}' where id = {model.ID}";
                }
                else
                {
                    query = $"insert into dbo.Cartridges (PrinterModelID, CartridgeModel, CartridgeColor) values ({model.PrinterModelID}, '{model.CartridgeModel}', '{model.CartridgeColor}')";
                }
                db.Query(query);
            }
            return;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var cartridgeDelete = new ListItemViewModel();
            using (IDbConnection db = new SqlConnection(constr))
            {
                cartridgeDelete = db.Query<ListItemViewModel>($"SELECT dbo.Cartridges.ID, dbo.Cartridges.CartridgeModel, dbo.PrintBrands.PrinterBrand, dbo.PrintModels.PrinterModel, dbo.Cartridges.CartridgeColor " +
                        "FROM dbo.PrintModels INNER JOIN " +
                         "dbo.Cartridges ON dbo.PrintModels.ID = dbo.Cartridges.PrinterModelID INNER JOIN " +
                         "dbo.PrintBrands ON dbo.PrintModels.PrinterBrandID = dbo.PrintBrands.ID " +
                         $"WHERE Cartridges.ID = {id}").FirstOrDefault();
            }
            return View(cartridgeDelete);
        }

        [HttpPost]
        public ActionResult Delete(ListItemViewModel model)
        {
            using (IDbConnection db = new SqlConnection(constr))
            {
                db.Query($"update dbo.Cartridges set IsDeleted = 1 where id = {model.ID}");
            }
            return RedirectToAction("Index", "Cartridges");
        }
    }
}