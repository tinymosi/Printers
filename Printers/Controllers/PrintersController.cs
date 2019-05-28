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
            var model = new PrintersList();

            using (IDbConnection db = new SqlConnection(constr))
            {
                if (ID != null)
                {
                    model = db.Query<PrintersList>($"select * from dbo.Printers where ID = {ID}").FirstOrDefault();
                }
                model.Printer = new Printer() { PurchaseDate = DateTime.Now };
                model.GetPrintersBrands = listsService.GetPrintersBrands();
                model.GetPrintersModels = new List<SelectListItem>();
                model.GetStatus = listsService.GetStatus();
            };
            return View(model);
        }

        //POST: Printers Create
        [HttpPost]
        public ActionResult Create(PrintersList model)
        {
            try
            {
                AddPrinter(model.Printer);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                model.GetPrintersBrands = listsService.GetPrintersBrands();
                model.GetPrintersModels = listsService.GetPrintersModels(model.Printer.PrinterBrandID);
                model.GetStatus = listsService.GetStatus();
                return View(model);
            }

        }

        //SEND: Printers Create
        public void AddPrinter(Printer model)
        {
            using (IDbConnection db = new SqlConnection(constr))
            {
                model = db.Query<Printer>($"insert into dbo.Printer (PrinterModelID, InventoryNumber, PurchaseDate, Price) values ({model.PrinterModelID}, {model.InventoryNumber}, {model.PurchaseDate}, {model.Price})").FirstOrDefault();
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
                db.Query($"update dbo.printers set IsDeleted = 1 where id = {model.ID}");
            }
            return RedirectToAction("Index", "Printers");
        }
    }
}