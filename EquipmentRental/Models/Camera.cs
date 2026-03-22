namespace EquipmentRental.Models;

public class Camera : Equipment
{
    public int Megapixels { get; set; }
    public bool HasStabilization { get; set; }

    public override string TypeLabel => "Camera";

    public override string ToString() =>
        base.ToString() + $" | {Megapixels}MP, Stabilization: {(HasStabilization ? "Yes" : "No")}";
}