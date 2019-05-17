using System;
using System.ComponentModel.DataAnnotations;

public class Movement
{
    public int ID { get; set; }

    public int PrinterID { get; set; }

    public int BuildingOldID { get; set; }

    public int CabinetOldID { get; set; }

    public int BuildingNewID { get; set; }

    public int CabinetNewID { get; set; }

    public int StatusID { get; set; }

    public int PerformerID { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
    public DateTime MoveDate { get; set; }

    public string IP { get; set; }

}
