using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyNhanVien.Enums;

namespace QuanLyNhanVien.Extensions
{
    public static class HtmlHelperExtension
    {
        public static IHtmlContent RenderTrangThai(this IHtmlHelper objHtmlHelper, int intTrangThai)
        {
            var objTrangThai = (TrangThaiNhanVien)intTrangThai;
            string strClass;
            string strNhan;

            switch (objTrangThai)
            {
                case TrangThaiNhanVien.DangLam:
                    strClass = "badge bg-success";
                    strNhan = "Dang Lam";
                    break;
                case TrangThaiNhanVien.NghiPhep:
                    strClass = "badge bg-warning text-dark";
                    strNhan = "Nghi Phep";
                    break;
                case TrangThaiNhanVien.DaNghi:
                    strClass = "badge bg-secondary";
                    strNhan = "Da Nghi";
                    break;
                default:
                    strClass = "badge bg-light text-dark";
                    strNhan = "Khong xac dinh";
                    break;
            }

            return new HtmlString($"<span class=\"{strClass}\">{strNhan}</span>");
        }

        public static IHtmlContent RenderGioiTinh(this IHtmlHelper objHtmlHelper, bool boolGioiTinh)
        {
            if (boolGioiTinh)
                return new HtmlString("<span class=\"badge bg-primary\">Nam</span>");

            return new HtmlString("<span class=\"badge\" style=\"background-color:#e91e8c\">Nu</span>");
        }
    }
}
