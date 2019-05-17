using System.ComponentModel.DataAnnotations;

public class CartridgesView
{
    public int ID { get; set; }

    [Display(Name = "Бренд")]
    public string PrinterBrand { get; set; }

    [Display(Name = "Модель")]
    public string PrinterModel { get; set; }

    [Display(Name = "Картридж")]
    public string CartridgeModel { get; set; }

    [Display(Name = "Цвет")]
    public string CartridgeColor { get; set; }
}
