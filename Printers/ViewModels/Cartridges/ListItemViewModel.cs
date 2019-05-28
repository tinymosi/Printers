using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Printers.ViewModels.Cartridges
{
    public class ListItemViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Модель принтера")]
        public string PrinterModel { get; set; }

        [Display(Name = "Модель картриджа")]
        public string CartridgeModel { get; set; }

        [Display(Name ="Цвет картриджа")]
        public string CartridgeColor { get; set; }
    }
}