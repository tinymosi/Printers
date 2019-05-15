using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Printers.Models.ListModels
{
    public class CabinetsList
    {
        public Cabinets Cabinets{ get; set; }
        public List<SelectListItem> GetBuildings { get; set; }

        public string Number { get; set; }
    }
}