namespace EquipmentRental.Models;

public abstract class User
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";
    public abstract int MaxRentals { get; }
    public abstract string UserType { get; }

    public override string ToString() =>
        $"[{UserType}] {FullName} | ID: {Id.ToString()[..8]} | Max rentals: {MaxRentals}";
}