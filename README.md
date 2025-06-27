
# **UniTrail.Admin**  
*A Backend Admin System for University Trail Management*  

---

## **📌 Table of Contents**  
1. [Project Overview](#-project-overview)  
2. [Features](#-features)  
3. [Tech Stack](#-tech-stack)  
4. [Setup Guide](#-setup-guide)  
5. [API Documentation](#-api-documentation)  
6. [Environment Variables](#-environment-variables)  
7. [Testing](#-testing)  
8. [Deployment](#-deployment)  
9. [Contributing](#-contributing)  
10. [License](#-license)  

---

## **🚀 Project Overview**  
**UniTrail.Admin** is a secure backend API for managing university trails, buildings, points of interest (POIs), and user authentication. Built with **.NET 6**, it provides:  
- JWT-based authentication  
- Role-based access control (Admin/User)  
- CRUD operations for campus resources  

---

## **✨ Features**  
✅ **Authentication & Authorization**  
- Login, password reset, and profile management  
- Admin-only endpoints (e.g., user registration)  

✅ **Resource Management**  
- Buildings, POIs, and events management  
- QR code generation for locations  

✅ **Security**  
- Encrypted passwords (BCrypt)  
- JWT token validation  

✅ **Developer-Friendly**  
- Swagger/OpenAPI documentation  
- Logging with `ILogger`  

---

## **🛠 Tech Stack**  
| Component       | Technology |  
|-----------------|------------|  
| Backend         | .NET 6     |  
| Database        | SQL Server |  
| Authentication  | JWT        |  
| API Docs        | Swagger    |  
| Testing         | xUnit      |  

---

## **⚙ Setup Guide**  

### **Prerequisites**  
- [.NET 6 SDK](https://dotnet.microsoft.com/download)  
- [SQL Server](https://www.microsoft.com/en-us/sql-server/)  
- [Git](https://git-scm.com/)  

### **Steps**  
1. **Clone the repository**:  
   ```bash
   git clone https://github.com/your-username/UniTrail.Admin.git
   cd UniTrail.Admin
   ```

2. **Configure the database**:  
   - Update `appsettings.json` with your SQL Server connection string:  
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=UniTrailDb;User=YOUR_USER;Password=YOUR_PASSWORD;"
     }
     ```

3. **Run migrations**:  
   ```bash
   dotnet ef database update
   ```

4. **Launch the API**:  
   ```bash
   dotnet run
   ```
   - Swagger UI: `http://localhost:5000`  

---

## **📚 API Documentation**  

### **Authentication**  
| Endpoint                | Method | Description                     | Auth Required |  
|-------------------------|--------|---------------------------------|---------------|  
| `/api/auth/login`       | POST   | Login with credentials          | No            |  
| `/api/auth/change-password` | POST   | Update user password           | Yes (JWT)     |  
| `/api/auth/register-admin` | POST  | Register new admin (Admin-only) | Yes (Admin)   |  

**Example Request (Login):**  
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "Admin@123"
}
```

---

## **🔑 Environment Variables**  
Create a `.env` file (or use `appsettings.Development.json`):  
```json
{
  "Jwt": {
    "Key": "YourSuperSecretKeyHere_AtLeast32Chars",
    "Issuer": "UniTrail.Admin",
    "Audience": "UniTrail.Users"
  }
}
```

---

## **🧪 Testing**  
Run unit tests:  
```bash
dotnet test
```

**Test Cases**:  
- Authentication flow  
- Role-based access control  

---

## **🚀 Deployment**  
### **Docker**  
1. Build the image:  
   ```bash
   docker build -t unitrail-admin .
   ```
2. Run the container:  
   ```bash
   docker run -p 5000:80 unitrail-admin
   ```


## **🤝 Contributing**  
1. Fork the repository.  
2. Create a branch:  
   ```bash
   git checkout -b feature/your-feature
   ```
3. Commit changes:  
   ```bash
   git commit -m "feat: add new endpoint"
   ```
4. Push and open a PR.  

**Guidelines**:  
- Follow RESTful conventions.  
- Write unit tests for new features.  

---

## **📬 Contact**  
For questions, email `Umeokekeprince@gmail.com`.  

---
