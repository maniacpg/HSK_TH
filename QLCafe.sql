-- Tạo cơ sở dữ liệu
CREATE DATABASE QLyQuanCafe
GO

USE QLyQuanCafe
GO

-- Bảng Ban
CREATE TABLE Ban
(
    id INT IDENTITY PRIMARY KEY,
    name NVARCHAR(100) NOT NULL DEFAULT N'Chua dat ten',
    TrangThai NVARCHAR(100) NOT NULL, --empty/full
)
GO

-- Bảng TaiKhoan
CREATE TABLE TaiKhoan
(   
    Ten NVARCHAR(100) NOT NULL,
    TenTK NVARCHAR(100) PRIMARY KEY,
    MatKhau NVARCHAR(1000) NOT NULL DEFAULT N'0',
    Type INT NOT NULL DEFAULT 0 --1.admin/0.staff
)
GO

-- Bảng DanhmucDoUong
CREATE TABLE DanhmucDoUong
(
    id INT IDENTITY PRIMARY KEY,
    TenDoUong NVARCHAR(100) NOT NULL DEFAULT N'Chua dat ten'
)
GO

-- Bảng DoUong
CREATE TABLE DoUong
(
    id INT IDENTITY PRIMARY KEY,
    TenDoUong NVARCHAR(100) DEFAULT N'Chua dat ten',
    idDanhMuc INT NOT NULL,
    DonGia FLOAT NOT NULL DEFAULT 0

    FOREIGN KEY (idDanhMuc) REFERENCES dbo.DanhmucDoUong(id)
)
GO

-- Bảng NhanVien (không có IDENTITY ở cột id)
CREATE TABLE NhanVien
(
    id INT PRIMARY KEY,
    TenNV NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
    SDTNV NVARCHAR(20),
    emailNV NVARCHAR(100),
    GioiTinh BIT,
    NgaySinh DATE
)
GO

-- Bảng KhachHang
CREATE TABLE KhachHang
(
    id INT PRIMARY KEY,
    TenKH NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
    SDTKH NVARCHAR(20),
    emailKH NVARCHAR(100),
)
GO

-- Bảng HoaDon
CREATE TABLE HoaDon
(
    id INT IDENTITY PRIMARY KEY,
    DateCheckIn DATE NOT NULL DEFAULT GETDATE(),
    DateCheckOut DATE,
    idBan INT NOT NULL,
    status INT NOT NULL,   -- 1.Paid/0.not paid
    idKH INT,        
    idNV INT,           
    FOREIGN KEY (idBan) REFERENCES dbo.Ban(id),
    FOREIGN KEY (idKH) REFERENCES dbo.KhachHang(id),
    FOREIGN KEY (idNV) REFERENCES dbo.NhanVien(id)
)
GO

-- Bảng ChiTietHoaDonBan
CREATE TABLE ChiTietHoaDonBan
(
    id INT IDENTITY PRIMARY KEY,
    idHoaDon INT NOT NULL,
    idDoUong INT NOT NULL,
    count INT NOT NULL DEFAULT 0,
    FOREIGN KEY (idHoaDon) REFERENCES dbo.HoaDon(id),
    FOREIGN KEY (idDoUong) REFERENCES dbo.DoUong(id),
)
GO

-- Bảng NCC
CREATE TABLE NCC
(
    id INT IDENTITY PRIMARY KEY,
    name NVARCHAR(1000),
    diaChi NVARCHAR(1000),
    sDT VARCHAR(10),
)
GO

-- Bảng HoaDonNhapHang
CREATE TABLE HoaDonNhapHang
(
    id INT IDENTITY PRIMARY KEY,         
    idNCC INT NOT NULL,   
    idNV INT NOT NULL,
    DateReceived DATE NOT NULL DEFAULT GETDATE(), 
    TotalAmount FLOAT NOT NULL DEFAULT 0,         
    FOREIGN KEY (idNCC) REFERENCES dbo.NCC(id),
    FOREIGN KEY (idNV) REFERENCES dbo.NhanVien(id) 
)
GO

-- Bảng ChiTietHoaDonNhapHang
CREATE TABLE ChiTietHoaDonNhapHang
(
    id INT IDENTITY PRIMARY KEY,     
    idHoaDon INT NOT NULL,           
    idDoUong INT NOT NULL,             
    SoLuong INT NOT NULL DEFAULT 0, 
    Gia FLOAT NOT NULL DEFAULT 0,  
    FOREIGN KEY (idHoaDon) REFERENCES dbo.HoaDonNhapHang(id),
    FOREIGN KEY (idDoUong) REFERENCES dbo.DoUong(id)              
)
GO

-- Stored Procedures

-- Insert_KhachHang
CREATE PROCEDURE Insert_KhachHang
    @TenKH NVARCHAR(100),
    @SDTKH NVARCHAR(20),
    @emailKH NVARCHAR(100)
AS
BEGIN
    INSERT INTO KhachHang(TenKH, SDTKH, emailKH)
    VALUES (@TenKH, @SDTKH, @emailKH)
END
GO

-- Select_KhachHang
CREATE PROCEDURE Select_KhachHang
AS
BEGIN
    SELECT id, TenKH, SDTKH, emailKH
    FROM KhachHang;
END 
GO

-- Update_KhachHang
CREATE PROCEDURE Update_KhachHang
    @ID INT,
    @TenKH NVARCHAR(100),
    @SDTKH NVARCHAR(20),
    @EmailKH NVARCHAR(100)
