using EquipmentRental.Models;

namespace EquipmentRental.Interfaces;

// TODO: define full contract in next step
public interface IRentalService
{
    void AddUser(User user);
    void AddEquipment(Equipment equipment);
    List<Equipment> GetAllEquipment();
}
