# Warehouse Management API

Warehouse Management API là hệ thống quản lý kho hàng được xây dựng bằng **ASP.NET Core Web API (.NET 8)**.  
Dự án tập trung vào các nghiệp vụ cốt lõi của kho: quản lý sản phẩm, tồn kho và dữ liệu liên quan, sử dụng **Entity Framework Core** để làm việc với cơ sở dữ liệu SQL Server.

---

## 🎯 Mục tiêu dự án
- Xây dựng nền tảng API quản lý kho theo mô hình chuẩn
- Áp dụng Entity Framework Core với Code First + Migration
- Dễ mở rộng cho các nghiệp vụ thực tế trong doanh nghiệp

---

## 🛠 Công nghệ sử dụng
- **Framework:** .NET 8.0 (ASP.NET Core Web API)
- **Database:** SQL Server
- **ORM:** Entity Framework Core 8.0.12
- **IDE:** Visual Studio 2022
- **Quản lý mã nguồn:** Git & GitHub

---

## 📦 NuGet Packages
Dự án sử dụng các thư viện thuộc hệ sinh thái .NET 8 để đảm bảo tính ổn định và bảo mật:

🗄️ Database & ORM
- Microsoft.EntityFrameworkCore.SqlServer (8.0.12)
- Microsoft.EntityFrameworkCore.Tools (8.0.12)
- Microsoft.EntityFrameworkCore.Design (8.0.12)

🔐 Security & Authentication
- BCrypt.Net-Next
- Microsoft.AspNetCore.Authentication.JwtBearer

## 🚀 Lệnh cài đặt nhanh
```bash
# Cài đặt EF Core
Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 8.0.12
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 8.0.12
Install-Package Microsoft.EntityFrameworkCore.Design -Version 8.0.12

# Cài đặt Security & JWT
Install-Package BCrypt.Net-Next
Install-Package Microsoft.AspNetCore.Authentication.JwtBearer -Version 8.0.0
```

---
## 🚀 Hướng dẫn cài đặt & chạy dự án

### 1️⃣ Clone source code

git clone:
```bash
https://github.com/anhthu100903/WarehouseManagement.git
```

### 2. Cấu hình Cơ sở dữ liệu

Mở file `appsettings.json` trong project **WarehouseManagement.Api** và cập nhật chuỗi kết nối SQL Server phù hợp với máy của bạn:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=WarehouseDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```
### 3️⃣ Khởi tạo cơ sở dữ liệu (Migrations)
Mở Package Manager Console trong Visual Studio và chạy:

Add-Migration InitialCreate
Update-Database
### 4️⃣ Chạy ứng dụng
Nhấn F5 hoặc Start trong Visual Studio

Truy cập Swagger UI để kiểm tra API:
[https://localhost:<port>/swagger](https://localhost:<port>/swagger)

📝 Giấy phép
Dự án sử dụng MIT License.
Bạn có quyền tự do sử dụng, chỉnh sửa và phân phối lại mã nguồn.

👤 Tác giả
GitHub: anhthu100903
