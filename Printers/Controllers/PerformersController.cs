using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace Printers.Controllers
{
    public class PerformersController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["DC"].ConnectionString;

        // Performers
        public ActionResult Index()
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
        public ActionResult Create()
        {
            var performercreate = new Performers();

            ViewBag.Title = "Сотрудники";
            ViewBag.Message = "Добавление нового сотрудника";

            return View();
        }

        [HttpPost]
        public ActionResult Create(string Name)
        {
            var performercreate = new Performers();
            using (IDbConnection db = new SqlConnection(constr))
            {
                performercreate = db.Query<Performers>($"insert into dbo.Performers ([Name]) values ('{Name}')").FirstOrDefault();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
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
        public ActionResult Edit(string Name, int id)
        {
            var performeredit = new Performers();
            using (IDbConnection db = new SqlConnection(constr))
            {
                performeredit = db.Query<Performers>($"update dbo.Performers set [Name] = '{Name}' where ID = {id}").FirstOrDefault();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id, string Name)
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
        public ActionResult Delete(int id)
        {
            var performerdelete = new Performers();
            using (IDbConnection db = new SqlConnection(constr))
            {
                performerdelete = db.Query<Performers>($"delete from dbo.Performers where id = {id}").FirstOrDefault();
            }
            return RedirectToAction("Index");
        }
    }
}