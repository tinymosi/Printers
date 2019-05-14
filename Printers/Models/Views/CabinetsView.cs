using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

public class CabinetsView
{
    public int ID { get; set; }

    [Display(Name = "Здание")]
    public string Building { get; set; }

    [Display(Name = "Кабинет")]
    public string Number { get; set; }
}
