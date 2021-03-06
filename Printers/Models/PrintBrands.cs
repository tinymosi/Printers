﻿using System.ComponentModel.DataAnnotations;

public class PrintBrands
{
    public int ID { get; set; }

    [Display(Name = "Бренд")]
    [Required(ErrorMessage = "Введите название бренда")]
    public string PrinterBrand { get; set; }

    public bool IsDeleted { get; set; }

}
