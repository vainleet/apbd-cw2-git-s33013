using EquipmentRental.Interfaces;
using EquipmentRental.Models;

namespace EquipmentRental.Services;

public class RentalService : IRentalService
{
    private readonly List<User> _users = new();
    private readonly List<Equipment> _equipment = new();

    public void AddUser(User user) => _users.Add(user);

    public void AddEquipment(Equipment equipment) => _equipment.Add(equipment);

    public List<Equipment> GetAllEquipment() => new(_equipment);
}
