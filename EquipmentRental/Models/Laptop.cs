namespace EquipmentRental.Models;

public class Laptop : Equipment
{
    public int RamGb { get; set; }
    public string Cpu { get; set; } = string.Empty;

    public override string TypeLabel => "Laptop";

    public override string ToString() =>
        base.ToString() + $" | RAM: {RamGb}GB, CPU: {Cpu}";
}