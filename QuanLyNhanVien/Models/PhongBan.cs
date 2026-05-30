using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyNhanVien.Models
{
    public class PhongBan
    {
        [Key]
        public int intMaPhongBan { get; set; }

        [Required(ErrorMessage = "Ten phong ban khong duoc de trong")]
        [StringLength(100, ErrorMessage = "Ten phong ban khong vuot qua 100 ky tu")]
        public string strTenPhongBan { get; set; }

        // Navigation property: 1 Phòng ban có nhiều Nhân viên (tiền tố lst cho danh sách)
        public virtual ICollection<NhanVien> lstNhanVien { get; set; }
    }
}