# 🚗 Parking System API (ASP.NET Core + MongoDB)

A simple Parking System built using **ASP.NET Core Web API** and **MongoDB**.  
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
- **Backend**: ASP.NET Core Web API (.NET 6/7/8)  
- **Database**: MongoDB Atlas (Cloud) or Local MongoDB  
- **Serialization**: MongoDB.Bson  
- **Dependency Injection**: Built-in .NET DI  

---

## 📂 Project Structure
ParkingSystem/
│── Controllers/ # API Controllers
│── Models/ # Data Models
│── Services/ # Business logic & DB access
│── appsettings.json # App configuration (ignored in GitHub)
│── Program.cs # App entry point
