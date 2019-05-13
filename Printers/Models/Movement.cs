using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

public class Movement
{
    public int ID { get; set; }

    public int PrinterID { get; set; }

    public int NewBuildingID { get; set; }

    public int NewCabinetID { get; set; }

    public int PerformerID { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.mm.yyyy}")]
    public DateTime MoveDate { get; set; }

}
