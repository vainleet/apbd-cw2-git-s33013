# EquipmentRental

University equipment rental system

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
│   └── RentalService.cs    # Business logic, uses DataStore as data source
├── Config/
│   └── RentalPolicy.cs     # Penalty rate and default rental duration
├── Data/
│   └── DataStore.cs        # Centralized in-memory storage with JSON save/load
├── UI/
│   └── ConsoleUI.cs        # Console output helpers
└── Program.cs              # Demo scenario (15 steps)
```

## Demo scenario

The demo in `Program.cs` covers 15 steps:

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
15. Saving data to JSON file

## Design decisions

### Separation of responsibilities
Each class has one clear job. `RentalService` handles all business logic. `Program.cs` only drives the demo. `ConsoleUI` only handles output formatting. `DataStore` holds the in-memory state and manages persistence. Models hold data only — the only logic in models is `IsActive` and `IsOverdue()` which depend purely on the model's own fields.

### DataStore and JSON persistence
`DataStore` is a static class that acts as a centralized in-memory data container. `RentalService` references its lists directly. On startup `DataStore.Load()` reads `data.json` if it exists. At the end of the demo `DataStore.Save()` writes the current state to `data.json`. Polymorphic types (`User`, `Equipment`) are handled via custom `JsonConverter` implementations so that subtypes (`Student`, `Employee`, `Laptop`, etc.) survive serialization and deserialization correctly.

### Inheritance
`Equipment` and `User` are abstract base classes because the domain genuinely has shared data and type-specific fields. `Laptop`, `Projector`, `Camera`, `Student`, `Employee` extend them. Inheritance follows from the domain, not from a desire to use it artificially.

### Interface
`IRentalService` defines the full service contract. `Program.cs` depends only on the interface, not the concrete implementation. This makes the service easy to swap or test independently.

### Business rules in one place
`RentalPolicy` holds the penalty rate and default rental duration as constants — one place to change if the rules change. Rental limits (`MaxRentals`) live in each user subclass, so adding a new user type with a different limit requires no changes to `RentalService`.

### Cohesion
Each class is focused on one thing. `ConsoleUI` knows nothing about rentals. `RentalService` knows nothing about the console. `RentalPolicy` is just constants. `DataStore` handles only data storage and persistence.

### Coupling
`RentalService` does not depend on `ConsoleUI` or `Program.cs`. The console layer depends on the service, not the other way around.

### Error handling
Invalid operations throw `InvalidOperationException` with a descriptive message. `ConsoleUI.TryAction` catches these and prints them in red, so the demo clearly shows which operations are blocked and why.