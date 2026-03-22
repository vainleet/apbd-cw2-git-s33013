namespace EquipmentRental.Models;

public abstract class Equipment
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = true;

    public abstract string TypeLabel { get; }
}
