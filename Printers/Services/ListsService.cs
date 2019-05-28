using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace Printers.Services
{
    public class ListsService
    {
        string constr = ConfigurationManager.ConnectionStrings["DC"].ConnectionString;

        // Получение списка зданий
        public List<SelectListItem> GetBuildings()
        {
            var building = new List<SelectListItem>();
            List<Buildings> model = new List<Buildings>();
            using (IDbConnection db = new SqlConnection(constr))
            {
                model = db.Query<Buildings>($"Select * from dbo.Buildings where IsDeleted = 0 order by id").ToList();
            }
            foreach (var item in model)
            {
                building.Add(new SelectListItem
                {
                    Text = item.Building,
                    Value = item.ID.ToString()
                });
            }
            return building;
        }

        // Получение списка кабинетов
        public List<SelectListItem> GetCabinets(int? id)
        {
            var cabinet = new List<SelectListItem>();
            List<Cabinets> model = new List<Cabinets>();
            using (IDbConnection db = new SqlConnection(constr))
            {
                model = db.Query<Cabinets>("Select * from dbo.Cabinets where (@BuildingID = 0 or BuildingID = @BuildingID) AND IsDeleted = 0 order by Number",
                        new { BuildingID = id ?? 0 }).ToList();
            }
            foreach (var item in model)
            {
                cabinet.Add(new SelectListItem
                {
                    Text = item.Number,
                    Value = item.ID.ToString()
                });
            }
            return cabinet;
        }

        // Получение списка картриджей
        public List<SelectListItem> GetCartridges()
        {
            var cartidge = new List<SelectListItem>();
            List<Cartridges> model = new List<Cartridges>();
            using (IDbConnection db = new SqlConnection(constr))
            {
                model = db.Query<Cartridges>($"Select * from dbo.Cartridges where IsDeleted = 0 order by id").ToList();
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

        // Получение списка сотрудников
        public List<SelectListItem> GetPerformers()
        {
            var user = new List<SelectListItem>();
            List<Performers> model = new List<Performers>();
            using (IDbConnection db = new SqlConnection(constr))
            {
                model = db.Query<Performers>($"Select * from dbo.Performers where IsDeleted = 0 order by Name").ToList();
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

        // Получение списка брендов принтеров
        public List<SelectListItem> GetPrintersBrands()
        {
            var printerbrand = new List<SelectListItem>();
            List<PrintBrands> model = new List<PrintBrands>();
            using (IDbConnection db = new SqlConnection(constr))
            {
                model = db.Query<PrintBrands>($"Select * from dbo.PrintBrands where IsDeleted = 0").ToList();
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

        // Получени списка моделей принтеров
        public List<SelectListItem> GetPrintersModels(int? id = null)
        {
            var printermodel = new List<SelectListItem>();
            List<PrintModels> model = new List<PrintModels>();
            using (IDbConnection db = new SqlConnection(constr))
            {
                model = db
                    .Query<PrintModels>(
                        $"Select * from dbo.PrintModels where (@BrandId = 0 or PrinterBrandID = @BrandId) AND IsDeleted = 0 order by id",
                        new { BrandId = id ?? 0 })
                    .ToList();
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

        

        // Получени списка статусов
        public List<SelectListItem> GetStatus()
        {
            var status = new List<SelectListItem>();
            using (IDbConnection db = new SqlConnection(constr))
            {
                status = db.Query<Status>($"Select * from dbo.Status where IsDeleted = 0 order by id")
                    .Select(s => new SelectListItem { Text = s.StatusMsg, Value = s.ID.ToString() })
                    .ToList();
            }
            return status;
        }
    }
}