using EquipmentRental.Interfaces;
using EquipmentRental.Models;
using EquipmentRental.Services;
using EquipmentRental.UI;

IRentalService service = new RentalService();

ConsoleUI.Header("STEP 1 — Adding equipment");

var laptop1 = new Laptop    { Name = "Dell XPS 15",    RamGb = 16, Cpu = "Intel i7-1260P" };
var laptop2 = new Laptop    { Name = "MacBook Pro 14", RamGb = 32, Cpu = "Apple M3 Pro" };
var proj1   = new Projector { Name = "Epson EB-X51",   Lumens = 3800, Resolution = "1024x768" };
var proj2   = new Projector { Name = "BenQ MH560",     Lumens = 4000, Resolution = "1920x1080" };
var cam1    = new Camera    { Name = "Canon EOS R50",  Megapixels = 24, HasStabilization = true };
var cam2    = new Camera    { Name = "Sony ZV-E10",    Megapixels = 24, HasStabilization = false };

foreach (var eq in new Equipment[] { laptop1, laptop2, proj1, proj2, cam1, cam2 })
{
    service.AddEquipment(eq);
    ConsoleUI.Success($"Added: {eq}");
}

ConsoleUI.Header("STEP 2 — Adding users");

var student1  = new Student  { FirstName = "Anna",  LastName = "Kowalska" };
var student2  = new Student  { FirstName = "Piotr", LastName = "Nowak" };
var employee1 = new Employee { FirstName = "Marek", LastName = "Wiśniewski" };
var employee2 = new Employee { FirstName = "Julia", LastName = "Zielińska" };

foreach (var user in new User[] { student1, student2, employee1, employee2 })
{
    service.AddUser(user);
    ConsoleUI.Success($"Added: {user}");
}

ConsoleUI.Header("STEP 3 — All equipment");
ConsoleUI.PrintList(service.GetAllEquipment());

ConsoleUI.Header("STEP 4 — Available equipment");
ConsoleUI.PrintList(service.GetAvailableEquipment());

ConsoleUI.Header("STEP 5 — Valid rentals");

Rental? rentalAnnaLaptop = null;
Rental? rentalMarekProj  = null;

ConsoleUI.TryAction("Anna rents Dell XPS 15", () =>
{
    rentalAnnaLaptop = service.RentEquipment(student1.Id, laptop1.Id);
    ConsoleUI.Success($"Rental created: {rentalAnnaLaptop}");
});

ConsoleUI.TryAction("Anna rents Canon EOS R50 (2nd — at her limit)", () =>
{
    service.RentEquipment(student1.Id, cam1.Id);
    ConsoleUI.Success("Rental created.");
});

ConsoleUI.TryAction("Marek rents Epson Projector", () =>
{
    rentalMarekProj = service.RentEquipment(employee1.Id, proj1.Id);
    ConsoleUI.Success($"Rental created: {rentalMarekProj}");
});

ConsoleUI.Header("STEP 6 — Invalid: rent already rented equipment");

ConsoleUI.TryAction("Piotr tries to rent Dell XPS 15 (taken by Anna)", () =>
    service.RentEquipment(student2.Id, laptop1.Id));

ConsoleUI.Header("STEP 7 — Invalid: exceed rental limit");

ConsoleUI.TryAction("Anna tries to rent MacBook Pro (would be her 3rd)", () =>
    service.RentEquipment(student1.Id, laptop2.Id));

ConsoleUI.Header("STEP 8 — Mark Sony ZV-E10 as unavailable (damaged)");

ConsoleUI.TryAction("Marking Sony ZV-E10 as unavailable", () =>
{
    service.MarkEquipmentUnavailable(cam2.Id);
    ConsoleUI.Info("Sony ZV-E10 is now unavailable.");
});

ConsoleUI.TryAction("Julia tries to rent Sony ZV-E10 (damaged)", () =>
    service.RentEquipment(employee2.Id, cam2.Id));

ConsoleUI.Header("STEP 9 — Active rentals for Anna");
ConsoleUI.PrintList(service.GetActiveRentalsForUser(student1.Id));

ConsoleUI.Header("STEP 10 — On-time return");

ConsoleUI.TryAction("Anna returns Dell XPS 15 today", () =>
{
    service.ReturnEquipment(rentalAnnaLaptop!.Id, DateTime.Now);
    ConsoleUI.Success($"Returned. Penalty: {rentalAnnaLaptop.Penalty} PLN");
});

ConsoleUI.Header("STEP 11 — Late return");

ConsoleUI.TryAction("Marek returns Epson Projector 5 days late", () =>
{
    service.ReturnEquipment(rentalMarekProj!.Id, rentalMarekProj.DueDate.AddDays(5));
    ConsoleUI.Success($"Returned late. Penalty: {rentalMarekProj.Penalty} PLN (5 days × 10 PLN)");
});

ConsoleUI.Header("STEP 12 — Overdue rentals");

ConsoleUI.TryAction("Piotr rents BenQ MH560 with due date 3 days ago (simulated)", () =>
{
    service.RentEquipment(student2.Id, proj2.Id, DateTime.Now.AddDays(-3));
    ConsoleUI.Success("Rental created with past due date.");
});

var overdue = service.GetOverdueRentals();
ConsoleUI.Info($"Overdue count: {overdue.Count}");
ConsoleUI.PrintList(overdue);

ConsoleUI.Header("STEP 13 — Summary report");
Console.WriteLine(service.GenerateSummaryReport());

ConsoleUI.Header("STEP 14 — Full rental history");
ConsoleUI.PrintList(service.GetAllRentals());

Console.WriteLine();
ConsoleUI.Success("Demo complete.");