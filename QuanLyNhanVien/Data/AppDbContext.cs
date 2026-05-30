using Microsoft.EntityFrameworkCore;
using QuanLyNhanVien.Enums;

namespace QuanLyNhanVien.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PhongBan> PhongBan { get; set; }
        public DbSet<NhanVien> NhanVien { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Chen du lieu mau cho bang PhongBan
            modelBuilder.Entity<PhongBan>().HasData(
                new PhongBan { intMaPhongBan = 1, strTenPhongBan = "Phong Ky Thuat" },
                new PhongBan { intMaPhongBan = 2, strTenPhongBan = "Phong Nhan Su" },
                new PhongBan { intMaPhongBan = 3, strTenPhongBan = "Phong Ke Toan" },
                new PhongBan { intMaPhongBan = 4, strTenPhongBan = "Phong Kinh Doanh" }
            );

            // 2. Chen du lieu mau cho bang NhanVien (Su dung ep kieu tu Enum)
            modelBuilder.Entity<NhanVien>().HasData(
                new NhanVien
                {
                    intMaNhanVien = 1,
                    strTenNhanVien = "Nguyen Van A",
                    intNamSinh = 1995,
                    boolGioiTinh = true, // Nam
                    dblLuong = 15000000,
                    intTrangThai = (int)TrangThaiNhanVien.DangLam, // Su dung Enum
                    intMaPhongBan = 1
                },
                new NhanVien
                {
                    intMaNhanVien = 2,
                    strTenNhanVien = "Tran Thi B",
                    intNamSinh = 1998,
                    boolGioiTinh = false, // Nu
                    dblLuong = 12000000,
                    intTrangThai = (int)TrangThaiNhanVien.NghiPhep, // Su dung Enum
                    intMaPhongBan = 2
                },
                new NhanVien
                {
                    intMaNhanVien = 3,
                    strTenNhanVien = "Le Van C",
                    intNamSinh = 1990,
                    boolGioiTinh = true, // Nam
                    dblLuong = 20000000,
                    intTrangThai = (int)TrangThaiNhanVien.DangLam, // Su dung Enum
                    intMaPhongBan = 3
                },
                new NhanVien
                {
                    intMaNhanVien = 4,
                    strTenNhanVien = "Pham Thi D",
                    intNamSinh = 2000,
                    boolGioiTinh = false, // Nu
                    dblLuong = 10000000,
                    intTrangThai = (int)TrangThaiNhanVien.DaNghi, // Su dung Enum
                    intMaPhongBan = 4
                },
                new NhanVien
                {
                    intMaNhanVien = 5,
                    strTenNhanVien = "Hoang Van E",
                    intNamSinh = 1995,
                    boolGioiTinh = true, // Nam
                    dblLuong = 18000000,
                    intTrangThai = (int)TrangThaiNhanVien.DangLam, // Su dung Enum
                    intMaPhongBan = 1
                },
                new NhanVien
                {
                    intMaNhanVien = 6,
                    strTenNhanVien = "Ngo Thi F",
                    intNamSinh = 1997,
                    boolGioiTinh = false, // Nu
                    dblLuong = 14000000,
                    intTrangThai = (int)TrangThaiNhanVien.DangLam, // Su dung Enum
                    intMaPhongBan = 2
                }
            );
        }
    }
}