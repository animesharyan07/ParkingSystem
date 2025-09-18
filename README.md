# 🚗 Parking System API (ASP.NET Core + MongoDB)

A simple Parking System built using **ASP.NET Web API** and **MongoDB**.  
This project demonstrates CRUD operations, MongoDB integration, and clean API design.

---

## 📌 Features
- Create, Read, Update, Delete (CRUD) Parking records  
- MongoDB integration with `MongoDB.Driver`  
- Auto-handling of MongoDB collections (no need to pre-create)  
- RESTful API endpoints  
- Easily extendable with Razor Pages or React frontend  

---

## 🛠️ Tech Stack
- **Backend**: ASP.NET Core Web API (.NET 8)  
- **Database**: MongoDB Atlas (Cloud)   
- **Serialization**: MongoDB.Bson  
- **Dependency Injection**: Built-in .NET DI  

---

## 📂 Project Structure

ParkingSystem/
│── Controllers/              # API Controllers
│   └── ParkingController.cs
|   
│
│── Models/                   # Data Models
│   └── Parking.cs
|   └── IParkingDatabase.cs
|   └── ParkingDatabaseSetting.cs
│
│── Services/                 # Business logic & DB access
│   └── ParkingService.cs
│   └──IParkingServices.cs
|
│── Properties/
│   └── launchSettings.json   # Local launch config
│
│── appsettings.json          # App configuration (ignored in GitHub)
│── appsettings.Development.json # App configuration (ignored in GitHub)
│── Program.cs                # App entry point
│── ParkingSystem.csproj      # Project file

