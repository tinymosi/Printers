using System.Collections.Generic;
using System.Web.Mvc;

namespace Printers.Models.ListModels
{
    public class MovementList
    {
        public Movement Movement { get; set; }

        public List<SelectListItem> GetBuilding { get; set; }

        public List<SelectListItem> GetCabinet { get; set; }

        public List<SelectListItem> GetPerformer { get; set; }

        public List<SelectListItem> GetStatus { get; set; }
    }
}