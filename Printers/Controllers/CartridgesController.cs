using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Printers.Services;

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
                cartridges = db.Query<CartridgesView>("select * from dbo.CartridgesView order by id").ToList();
            }
            ViewBag.Title = "Справочник подходящих расходников";
            //ViewBag.Message = "";

            return View(cartridges);
        }
    }
}