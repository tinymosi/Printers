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

        public ActionResult Index()
        {
            ViewBag.Title = "Кабинеты";
            ViewBag.Message = "Справочник кабинетов организации.";
            return View();
        }

        public ActionResult Brands()
        {
            var brands = new List<PrintBrands>();

            using (IDbConnection db = new SqlConnection(constr))
            {
                brands = db.Query<PrintBrands>("SELECT PrinterBrand FROM dbo.PrintBrands WHERE IsDeleted = 0").ToList();
            }
            return PartialView("_Brands", brands);
        }

        public ActionResult Models()
        {
            var models = new List<PrinterModelView>();

            using (IDbConnection db = new SqlConnection(constr))
            {
                models = db.Query<PrinterModelView>("SELECT dbo.PrintBrands.PrinterBrand, dbo.PrintModels.PrinterModel " +
                    "FROM dbo.PrintBrands INNER JOIN " +
                         "dbo.PrintModels ON dbo.PrintBrands.ID = dbo.PrintModels.PrinterBrandID").ToList();
            }
            return PartialView("_Models", models);
        }
    }
}