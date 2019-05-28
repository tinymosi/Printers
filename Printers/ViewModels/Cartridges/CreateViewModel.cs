using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Printers.ViewModels.Cartridges
{
    public class CreateViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Выберите модель")]
        [Required(ErrorMessage = "Поле является обязательным")]
        public int PrinterModelID { get; set; }

        public List<SelectListItem> GetPrintersModels { get; set; }

        [Display(Name = "Введите модель картриджа")]
        [Required(ErrorMessage = "Поле является обязательным")]
        [StringLength(100, ErrorMessage = "Максимальная длина {1} символов")]
        public string CartridgeModel { get; set; }

        [Display(Name = "Введите цвет")]
        [StringLength(100, ErrorMessage = "Максимальная длина {1} символов")]
        public string CartridgeColor { get; set; }
    }
}