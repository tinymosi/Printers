using System.ComponentModel.DataAnnotations;

public class PrintBrands
{
    public int ID { get; set; }

    [Display(Name = "Бренд")]
    public string PrinterBrand { get; set; }
}
