using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

public class Printer
{
    public int ID { get; set; }

    public int PrinterBrandID { get; set; }

    public int PrinterModelID { get; set; }

    [Display(Name = "Инв. номер")]
    public string InventoryNumber { get; set; }

    [Display(Name = "Статус")]
    public int StatusID { get; set; }

    [Display(Name = "IP-адрес")]
    public string IP { get; set; }

    [Display(Name = "Дата покупки")]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.mm.yyyy}")]
    public DateTime PurchaseDate { get; set; }

    [Display(Name = "Цена")]
    public decimal Price { get; set; }

}
