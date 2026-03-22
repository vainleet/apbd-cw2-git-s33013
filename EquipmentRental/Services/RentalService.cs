using EquipmentRental.Interfaces;
using EquipmentRental.Models;

namespace EquipmentRental.Services;

public class RentalService : IRentalService
{
    private readonly List<User> _users = new();
    private readonly List<Equipment> _equipment = new();

    public void AddUser(User user) => _users.Add(user);

    public List<User> GetAllUsers() => new(_users);

    public void AddEquipment(Equipment equipment) => _equipment.Add(equipment);

    public List<Equipment> GetAllEquipment() => new(_equipment);

    public List<Equipment> GetAvailableEquipment() =>
        _equipment.Where(e => e.IsAvailable).ToList();

    public void MarkEquipmentUnavailable(Guid equipmentId) =>
        throw new NotImplementedException();

    public Rental RentEquipment(Guid userId, Guid equipmentId) =>
        throw new NotImplementedException();

    public Rental RentEquipment(Guid userId, Guid equipmentId, DateTime dueDate) =>
        throw new NotImplementedException();

    public void ReturnEquipment(Guid rentalId, DateTime? actualReturnDate = null) =>
        throw new NotImplementedException();

    public List<Rental> GetActiveRentalsForUser(Guid userId) =>
        throw new NotImplementedException();

    public List<Rental> GetOverdueRentals(DateTime? asOf = null) =>
        throw new NotImplementedException();

    public List<Rental> GetAllRentals() => new();

    public string GenerateSummaryReport() =>
        throw new NotImplementedException();
}