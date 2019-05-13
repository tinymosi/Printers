using System;

public class CabinetsView
{
    [Display(Name = "Здание")]
    public string Building { get; set; }

    [Display(Name = "Кабинет")]
    public string Number { get; set; }
}
