using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Printers.ViewModels.Printers
{
    public class ListItemViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Бренд")]
        public string PrinterBrand { get; set; }

        [Display(Name = "Модель")]
        public string PrinterModel { get; set; }

        [Display(Name = "Инвентарный номер")]
        public string InventoryNumber { get; set; }

        [Display(Name = "Здание")]
        public string Building { get; set; }

        [Display(Name = "Кабинет")]
        public string Number { get; set; }

        [Display(Name = "IP-адрес")]
        public string IP { get; set; }

        [Display(Name = "Статус")]
        public string StatusMsg { get; set; }

        [Display(Name = "Дата покупки")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "Цена")]
        public string Price { get; set; }
    }
}