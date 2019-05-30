using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Printers.ViewModels.Models
{
    public class PrinterModelView
    {
        public int ID { get; set; }

        [Display(Name = "Бренд")]
        [Required(ErrorMessage = "Выберте бренд")]
        public string PrinterBrand { get; set; }

        public List<SelectListItem> GetBrands { get; set; }

        [Display(Name = "Модель")]
        [Required(ErrorMessage = "Введите название модели")]
        public string PrinterModel { get; set; }

        public bool IsDeleted { get; set; }

    }
}