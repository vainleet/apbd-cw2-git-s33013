using EquipmentRental.Models;

namespace EquipmentRental.Data;

public static class DataStore
{
    public static List<User> Users = new();
    public static List<Equipment> Equipments = new();
    public static List<Rental> Rentals = new();
}