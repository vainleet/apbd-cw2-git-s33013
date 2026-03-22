namespace EquipmentRental.Models;

public abstract class User
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";

    // To be defined in subclasses
    public abstract int MaxRentals { get; }
    public abstract string UserType { get; }
}
