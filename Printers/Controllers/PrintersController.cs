using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Dapper;
using Printers.Models.ListModels;
using Printers.Services;
using Printers.ViewModels.Printers;

namespace Printers.Controllers
{
    public class PrintersController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["DC"].ConnectionString;

        ListsService listsService = new ListsService();

        // GET: Printers
        public ActionResult Index()
        {
            var printers = new List<PrintersView>();

            using (IDbConnection db = new SqlConnection(constr))
            {
                printers = db.Query<PrintersView>("select * from dbo.PrintersView ORDER BY ID DESC").ToList();
            }
            ViewBag.Title = "Справочник техники организации";
            //ViewBag.Message = "";

            return View(printers);
        }

        // Printers Create
        public ActionResult Create(int? ID = null)
        {
            var model = new CreateViewModel();

            using (IDbConnection db = new SqlConnection(constr))
            {
                if (ID != null)
                {
                    model = db.Query<CreateViewModel>($"select * from dbo.Printer where ID = {ID}").FirstOrDefault();
                    model.PrinterBrandID = db.Query<int>($"select PrinterBrandID from dbo.PrintModels where ID = {model.PrinterModelID}").FirstOrDefault();
                    model.GetPrintersBrands = listsService.GetPrintersBrands();
                    model.GetPrintersModels = listsService.GetPrintersModels(model.PrinterBrandID);
                }
                else
                {
                    model.PurchaseDate = DateTime.Now;
                    model.GetPrintersBrands = listsService.GetPrintersBrands();
                    model.GetPrintersModels = listsService.GetPrintersModels(int.Parse(model.GetPrintersBrands.FirstOrDefault().Value));
                }              
            };
            return View(model);
        }

        //POST: Printers Create
        [HttpPost]
        public ActionResult Create(CreateViewModel model)
        {
            try
            {
                AddPrinter(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                model.GetPrintersBrands = listsService.GetPrintersBrands();
                model.GetPrintersModels = listsService.GetPrintersModels(model.PrinterBrandID);
                return View(model);
            }

        }

        //SEND: Printers Create
        public void AddPrinter(CreateViewModel model)
        {
            using (IDbConnection db = new SqlConnection(constr))
            {
                string price = model.Price?.ToString(CultureInfo.InvariantCulture) ?? "NULL";

                if (model.ID != 0)
                {
                    string query = $"UPDATE dbo.Printer set PrinterModelID = {model.PrinterModelID}, InventoryNumber = '{model.InventoryNumber}', PurchaseDate = '{model.PurchaseDate:yyyy-MM-dd}', Price = {price} WHERE ID = {model.ID}";
                    db.Query(query);
                }
                else
                {
                    string query = $"insert into dbo.Printer (PrinterModelID, InventoryNumber, PurchaseDate, Price) values ({model.PrinterModelID}, '{model.InventoryNumber}', '{model.PurchaseDate:yyyy-MM-dd}', {price}); select scope_identity();";
                    int? printerID = db.Query<int?>(query).FirstOrDefault();

                    if (printerID != null)
                    {
                        db.Query($"INSERT INTO dbo.Movement (PrinterID,BuildingOldID,CabinetOldID,BuildingNewID,CabinetNewID,PerformerID,MoveDate) VALUES ({printerID}, 9, 44, 2, 27, 1, '{model.PurchaseDate:yyyy-MM-dd}')");
                    }
                }
                
                
            }
            return;
        }

        [HttpPost]
        public ActionResult GetPrintersModels(int? id)
        {
            var printermodels = listsService.GetPrintersModels(id);
            return Json(printermodels);
        }
        
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var printerDelete = new PrintersView();
            using (IDbConnection db = new SqlConnection(constr))
            {
                printerDelete = db.Query<PrintersView>($"select * from dbo.printersView where ID = {id}").FirstOrDefault();
            }
            return View(printerDelete);
        }

        [HttpPost]
        public ActionResult Delete(PrintersView model)
        {
            using (IDbConnection db = new SqlConnection(constr))
            {
                db.Query($"update dbo.Printer set IsDeleted = 1 where id = {model.ID}");
            }
            return RedirectToAction("Index", "Printers");
        }
    }
}