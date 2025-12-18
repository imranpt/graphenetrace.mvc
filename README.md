# Graphanetrace

Graphanetrace is a web-based healthcare monitoring system built using **ASP.NET Core MVC**.  
The application manages users (Admin, Clinician, Patient) and visualises wheelchair pressure data to help assess pressure injury risk.

The system focuses on pressure-based metrics and visual analysis to support clinical decision-making.

---

## 👤 Project Type
Individual Project

---

## 🛠️ Technologies Used

- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server
- Bootstrap 5
- HTML / CSS / JavaScript
- Chart.js
- CSV file processing

---

## 👥 User Roles

### Admin
- Create, edit, and delete users
- Manage system access and roles

### Clinician
- View assigned patients
- Upload pressure data (CSV)
- View patient pressure metrics and risk levels

### Patient
- View personal dashboard
- View pressure heatmap and latest metrics

---

## 📊 Features

- Role-based authentication and session management
- User and patient management
- CSV upload for pressure sensor data
- Pressure heatmap visualisation
- Automatic risk classification (Low / Medium / High)
- Clean and responsive user interface
- Server-side and client-side form validation
- Secure database operations using Entity Framework Core

---


## 🗂️ Project Structure

```
Project
│
├── Controllers
│   ├── AuthController.cs
│   ├── UsersController.cs
│   ├── ClinicianController.cs
│   └── PatientController.cs
│
├── Models
│   ├── User.cs
│   ├── Patient.cs
│   └── PressureFrame.cs
│
├── Views
│   ├── Auth
│   ├── Users
│   ├── Clinician
│   └── Patient
│
├── Data
│   └── AppDBContext.cs
│
├── Migrations
├── wwwroot
│   ├── css
│   ├── js
│   └── images
│
└── Program.cs
```

---

##  How to Run the Project

1. Clone the repository:
```bash
git clone https://github.com/your-username/Graphanetrace.git

2.Open the project in Visual Studio

3.Update the database connection string in appsettings.json

4.Apply migrations:
Update-Database
5.Run the project using IIS Express

 Notes

This project was developed as part of a university group assignment.
The repository clearly documents my individual contributions for transparency.

 License

This project is for educational purposes only.
