using System;

public class CartridgesView
{
    [Display(Name = "Бренд")]
    public string PrinterBrand { get; set; }

    [Display(Name = "Модель")]
    public string PrinterModel { get; set; }

    [Display(Name = "Картрижд")]
    public string CartdgeModel { get; set; }

    [Display(Name = "Цвет")]
    public string CartridgeColor { get; set; }
}
