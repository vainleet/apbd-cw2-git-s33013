# EquipmentRental

University equipment rental system — APBD Tutorial 2 (C#).

## How to run
```bash
dotnet run --project EquipmentRental
```

## Project structure
```
EquipmentRental/
├── Models/         # Domain entities
│   ├── Equipment.cs        # Abstract base for all equipment
│   ├── Laptop.cs
│   ├── Projector.cs
│   ├── Camera.cs
│   ├── User.cs             # Abstract base for all users
│   ├── Student.cs
│   ├── Employee.cs
│   └── Rental.cs
├── Interfaces/
│   └── IRentalService.cs   # Service contract
├── Services/
│   └── RentalService.cs    # Business logic
├── Config/
│   └── RentalPolicy.cs     # Penalty rate and default rental duration
├── UI/
│   └── ConsoleUI.cs        # Console output helpers
└── Program.cs              # Demo scenario (14 steps)
```

## How to run
```bash
dotnet run --project EquipmentRental
```

## Demo scenario

The demo in `Program.cs` covers 14 steps:

1. Adding equipment of three types (Laptop, Projector, Camera)
2. Adding users of two types (Student, Employee)
3. Listing all equipment
4. Listing only available equipment
5. Valid rental operations
6. Blocked rental — equipment already rented
7. Blocked rental — user exceeds their limit
8. Marking equipment as unavailable (damaged), blocked rental attempt
9. Active rentals for a specific user
10. On-time return with zero penalty
11. Late return with penalty calculation
12. Simulated overdue rental
13. Summary report
14. Full rental history

## Design decisions

### Separation of responsibilities
Each class has one clear job. `RentalService` handles all business logic — registering users and equipment, renting, returning, calculating penalties, generating reports. `Program.cs` only drives the demo. `ConsoleUI` only handles output formatting. Models hold data only — the only logic in models is `IsActive` and `IsOverdue()` which depend purely on the model's own fields.

### Inheritance
`Equipment` and `User` are abstract base classes because the domain genuinely has shared data and type-specific fields. `Laptop`, `Projector`, `Camera`, `Student`, `Employee` extend them. Inheritance follows from the domain, not from a desire to use it artificially.

### Interface
`IRentalService` defines the full service contract. `Program.cs` depends only on the interface, not the concrete implementation. This makes the service easy to swap or test independently.

### Business rules in one place
`RentalPolicy` holds the penalty rate and default rental duration as constants — one place to change if the rules change. Rental limits (`MaxRentals`) live in each user subclass, so adding a new user type with a different limit requires no changes to `RentalService`.

### Cohesion
Each class is focused on one thing. `ConsoleUI` knows nothing about rentals. `RentalService` knows nothing about the console. `RentalPolicy` is just constants. This makes each part easy to read and change independently.

### Coupling
`RentalService` does not depend on `ConsoleUI` or `Program.cs`. The console layer depends on the service, not the other way around.

### Error handling
Invalid operations throw `InvalidOperationException` with a descriptive message. `ConsoleUI.TryAction` catches these and prints them in red, so the demo clearly shows which operations are blocked and why.