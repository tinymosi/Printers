using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Printers.Models.ListModels
{
    public class PrintersList
    {
        public Printer Printer { get; set; }

        public List<SelectListItem> GetPrintersBrands { get; set; }
        public List<SelectListItem> GetPrintersModels { get; set; }

        public string InventoryNumber { get; set; }

        // Списки зданий не нужны и должны быть NULL в модели
        //public List<SelectListItem> GetBuildings { get; set; }
        //public List<SelectListItem> GetCabinets { get; set; }

        public List<SelectListItem> GetStatus { get; set; }

        public string IP { get; set; }

        public DateTime PurchaseDate { get; set; }

        public decimal Price { get; set; }
    }
}