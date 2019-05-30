using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Printers.ViewModels.Models
{
    public class CreateViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Выберите Бренд")]
        public int PrinterBrandID { get; set; }

        public List<SelectListItem> GetPrintersBrands { get; set; }

        [Display(Name = "Введите название модели")]
        [Required(ErrorMessage = "Поле является обязательным")]
        [StringLength(150, ErrorMessage = "Максимальная длина {1} символов")]
        public string PrinterModel { get; set; }

    }
}