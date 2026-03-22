using EquipmentRental.Config;
using EquipmentRental.Interfaces;
using EquipmentRental.Models;

namespace EquipmentRental.Services;

public class RentalService : IRentalService
{
    private readonly List<User> _users = new();
    private readonly List<Equipment> _equipment = new();
    private readonly List<Rental> _rentals = new();

    public void AddUser(User user)
    {
        ArgumentNullException.ThrowIfNull(user);
        _users.Add(user);
    }

    public List<User> GetAllUsers() => new(_users);

    public void AddEquipment(Equipment equipment)
    {
        ArgumentNullException.ThrowIfNull(equipment);
        _equipment.Add(equipment);
    }

    public List<Equipment> GetAllEquipment() => new(_equipment);

    public List<Equipment> GetAvailableEquipment() =>
        _equipment.Where(e => e.IsAvailable).ToList();

    public void MarkEquipmentUnavailable(Guid equipmentId)
    {
        var eq = FindEquipment(equipmentId);
        eq.IsAvailable = false;
    }

    public Rental RentEquipment(Guid userId, Guid equipmentId) =>
        RentEquipment(userId, equipmentId, DateTime.Now.AddDays(RentalPolicy.DefaultRentalDays));

    public Rental RentEquipment(Guid userId, Guid equipmentId, DateTime dueDate)
    {
        var user = FindUser(userId);
        var equipment = FindEquipment(equipmentId);

        if (!equipment.IsAvailable)
            throw new InvalidOperationException(
                $"Equipment '{equipment.Name}' is not available for rental.");

        int activeCount = _rentals.Count(r => r.User.Id == userId && r.IsActive);
        if (activeCount >= user.MaxRentals)
            throw new InvalidOperationException(
                $"{user.UserType} '{user.FullName}' has reached the rental limit ({user.MaxRentals}).");

        var rental = new Rental
        {
            User = user,
            Equipment = equipment,
            RentDate = DateTime.Now,
            DueDate = dueDate
        };

        equipment.IsAvailable = false;
        _rentals.Add(rental);
        return rental;
    }

    public void ReturnEquipment(Guid rentalId, DateTime? actualReturnDate = null)
    {
        var rental = _rentals.FirstOrDefault(r => r.Id == rentalId)
            ?? throw new InvalidOperationException($"Rental '{rentalId}' not found.");

        if (!rental.IsActive)
            throw new InvalidOperationException("This rental has already been closed.");

        var returnDate = actualReturnDate ?? DateTime.Now;
        rental.ReturnDate = returnDate;
        rental.Equipment.IsAvailable = true;

        if (returnDate > rental.DueDate)
        {
            int daysLate = (int)(returnDate - rental.DueDate).TotalDays;
            rental.Penalty = daysLate * RentalPolicy.PenaltyPerDay;
        }
    }

    public List<Rental> GetActiveRentalsForUser(Guid userId) =>
        _rentals.Where(r => r.User.Id == userId && r.IsActive).ToList();

    public List<Rental> GetOverdueRentals(DateTime? asOf = null) =>
        _rentals.Where(r => r.IsOverdue(asOf)).ToList();

    public List<Rental> GetAllRentals() => new(_rentals);

    public string GenerateSummaryReport() => "Report — coming in next commit.";

    private User FindUser(Guid id) =>
        _users.FirstOrDefault(u => u.Id == id)
        ?? throw new InvalidOperationException($"User '{id}' not found.");

    private Equipment FindEquipment(Guid id) =>
        _equipment.FirstOrDefault(e => e.Id == id)
        ?? throw new InvalidOperationException($"Equipment '{id}' not found.");
}