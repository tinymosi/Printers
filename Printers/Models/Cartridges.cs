using System.ComponentModel.DataAnnotations;

public class Cartridges
{
    public int ID { get; set; }

    public int PrinterModelID { get; set; }

    [Display(Name = "Картридж")]
    public string CartridgeModel { get; set; }

    [Display(Name = "Цвет")]
    public string CartridgeColor { get; set; }

}
