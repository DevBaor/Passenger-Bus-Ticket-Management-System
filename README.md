# 🚌 Passenger Bus Ticket Management System

[![C#](https://img.shields.io/badge/C%23-9.0-blue?logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![.NET](https://img.shields.io/badge/.NET-Framework-512BD4?logo=.net)](https://dotnet.microsoft.com/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-2019-CC2927?logo=microsoft-sql-server)](https://www.microsoft.com/en-us/sql-server/)
[![Windows](https://img.shields.io/badge/Platform-Windows-0078D4?logo=windows)](https://www.microsoft.com/windows/)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

**Hệ thống quản lý bán vé xe khách - Quản lý lịch trình, bán vé, hành khách và doanh thu**

---

## 📌 Giới Thiệu

Passenger Bus Ticket Management System là ứng dụng desktop được xây dựng bằng C#, được thiết kế để giúp các công ty vận tải quản lý hiệu quả:

- 🚌 **Lịch trình chuyến xe** - Quản lý tuyến đường và thời gian chạy
- 🎫 **Bán vé** - Hệ thống bán vé nhanh và an toàn
- 👥 **Quản lý hành khách** - Lưu thông tin hành khách
- 💰 **Doanh thu** - Theo dõi thu nhập từ bán vé
- 🛞 **Quản lý xe** - Thông tin chi tiết mỗi chiếc xe
- 📊 **Báo cáo** - Thống kê hoạt động và doanh thu

---

## 🎯 Tính Năng Chính

### 🚌 Quản Lý Tuyến & Chuyến
- ✅ Thêm/sửa tuyến đường
- ✅ Quản lý thời gian khởi hành
- ✅ Điểm đón/trả khách
- ✅ Giá vé theo tuyến
- ✅ Lịch trình chi tiết

### 🎫 Bán Vé
- ✅ Tìm kiếm chuyến xe
- ✅ Chọn chỗ ngồi
- ✅ Tính toán giá vé
- ✅ Xử lý thanh toán
- ✅ In vé
- ✅ Giữ chỗ tạm thời

### 👥 Quản Lý Hành Khách
- ✅ Lưu thông tin hành khách
- ✅ Danh sách hành khách trên chuyến
- ✅ Lịch sử mua vé
- ✅ Tìm kiếm hành khách
- ✅ Quản lý khách VIP

### 💳 Thanh Toán & Hoá Đơn
- ✅ Xử lý thanh toán tiền mặt
- ✅ Thanh toán chuyển khoản
- ✅ In hoá đơn
- ✅ Quản lý giảm giá
- ✅ Hoàn tiền vé

### 🚗 Quản Lý Xe
- ✅ Danh sách xe
- ✅ Thông tin kỹ thuật
- ✅ Sức chứa ghế
- ✅ Lịch bảo trì
- ✅ Trạng thái xe

### 📊 Báo Cáo & Thống Kê
- ✅ Doanh thu theo tuyến
- ✅ Doanh thu theo thời gian
- ✅ Tỷ lệ lấp đầy xe
- ✅ Top tuyến phổ biến
- ✅ Xuất báo cáo Excel, PDF

---

## 🛠️ Công Nghệ Sử Dụng

| Công Nghệ | Phiên Bản | Mục Đích |
|-----------|----------|---------|
| **C#** | 9.0+ | Ngôn ngữ lập trình |
| **.NET Framework** | 4.7.2+ | Framework |
| **Windows Forms** | Latest | UI Desktop |
| **SQL Server** | 2019+ | Database |
| **LINQ** | - | Data access |

---

## 📦 Cấu Trúc Dự Án

```
Passenger-Bus-Ticket-Management-System/
├── QuanLyBanVe/              # Dự án chính
│   ├── Forms/                # Giao diện
│   │   ├── LoginForm.cs
│   │   ├── MainForm.cs
│   │   ├── TicketForm.cs
│   │   ├── BusForm.cs
│   │   ├── RouteForm.cs
│   │   ├── ReportForm.cs
│   │   └── ...
│   ├── Models/               # Data models
│   │   ├── Bus.cs
│   │   ├── Route.cs
│   │   ├── Schedule.cs
│   │   ├── Ticket.cs
│   │   ├── Passenger.cs
│   │   └── ...
│   ├── Services/             # Business logic
│   │   ├── TicketService.cs
│   │   ├── BusService.cs
│   │   └── ...
│   ├── Database/             # Database utilities
│   │   ├── Connection.cs
│   │   └── DatabaseManager.cs
│   ├── bin/                  # Compiled output
│   ├── obj/
│   └── App.config
├── .vs/QuanLyBanVeXeKhach/  # VS cache
├── QuanLyBanVeXeKhach.sln   # Solution file
├── .gitignore
└── .gitattributes
```

---

## 🚀 Hướng Dẫn Cài Đặt

### ✅ Yêu Cầu Hệ Thống
- **Windows 7+** hoặc **Windows 10/11**
- **Visual Studio 2019+** (Community Edition)
- **.NET Framework 4.7.2+**
- **SQL Server 2019+** hoặc **SQL Server Express**
- **Quyền Administrator**

### 1️⃣ Chuẩn Bị Database

```bash
# Mở SQL Server Management Studio (SSMS)
# Tạo database mới
CREATE DATABASE QuanLyBanVeXeKhach;
GO

# Tạo các bảng chính
USE QuanLyBanVeXeKhach;

-- Bảng xe
CREATE TABLE Buses (
    BusID INT PRIMARY KEY IDENTITY,
    BusNumber VARCHAR(50) UNIQUE,
    Capacity INT,
    Status NVARCHAR(50),
    YearMade INT
);

-- Bảng tuyến đường
CREATE TABLE Routes (
    RouteID INT PRIMARY KEY IDENTITY,
    RouteName NVARCHAR(100),
    StartPoint NVARCHAR(100),
    EndPoint NVARCHAR(100),
    Distance FLOAT,
    Price DECIMAL(10,2)
);

-- Bảng lịch trình
CREATE TABLE Schedules (
    ScheduleID INT PRIMARY KEY IDENTITY,
    RouteID INT,
    BusID INT,
    DepartureTime DATETIME,
    ArrivalTime DATETIME,
    FOREIGN KEY (RouteID) REFERENCES Routes(RouteID),
    FOREIGN KEY (BusID) REFERENCES Buses(BusID)
);

-- Bảng hành khách
CREATE TABLE Passengers (
    PassengerID INT PRIMARY KEY IDENTITY,
    FullName NVARCHAR(100),
    Phone VARCHAR(20),
    Email VARCHAR(100),
    IdentityNumber VARCHAR(50),
    Address NVARCHAR(200)
);

-- Bảng vé
CREATE TABLE Tickets (
    TicketID INT PRIMARY KEY IDENTITY,
    ScheduleID INT,
    PassengerID INT,
    SeatNumber INT,
    Price DECIMAL(10,2),
    BookingDate DATETIME,
    Status NVARCHAR(50),
    FOREIGN KEY (ScheduleID) REFERENCES Schedules(ScheduleID),
    FOREIGN KEY (PassengerID) REFERENCES Passengers(PassengerID)
);
```

### 2️⃣ Mở Project

```bash
# Double-click: QuanLyBanVeXeKhach.sln
# Hoặc dùng command line:
start QuanLyBanVeXeKhach.sln
```

### 3️⃣ Cấu Hình Connection String

**File**: `QuanLyBanVe/App.config`

```xml
<configuration>
  <connectionStrings>
    <add name="DefaultConnection" 
         connectionString="Server=.;Database=QuanLyBanVeXeKhach;User Id=sa;Password=YOUR_PASSWORD;" 
         providerName="System.Data.SqlClient" />
  </connectionStrings>
</configuration>
```

### 4️⃣ Build & Chạy

```bash
# Visual Studio:
# Build Solution (Ctrl + Shift + B)
# Start Debugging (F5)

# Hoặc PowerShell:
msbuild QuanLyBanVeXeKhach.sln
.\QuanLyBanVe\bin\Debug\QuanLyBanVe.exe
```

---

## 📖 Hướng Dẫn Sử Dụng

### 🔐 Đăng Nhập
- **Username**: `admin`
- **Password**: `admin123`

### 🏠 Dashboard Chính
- Thống kê doanh thu ngày
- Chuyến xe sắp khởi hành
- Tỷ lệ lấp đầy xe
- Hành khách mới hôm nay

### 🚌 Quản Lý Tuyến & Xe
1. Menu **Quản Lý** → **Tuyến Đường**
2. Click **Thêm Tuyến**
3. Nhập thông tin tuyến (điểm đi, điểm đến, giá)
4. Lưu

### 🎫 Bán Vé
1. Menu **Bán Vé** → **Mua Vé Mới**
2. Chọn ngày khởi hành
3. Chọn tuyến đường
4. Chọn ghế trống
5. Nhập thông tin hành khách
6. Xử lý thanh toán
7. In vé

### 💰 Thanh Toán
1. Menu **Thanh Toán** → **Hoá Đơn**
2. Chọn vé cần thanh toán
3. Chọn phương thức thanh toán
4. Xác nhận
5. In hoá đơn

### 📊 Xem Báo Cáo
1. Menu **Báo Cáo**
2. Chọn loại báo cáo:
   - Doanh thu tuyến
   - Doanh thu thời gian
   - Tỷ lệ lấp đầy
3. Chọn khoảng thời gian
4. Xuất Excel/PDF

---

## 🔧 Tính Năng Advanced

### Tạo Chuyến Xe Định Kỳ
```csharp
// Tạo chuyến hàng ngày tự động
// Menu → Quản Lý → Lịch Trình Định Kỳ
// Cấu hình tuyến, giờ khởi hành, số ngày lặp lại
```

### Quản Lý Giảm Giá
```csharp
// Menu → Bán Vé → Áp Dụng Giảm Giá
// Nhập mã giảm giá hoặc phần trăm
// Áp dụng khi bán vé
```

### Sync Dữ Liệu
```bash
# Backup database
Backup-SqlDatabase -ServerInstance "." -Database "QuanLyBanVeXeKhach" -BackupFile "backup.bak"

# Restore database
Restore-SqlDatabase -ServerInstance "." -Database "QuanLyBanVeXeKhach" -BackupFile "backup.bak"
```

---

## ❓ Troubleshooting

### Lỗi Connection
```
❌ "Cannot connect to database"
✅ Kiểm tra SQL Server đang chạy, cấu hình connection string
```

### Lỗi Missing Tables
```
❌ "Table 'Tickets' doesn't exist"
✅ Chạy lại database creation script
```

### Lỗi Seat Management
```
❌ "Cannot select seat - already booked"
✅ Refresh dữ liệu, hoặc huỷ booking cũ
```

---

## 👨‍💻 Người Phát Triển

- **Duy Bảo (DevBaor)** - Full Stack Developer

---

## 🔗 Liên Kết

- 📧 Email: baotranduy666666@gmail.com
- 🔗 LinkedIn: [Duy Bảo](https://linkedin.com/in/duybaot105)
- 💻 GitHub: [@DevBaor](https://github.com/DevBaor)

---

## 📝 License

Dự án này được cấp phép theo **MIT License** - xem file [LICENSE](LICENSE) để chi tiết.

---

**Made with ❤️ by Duy Bảo - Bus Ticket Management Team**
