using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Printers.ViewModels.Models
{
    public class PrinterModelView
    {
        public int ID { get; set; }

        public string PrinterBrand { get; set; }

        public string PrinterModel { get; set; }
    }
}