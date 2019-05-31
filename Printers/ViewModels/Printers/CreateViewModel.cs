using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Printers.ViewModels.Printers
{
    public class CreateViewModel
    {
        public int ID { get; set; }

        public int PrinterBrandID { get; set; }

        public int PrinterModelID { get; set; }

        [Display(Name = "Инв. номер")]
        public string InventoryNumber { get; set; }

        [Display(Name = "Дата покупки")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "Цена")]
        public decimal? Price { get; set; }

        public bool IsDeleted { get; set; }

        public List<SelectListItem> GetPrintersBrands { get; set; }

        public List<SelectListItem> GetPrintersModels { get; set; }
    }
}