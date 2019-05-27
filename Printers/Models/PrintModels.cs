using System.ComponentModel.DataAnnotations;

public class PrintModels
{
    public int ID { get; set; }

    public int PrinterBrandID { get; set; }

    [Display(Name = "Модель")]
    public string PrinterModel { get; set; }

    public bool IsDeleted { get; set; }
}