AS
BEGIN
    UPDATE KhachHang
    SET TenKH = @TenKH,
        SDTKH = @SDTKH,
        emailKH = @EmailKH
    WHERE id = @ID;
END
GO

-- Delete_KhachHang
CREATE PROCEDURE Delete_KhachHang
    @ID INT
AS
BEGIN
    DELETE FROM KhachHang
    WHERE id = @ID;
END
GO

-- Insert_TaiKhoan
CREATE PROCEDURE Insert_TaiKhoan
    @Ten NVARCHAR(100),
    @TenTK NVARCHAR(100),
    @MatKhau NVARCHAR(1000),
    @Type INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM TaiKhoan WHERE TenTK = @TenTK)
    BEGIN
        INSERT INTO TaiKhoan (Ten, TenTK, MatKhau, Type)
        VALUES (@Ten, @TenTK, @MatKhau, @Type)
        SELECT N'Tài khoản đã được thêm thành công.' AS Result
    END
    ELSE
    BEGIN
        SELECT N'Tên tài khoản đã tồn tại. Vui lòng chọn tên khác.' AS Result
    END
END
GO

-- Select_All_TaiKhoan
CREATE PROCEDURE Select_All_TaiKhoan
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        Ten, 
        TenTK, 
        Type,
        CASE 
            WHEN Type = 1 THEN N'Admin'
            WHEN Type = 0 THEN N'Nhân viên'
            ELSE N'Không xác định'
        END AS LoaiTaiKhoan
    FROM 
        TaiKhoan
    ORDER BY 
        TenTK
END
GO

-- Update_TaiKhoan
CREATE PROCEDURE Update_TaiKhoan
    @Ten NVARCHAR(100),
    @TenTK NVARCHAR(100),
    @Type INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE TaiKhoan
    SET Ten = @Ten, Type = @Type
    WHERE TenTK = @TenTK
    IF @@ROWCOUNT > 0
        SELECT 1 AS Result
    ELSE
        SELECT 0 AS Result
END
GO

-- Delete_TaiKhoan
CREATE PROCEDURE Delete_TaiKhoan
    @TenTK NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM TaiKhoan WHERE TenTK = @TenTK;
    IF @@ROWCOUNT > 0
        SELECT 1;
    ELSE
        SELECT 0;
END
GO

-- Select_NhanVien
CREATE PROCEDURE Select_NhanVien
AS
BEGIN
    SELECT id, TenNV, SDTNV, emailNV, NgaySinh, GioiTinh
    FROM NhanVien;
END 
GO
--Insert NhanVien
CREATE PROCEDURE Insert_NhanVien
    @id INT,
    @TenNV NVARCHAR(100),
    @SDTNV NVARCHAR(20),
    @emailNV NVARCHAR(100),
    @GioiTinh BIT,
    @NgaySinh DATE
AS
BEGIN
    INSERT INTO NhanVien (id, TenNV, SDTNV, emailNV, GioiTinh, NgaySinh)
    VALUES (@id, @TenNV, @SDTNV, @emailNV, @GioiTinh, @NgaySinh);
END
GO
--cap nhat NhanVien
CREATE PROCEDURE Update_NhanVien
    @id INT,
    @TenNV NVARCHAR(100),
    @SDTNV NVARCHAR(20),
    @emailNV NVARCHAR(100),
    @GioiTinh BIT,
    @NgaySinh DATE
AS
BEGIN
    UPDATE NhanVien
    SET TenNV = @TenNV,
        SDTNV = @SDTNV,
        emailNV = @emailNV,
        GioiTinh = @GioiTinh,
        NgaySinh = @NgaySinh
    WHERE id = @id;
END
GO
--Xoa NhanVien
CREATE PROCEDURE Delete_NhanVien
    @id INT
AS
BEGIN
    DELETE FROM NhanVien
    WHERE id = @id;
END
GO
exec Delete_NhanVien @id = 12
select * from NhanVien
--tim kiem theo ten

CREATE PROCEDURE Search_NhanVien
    @TenNV NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * 
    FROM NhanVien
    WHERE TenNV LIKE '%' + @TenNV + '%';
END


-- Thêm dữ liệu mẫu
INSERT INTO NhanVien (id, TenNV, SDTNV, emailNV, GioiTinh, NgaySinh)
VALUES 
(1, N'Nguyễn Văn Hùng', '0901234567', 'nguyenvanhung@email.com', 1, '1990-01-15'),
(2, N'Trần Thị Mai', '0912345678', 'tranthimai@email.com', 0, '1992-05-20'),
(3, N'Lê Minh Tuấn', '0923456789', 'leminhtuanh@email.com', 1, '1988-11-30'),
(4, N'Phạm Thị Hương', '0934567890', 'phamthihuong@email.com', 0, '1995-08-10'),
(5, N'Hoàng Đức Anh', '0945678901', 'hoangducanh@email.com', 1, '1993-03-25'),
(6, N'Vũ Thị Lan Anh', '0956789012', 'vuthilananh@email.com', 0, '1991-12-05')
GO

-- Thêm dữ liệu mẫu cho bảng TaiKhoan
INSERT INTO TaiKhoan (Ten, TenTK, MatKhau, Type)
VALUES 
(N'Đình Quân', 'maniac', '123456', 0),
(N'Phong Nguyễn', 'phongn', '123456', 1)
GO