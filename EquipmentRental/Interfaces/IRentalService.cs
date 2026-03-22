using EquipmentRental.Models;

namespace EquipmentRental.Interfaces;

public interface IRentalService
{
    void AddUser(User user);
    List<User> GetAllUsers();

    void AddEquipment(Equipment equipment);
    List<Equipment> GetAllEquipment();
    List<Equipment> GetAvailableEquipment();
    void MarkEquipmentUnavailable(Guid equipmentId);

    Rental RentEquipment(Guid userId, Guid equipmentId);
    Rental RentEquipment(Guid userId, Guid equipmentId, DateTime dueDate);
    void ReturnEquipment(Guid rentalId, DateTime? actualReturnDate = null);
    List<Rental> GetActiveRentalsForUser(Guid userId);
    List<Rental> GetOverdueRentals(DateTime? asOf = null);
    List<Rental> GetAllRentals();

    string GenerateSummaryReport();
}