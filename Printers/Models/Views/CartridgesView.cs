using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

public class CartridgesView
{
    public int ID { get; set; }

    [Display(Name = "Бренд")]
    public string PrinterBrand { get; set; }

    [Display(Name = "Модель")]
    public string PrinterModel { get; set; }

    [Display(Name = "Картрижд")]
    public string CartdgeModel { get; set; }

    [Display(Name = "Цвет")]
    public string CartridgeColor { get; set; }
}
