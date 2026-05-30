using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNhanVien.Models
{
    public class NhanVien
    {
        [Key]
        public int intMaNhanVien { get; set; }

        [Required(ErrorMessage = "Ten nhan vien khong duoc de trong")]
        [StringLength(100, ErrorMessage = "Ten nhan vien khong vuot qua 100 ky tu")]
        public string strTenNhanVien { get; set; }

        [Required(ErrorMessage = "Nam sinh khong duoc de trong")]
        public int intNamSinh { get; set; }

        [Required(ErrorMessage = "Gioi tinh khong duoc de trong")]
        public bool boolGioiTinh { get; set; }

        // Đề bài ghi "validation số lượng > 0", đối với nhân viên ở đây là Lương
        [Required(ErrorMessage = "Luong khong duoc de trong")]
        [Range(1, double.MaxValue, ErrorMessage = "Luong phai lon hon 0")]
        public double dblLuong { get; set; }

        // Enum trạng thái (lưu dưới dạng int trong database)
        [Required(ErrorMessage = "Trang thai khong duoc de trong")]
        public int intTrangThai { get; set; }

        // Khóa ngoại liên kết với bảng PhongBan
        [Required(ErrorMessage = "Vui long chon phong ban")]
        public int intMaPhongBan { get; set; }

        // Navigation property: Liên kết tới đối tượng PhongBan (tiền tố obj cho object)
        [ForeignKey("intMaPhongBan")]
        public virtual PhongBan objPhongBan { get; set; }
    }
}