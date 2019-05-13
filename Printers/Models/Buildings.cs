using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

public class Buildings
{
    public int ID { get; set; }

    [Display(Name = "Название здания")]
    public string Building { get; set; }

}
