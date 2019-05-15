using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

public class PrintersView
{
    public int ID { get; set; }

    [Display(Name = "Бренд")]
    public string PrinterBrand { get; set; }

    [Display(Name = "Модель")]
    public string PrinterModel { get; set; }

    [Display(Name = "Инв. номер")]
    public string InventoryNumber { get; set; }

    [Display(Name = "Здание")]
    public string Building { get; set; }

    [Display(Name = "Кабинет")]
    public string Number { get; set; }

    [Display(Name = "Статус")]
    public string StatusMsg { get; set; }

    [Display(Name = "IP-адрес")]
    public string IP { get; set; }

    [Display(Name = "Дата покупки")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
    public DateTime PurchaseDate { get; set; }

    [Display(Name = "Цена")]
    public string Price { get; set; }
}
