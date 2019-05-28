using System.ComponentModel.DataAnnotations;

public class Cabinets
{
    public int ID { get; set; }

    public int BuildingID { get; set; }

    public string Number { get; set; }

    public bool IsDeleted { get; set; }

}
