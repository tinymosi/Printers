using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

public class CabinetsView
{
    [Display(Name = "Здание")]
    public string Building { get; set; }

    [Display(Name = "Кабинет")]
    public string Number { get; set; }
}
