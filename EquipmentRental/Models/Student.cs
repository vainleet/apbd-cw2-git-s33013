namespace EquipmentRental.Models;

public class Student : User
{
    public override int MaxRentals => 2;
    public override string UserType => "Student";
}
