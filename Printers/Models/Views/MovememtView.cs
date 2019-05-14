using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

public class MovementView
{
    public int ID { get; set; }

    [Display(Name = "Бренд")]
    public string PrinterBrand { get; set; }

    [Display(Name = "Модель")]
    public string PrinterModel { get; set; }

    [Display(Name = "Старое здание")]
    public string BuildingOld { get; set; }

    [Display(Name = "Старый кабинет")]
    public string CabinetOld { get; set; }

    [Display(Name = "Новое здание")]
    public string BuildingNew { get; set; }

    [Display(Name = "Новый кабинет")]
    public string CabinetNew { get; set; }

    [Display(Name = "Ответственный")]
    public string Name { get; set; }

    [Display(Name = "Дата")]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.mm.yyyy}")]
    public DateTime MoveDate { get; set; }

    [Display(Name = "IP-адрес")]
    public string IP { get; set; }
}
