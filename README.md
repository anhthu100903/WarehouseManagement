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
Các thư viện chính đang được sử dụng:
- `Microsoft.EntityFrameworkCore.SqlServer` (8.0.12)
- `Microsoft.EntityFrameworkCore.Tools` (8.0.12)
- `Microsoft.EntityFrameworkCore.Design` (8.0.12)

---

## 🚀 Hướng dẫn cài đặt & chạy dự án

### 1️⃣ Clone source code 
```bash
git clone https://github.com/anhthu100903/WarehouseManagement.git

### 2️⃣ Cấu hình cơ sở dữ liệu
Mở file `appsettings.json` trong project **WarehouseManagement.Api**  
Cập nhật chuỗi kết nối SQL Server cho phù hợp với máy của bạn:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=WarehouseDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
3. Khởi tạo Cơ sở dữ liệu (Migrations)
Mở cửa sổ Package Manager Console trong Visual Studio và chạy lần lượt 2 lệnh sau để tạo bảng:

Add-Migration InitialCreate
Update-Database
4. Chạy ứng dụng
Nhấn F5 hoặc chọn nút Start trong Visual Studio.

Sau khi ứng dụng khởi động, bạn có thể truy cập giao diện Swagger tại đường dẫn mặc định để kiểm tra các API.

📝 Giấy phép
Dự án này sử dụng giấy phép MIT License. Bạn có quyền tự do sử dụng, chỉnh sửa và phân phối lại mã nguồn này.

Tác giả: anhthu100903
