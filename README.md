﻿# Mini-Shop-System

A simple but functional console-based shop application written in C#.
The project was developed as part of a 7-day software development assignment with the goal of implementing a realistic, layered architecture using local JSON data storage.

## Features

- User login system with roles (Admin / Customer)
- Product listing with search and filter functions
- Shopping cart and order processing
- Admin product management (CRUD)
- Basic statistics (total orders, revenue, top products)
- Data persistence using JSON files
- Clean separation of layers (Presentation, Application, Infrastructure)

## Tech Stack

- C# (.NET Core)
- Console UI
- JSON for local data storage
- Layered architecture with interfaces

## Project Structure

```
MiniShop.Presentation     // Console interaction
MiniShop.Application      // Business logic and interfaces
MiniShop.Infrastructure   // Data access (JSON repositories)
```

## Notes

This project was developed for educational purposes as part of a German vocational training assignment (IHK). It follows best practices for modular architecture and is prepared for future extensions, such as database support with Entity Framework Core.

## Version Control

Daily commits are pushed to this GitHub repository to document the development process.

## License

This project is open-source and available under the [MIT License](LICENSE).
