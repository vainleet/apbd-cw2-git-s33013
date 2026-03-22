namespace EquipmentRental.Models;

public class Employee : User
{
    public override int MaxRentals => 5;
    public override string UserType => "Employee";
}
