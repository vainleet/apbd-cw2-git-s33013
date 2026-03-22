namespace EquipmentRental.Models;

public class Rental
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public User User { get; init; } = null!;
    public Equipment Equipment { get; init; } = null!;

    public DateTime RentDate { get; init; }
    public DateTime DueDate { get; init; }
    public DateTime? ReturnDate { get; set; }

    public decimal Penalty { get; set; }

    public bool IsActive => ReturnDate == null;
}
