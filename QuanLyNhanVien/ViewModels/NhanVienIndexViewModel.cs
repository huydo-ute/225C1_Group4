using QuanLyNhanVien.Models;
using System.Collections.Generic;

namespace QuanLyNhanVien.ViewModels
{
    public class NhanVienIndexViewModel
    {
        public string strTen { get; set; }
        public int? intMaPhongBan { get; set; }
        public int? intNamSinh { get; set; }
        public bool? boolGioiTinh { get; set; }
        public string strSortOrder { get; set; } = "ten_asc";
        public List<NhanVien> lstNhanVien { get; set; } = new List<NhanVien>();
    }
}
