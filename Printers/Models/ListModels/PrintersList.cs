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
        public List<SelectListItem> GetStatus { get; set; }
    }
}