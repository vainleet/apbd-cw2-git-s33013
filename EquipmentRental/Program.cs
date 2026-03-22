using EquipmentRental.Interfaces;
using EquipmentRental.Models;
using EquipmentRental.Services;

IRentalService service = new RentalService();

var laptop = new Laptop { Name = "Dell XPS 15", RamGb = 16, Cpu = "Intel i7" };
service.AddEquipment(laptop);

Console.WriteLine("Equipment added:");
foreach (var eq in service.GetAllEquipment())
    Console.WriteLine($"  - [{eq.TypeLabel}] {eq.Name}");
