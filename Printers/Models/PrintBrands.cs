using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

public class PrintBrands
{
    public int ID { get; set; }

    [Display(Name = "Бренд")]
    public string PrinterBrand { get; set; }
}
