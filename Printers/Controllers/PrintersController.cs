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
                printers = db.Query<PrintersView>("select * from dbo.PrintersView order by id desc").ToList();
            }
            ViewBag.Title = "Справочник техники организации";
            //ViewBag.Message = "";

            return View(printers);
        }

        // Printers Create
        public ActionResult Create()
        {
            var model = new PrintersList()
            {
                Printer = new Printer() { PurchaseDate = DateTime.Now },
                GetPrintersBrands = listsService.GetPrintersBrands(),
                GetPrintersModels = new List<SelectListItem>(),
                GetStatus = listsService.GetStatus(),
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
            catch
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
    }
}