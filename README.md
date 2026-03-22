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
│   └── RentalService.cs    # Business logic
├── Config/
│   └── RentalPolicy.cs     # Penalty rate, rental duration limits
└── Program.cs              # Demo scenario
```

## Design decisions

### Separation of responsibilities
Each class has one clear job. `RentalService` handles all business logic — adding users, renting, returning, calculating penalties.

### Inheritance vs composition
`Equipment` and `User` are abstract base classes because the domain genuinely has shared properties (id, name, availability for equipment; id, name, rental limit for users) and type-specific fields.

### Interface
`IRentalService` defines the full contract. `Program.cs` depends only on the interface, not the concrete class. 

### Business rules in one place
`RentalPolicy` holds the penalty rate and default rental duration as constants. If the rules change, there is exactly one place to edit. Rental limits (`MaxRentals`) are defined in each user subclass, so adding a new user type with a different limit requires no changes to the service.

### Coupling
`RentalService` does not depend on `ConsoleUI` or `Program.cs`. 

### Error handling
Invalid operations (renting unavailable equipment, exceeding rental limit, returning an already-closed rental) throw `InvalidOperationException` with a descriptive message.