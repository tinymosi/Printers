using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

public class Performers
{
    public int ID { get; set; }

    [Display(Name = "Сотрудник")]
    public string Name { get; set; }

}
