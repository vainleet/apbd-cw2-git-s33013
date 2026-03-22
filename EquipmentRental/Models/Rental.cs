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

    public bool IsOverdue(DateTime? asOf = null) =>
        IsActive && DueDate < (asOf ?? DateTime.Now);

    public override string ToString()
    {
        string status = ReturnDate.HasValue
            ? $"Returned: {ReturnDate.Value:dd.MM.yyyy}" + (Penalty > 0 ? $" | Penalty: {Penalty} PLN" : "")
            : (IsOverdue() ? "OVERDUE" : "Active");

        return $"Rental {Id.ToString()[..8]} | {User.FullName} → {Equipment.Name} " +
               $"| Rented: {RentDate:dd.MM.yyyy} | Due: {DueDate:dd.MM.yyyy} | {status}";
    }
}