using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

public class Cabinets
{
    public int ID { get; set; }

    public int BuildingID { get; set; }

    [Required(ErrorMessage = "Введите кабинет")]
    [Display(Name = "Номер кабинета")]
    public string Name { get; set; }
}
