﻿using System.ComponentModel.DataAnnotations;

public class Buildings
{
    public int ID { get; set; }

    [Display(Name = "Название здания")]
    public string Building { get; set; }

    public bool IsDeleted { get; set; }
}
