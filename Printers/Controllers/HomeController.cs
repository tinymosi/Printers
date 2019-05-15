using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;


namespace Printers.Controllers
{
    public class HomeController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["DC"].ConnectionString;

        public ActionResult Index()
        {
            return View();
        }

        // Movement
        public ActionResult Movement()
        {
            var movement = new List<MovementView>();

            using (IDbConnection db = new SqlConnection(constr))
            {
                movement = db.Query<MovementView>("select * from dbo.MovementView order by id desc").ToList();
            }
            ViewBag.Title = "Движение техники";
            ViewBag.Message = "История джижения техники. Где сейчас находится принтер, кто и когда его туда перенес.";

            return View(movement);
        }

        // Printers select
        public ActionResult PrintersSelect()
        {
            var printers = new List<PrintersView>();

            using (IDbConnection db = new SqlConnection(constr))
            {
                printers = db.Query<PrintersView>("select * from dbo.PrintersView order by id desc").ToList();
            }
            ViewBag.Title = "Справочник техники организации";
            ViewBag.Message = "Выберите устройство";

            return View(printers);
        }



        // Printers
        public ActionResult Printers()
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

        // Cartridges
        public ActionResult Cartridges()
        {
            var cartridges = new List<CartridgesView>();

            using (IDbConnection db = new SqlConnection(constr))
            {
                cartridges = db.Query<CartridgesView>("select * from dbo.CartridgesView order by id").ToList();
            }
            ViewBag.Title = "Справочник подходящих расходников";
            //ViewBag.Message = "";

            return View(cartridges);
        }

        // Cabinets
        public ActionResult Cabinets()
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


        // Performers
        public ActionResult Performers()
        {
            var performers = new List<Performers>();
            using (IDbConnection db = new SqlConnection(constr))
            {
                performers = db.Query<Performers>("select * from dbo.Performers order by id").ToList();
            }
            ViewBag.Title = "Сотрудники";
            ViewBag.Message = "Список ответственных сотрудников организации.";

            return View(performers);
        }

        [HttpGet]
        public ActionResult PerformersCreate()
        {
            var performercreate = new Performers();

            ViewBag.Title = "Сотрудники";
            ViewBag.Message = "Добавление нового сотрудника";

            return View();
        }

        [HttpPost]
        public ActionResult PerformersCreate(string Name)
        {
            var performercreate = new Performers();
            using (IDbConnection db = new SqlConnection(constr))
            {
                performercreate = db.Query<Performers>($"insert into dbo.Performers ([Name]) values ('{Name}')").FirstOrDefault();
            }
            return RedirectToAction("Performers");
        }

        [HttpGet]
        public ActionResult PerformersEdit(int id)
        {
            var performeredit = new Performers();
            using (IDbConnection db = new SqlConnection(constr))
            {
                performeredit = db.Query<Performers>($"select * from dbo.Performers where id = {id}").FirstOrDefault();
            }
            ViewBag.Title = "Сотрудники";
            ViewBag.Message = "Редактирование данных сотрудника";
            return View(performeredit);
        }

        [HttpPost]
        public ActionResult PerformersEdit(string Name, int id)
        {
            var performeredit = new Performers();
            using (IDbConnection db = new SqlConnection(constr))
            {
                performeredit = db.Query<Performers>($"update dbo.Performers set [Name] = '{Name}' where ID = {id}").FirstOrDefault();
            }
            return RedirectToAction("Performers");
        }

        public ActionResult PerformersDetails(int id)
        {
            var performerdetails = new Performers();
            using (IDbConnection db = new SqlConnection(constr))
            {
                performerdetails = db.Query<Performers>($"select * from dbo.Performers where id = {id}").FirstOrDefault();
            }
            return View(performerdetails);
        }

        [HttpGet]
        public ActionResult PerformersDelete(int id, string Name)
        {
            var performerdelete = new Performers();
            using (IDbConnection db = new SqlConnection(constr))
            {
                performerdelete = db.Query<Performers>($"select * from dbo.Performers where id = {id}").FirstOrDefault();
            }
            ViewBag.Title = "Сотрудники";
            ViewBag.Message = "Удалить сотрудника?";
            return View(performerdelete);
        }

        [HttpPost]
        public ActionResult PerformersDelete(int id)
        {
            var performerdelete = new Performers();
            using (IDbConnection db = new SqlConnection(constr))
            {
                performerdelete = db.Query<Performers>($"delete from dbo.Performers where id = {id}").FirstOrDefault();
            }
            return RedirectToAction("Performers");
        }



        //Получение списка сотрудников
        public List<SelectListItem> GetPerformers()
        {
            var user = new List<SelectListItem>();
            List<Performers> model = new List<Performers>();
            using (IDbConnection db = new SqlConnection(constr))
            {
                model = db.Query<Performers>($"Select * from dbo.Performers order by Name").ToList();
            }
            foreach (var item in model)
            {
                user.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.ID.ToString()
                });
            }
            return user;
        }

        //Получение списка брендов принтеров
        public List<SelectListItem> GetPrintersBrands()
        {
            var printerbrand = new List<SelectListItem>();
            List<PrintBrands> model = new List<PrintBrands>();
            using (IDbConnection db = new SqlConnection(constr))
            {
                model = db.Query<PrintBrands>($"Select * from dbo.PrintBrands").ToList();
            }
            foreach (var item in model)
            {
                printerbrand.Add(new SelectListItem
                {
                    Text = item.PrinterBrand,
                    Value = item.ID.ToString()
                });
            }
            return printerbrand;
        }

        //Получени списка моделей принтеров
        public List<SelectListItem> GetPrintersModels()
        {
            var printermodel = new List<SelectListItem>();
            List<PrintModels> model = new List<PrintModels>();
            using (IDbConnection db = new SqlConnection(constr))
            {
                model = db.Query<PrintModels>($"Select * from dbo.PrintModels").ToList();
            }
            foreach (var item in model)
            {
                printermodel.Add(new SelectListItem
                {
                    Text = item.PrinterModel,
                    Value = item.ID.ToString()
                });
            }
            return printermodel;
        }

        //Получение списка кабинетов
        public List<SelectListItem> GetCabinets()
        {
            var cabinet = new List<SelectListItem>();
            List<Cabinets> model = new List<Cabinets>();
            using (IDbConnection db = new SqlConnection(constr))
            {
                model = db.Query<Cabinets>($"Select * from dbo.Cabinets order by Name").ToList();
            }
            foreach (var item in model)
            {
                cabinet.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = item.ID.ToString()
                });
            }
            return cabinet;
        }

        //Получение списка картриджей
        public List<SelectListItem> GetCartridges()
        {
            var cartidge = new List<SelectListItem>();
            List<Cartridges> model = new List<Cartridges>();
            using (IDbConnection db = new SqlConnection(constr))
            {
                model = db.Query<Cartridges>($"Select * from dbo.Cartridges order by id").ToList();
            }
            foreach (var item in model)
            {
                cartidge.Add(new SelectListItem
                {
                    Text = item.CartridgeModel,
                    Value = item.ID.ToString()
                });
            }
            return cartidge;
        }
    }
}