﻿using System;
using System.ComponentModel.DataAnnotations;

public class Printer
{
    public int ID { get; set; }

    public int PrinterBrandID { get; set; }

    public int PrinterModelID { get; set; }

    [Display(Name = "Инв. номер")]
    public string InventoryNumber { get; set; }

    [Display(Name = "Дата покупки")]
    [DataType(DataType.DateTime)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
    public DateTime PurchaseDate { get; set; }

    [Display(Name = "Цена")]
    public decimal Price { get; set; }

    public bool IsDeleted { get; set; }
}
