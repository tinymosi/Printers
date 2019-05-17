using System.Collections.Generic;
using System.Web.Mvc;

namespace Printers.Models.ListModels
{
    public class PrintersList
    {
        public Printer Printer { get; set; }

        public List<SelectListItem> GetPrintersBrands { get; set; }

        public List<SelectListItem> GetPrintersModels { get; set; }
        
        // Списки зданий не нужны и должны быть NULL в модели
        //public List<SelectListItem> GetBuildings { get; set; }
        //public List<SelectListItem> GetCabinets { get; set; }

        public List<SelectListItem> GetStatus { get; set; }
    }
}