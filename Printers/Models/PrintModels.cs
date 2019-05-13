using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

public class PrintModels
{
    public int ID { get; set; }

    public int PrinterBrandID { get; set; }

    [Display(Name = "Модель")]
    public string PrinterModel { get; set; }
}
