using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Printers.ViewModels.Cabinets
{
    public class CreateViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Выберите здание")]
        public int BuildingID { get; set; }

        public List<SelectListItem> GetBuildings { get; set; }

        [Display(Name = "Введите кабинет")]
        [Required(ErrorMessage = "Поле является обязательным")]
        [StringLength(50, ErrorMessage = "Максимальная длина {1} символов")]
        public string Number { get; set; }
    }
}