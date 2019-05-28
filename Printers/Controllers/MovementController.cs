using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using Printers.Services;
using Printers.Models.ListModels;
using System;

namespace Printers.Controllers
{
    public class MovementController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["DC"].ConnectionString;

        ListsService listsService = new ListsService();

        // Movement
        public ActionResult Index()
        {
            var movement = new List<MovementView>();

            using (IDbConnection db = new SqlConnection(constr))
            {
                movement = db.Query<MovementView>("select * from dbo.MovementView order by MoveDate desc").ToList();
            }
            ViewBag.Title = "Движение техники";
            ViewBag.Message = "История джижения техники. Где сейчас находится принтер, кто и когда его туда перенес.";

            return View(movement);
        }

        // Printers select
        public ActionResult SelectPrinter()
        {
            var printers = new List<PrintersView>();

            using (IDbConnection db = new SqlConnection(constr))
            {
                printers = db.Query<PrintersView>("select * from dbo.PrintersView ORDER BY ID DESC").ToList();
            }
            ViewBag.Title = "Справочник техники организации";
            ViewBag.Message = "Выберите устройство";

            return View(printers);
        }

        public ActionResult Create(int PrinterID)
        {
            using (IDbConnection db = new SqlConnection(constr))
            {
                var move = db.Query<Movement>($"select * from dbo.movement where PrinterID = {PrinterID} order by movedate desc").FirstOrDefault();

                var buildings = listsService.GetBuildings();
                int buildingId = buildings.Any() ? int.Parse(buildings.First().Value) : 0;

                if (move != null)
                {
                    var model = new MovementList()
                    {
                        Movement = new Movement()
                        {
                            MoveDate = DateTime.Now,
                            PrinterID = move.PrinterID,
                            BuildingOldID= move.BuildingNewID,
                            CabinetOldID = move.CabinetNewID,
                            IP = move.IP
                        },
                        GetBuilding = buildings,
                        GetCabinet = buildingId == 0 ? new List<SelectListItem>() : listsService.GetCabinets(buildingId),
                        GetPerformer = listsService.GetPerformers(),
                        GetStatus = listsService.GetStatus(),
                    };
                    return View(model);
                }   
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Create(MovementList model)
        {
            try
            {
                AddMove(model.Movement);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                model.GetBuilding = listsService.GetBuildings();
                model.GetCabinet = listsService.GetCabinets(model.Movement.BuildingNewID);
                model.GetPerformer = listsService.GetPerformers();
                return View(model);
            }

        }

        //SEND: Printers Create
        public void AddMove(Movement model)
        {
            using (IDbConnection db = new SqlConnection(constr))
            {
                model = db.Query<Movement>($"INSERT INTO dbo.Movement (PrinterID,BuildingOldID,CabinetOldID,BuildingNewID,CabinetNewID,PerformerID,MoveDate,IP) VALUES ({model.PrinterID}, {model.BuildingOldID}, {model.CabinetOldID}, {model.BuildingNewID}, {model.CabinetNewID}, {model.PerformerID}, '{model.MoveDate:yyyy-MM-dd HH:mm:ss}', '{model.IP}')").FirstOrDefault();
            }
            return;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var moveDelete = new MovementView();
            using (IDbConnection db = new SqlConnection(constr))
            {
                moveDelete = db.Query<MovementView>($"select * from dbo.movementView where ID = {id}").FirstOrDefault();
            }
            return View(moveDelete);
        }

        [HttpPost]
        public ActionResult Delete(MovementView model)
        {
            using (IDbConnection db = new SqlConnection(constr))
            {
                db.Query($"update dbo.Movement set IsDeleted = 1 where id = {model.ID}");
            }
            return RedirectToAction("Index", "Movement");
        }
    }
}