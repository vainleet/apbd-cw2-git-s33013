using EquipmentRental.Interfaces;
using EquipmentRental.Models;
using EquipmentRental.Services;

IRentalService service = new RentalService();

Console.WriteLine("\n=== Adding Equipment ===");

var laptop1 = new Laptop { Name = "Dell XPS 15",    RamGb = 16, Cpu = "Intel i7-1260P" };
var laptop2 = new Laptop { Name = "MacBook Pro 14", RamGb = 32, Cpu = "Apple M3 Pro" };
var proj1   = new Projector { Name = "Epson EB-X51",  Lumens = 3800, Resolution = "1024x768" };
var cam1    = new Camera { Name = "Canon EOS R50",  Megapixels = 24, HasStabilization = true };
var cam2    = new Camera { Name = "Sony ZV-E10",    Megapixels = 24, HasStabilization = false };

foreach (var eq in new Equipment[] { laptop1, laptop2, proj1, cam1, cam2 })
{
    service.AddEquipment(eq);
    Console.WriteLine($"  + {eq.TypeLabel}: {eq.Name}");
}

Console.WriteLine("\n=== Adding Users ===");

var student1  = new Student  { FirstName = "Anna",  LastName = "Kowalska" };
var student2  = new Student  { FirstName = "Piotr", LastName = "Nowak" };
var employee1 = new Employee { FirstName = "Marek", LastName = "Wiśniewski" };

foreach (var u in new User[] { student1, student2, employee1 })
{
    service.AddUser(u);
    Console.WriteLine($"  + {u.UserType}: {u.FullName} (max rentals: {u.MaxRentals})");
}

Console.WriteLine("\n=== Valid Rentals ===");

Rental? rentalAnnaLaptop = null;
Rental? rentalMarekProj  = null;

Try("Anna rents Dell XPS 15", () =>
{
    rentalAnnaLaptop = service.RentEquipment(student1.Id, laptop1.Id);
    Console.WriteLine($"  OK  due: {rentalAnnaLaptop.DueDate:dd.MM.yyyy}");
});

Try("Anna rents Canon EOS R50 (2nd — at her limit)", () =>
{
    service.RentEquipment(student1.Id, cam1.Id);
    Console.WriteLine("  OK");
});

Try("Marek rents Epson Projector", () =>
{
    rentalMarekProj = service.RentEquipment(employee1.Id, proj1.Id);
    Console.WriteLine($"  OK  due: {rentalMarekProj.DueDate:dd.MM.yyyy}");
});

Console.WriteLine("\n=== Invalid Rentals (should be blocked) ===");

Try("Piotr rents Dell XPS 15 (already rented)", () =>
    service.RentEquipment(student2.Id, laptop1.Id));

Try("Anna rents MacBook Pro (would exceed limit of 2)", () =>
    service.RentEquipment(student1.Id, laptop2.Id));

Try("Renting Sony ZV-E10 after marking it damaged", () =>
{
    service.MarkEquipmentUnavailable(cam2.Id);
    service.RentEquipment(student2.Id, cam2.Id);
});

Console.WriteLine("\n=== Returns ===");

Try("Anna returns Dell XPS 15 on time", () =>
{
    service.ReturnEquipment(rentalAnnaLaptop!.Id, DateTime.Now);
    Console.WriteLine($"  OK  penalty: {rentalAnnaLaptop.Penalty} PLN");
});

Try("Marek returns Epson Projector 5 days late", () =>
{
    service.ReturnEquipment(rentalMarekProj!.Id, rentalMarekProj.DueDate.AddDays(5));
    Console.WriteLine($"  OK  penalty: {rentalMarekProj.Penalty} PLN  (5 days x 10 PLN)");
});

Console.WriteLine("\n=== Overdue Rentals ===");

Try("Piotr rents MacBook Pro with due date 3 days ago", () =>
{
    service.RentEquipment(student2.Id, laptop2.Id, DateTime.Now.AddDays(-3));
    Console.WriteLine("  OK  (simulated past due date)");
});

var overdue = service.GetOverdueRentals();
Console.WriteLine($"  Overdue count: {overdue.Count}");
foreach (var r in overdue)
    Console.WriteLine($"    - {r.User.FullName} → {r.Equipment.Name}, due: {r.DueDate:dd.MM.yyyy}");

Console.WriteLine("\n=== Done ===\n");

static void Try(string label, Action action)
{
    Console.Write($"  [{label}] ");
    try { action(); }
    catch (InvalidOperationException ex) { Console.WriteLine($"BLOCKED: {ex.Message}"); }
}