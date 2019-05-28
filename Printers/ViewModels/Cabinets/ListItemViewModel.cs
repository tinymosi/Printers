using System.ComponentModel.DataAnnotations;

namespace Printers.ViewModels.Cabinets
{
    public class ListItemViewModel
    {
        public int ID { get; set; }

        [Display(Name = "Здание")]
        public string Building { get; set; }

        [Display(Name = "Кабинет")]
        public string Number { get; set; }
    }
}