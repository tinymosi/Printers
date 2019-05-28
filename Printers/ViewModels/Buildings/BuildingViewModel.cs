using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Printers.ViewModels.Buildings
{
    public class BuildingViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Здание")]
        [Required(ErrorMessage = "Необходимо указать название")]
        public string Building { get; set; }
    }
}