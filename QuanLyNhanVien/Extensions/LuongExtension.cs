namespace QuanLyNhanVien.Extensions
{
    public static class LuongExtension
    {
        public static string ToCurrencyVN(this double dblLuong)
        {
            return dblLuong.ToString("N0") + " VND";
        }
    }
}
