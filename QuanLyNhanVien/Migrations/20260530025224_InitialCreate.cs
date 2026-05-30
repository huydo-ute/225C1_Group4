using Microsoft.EntityFrameworkCore.Migrations;

namespace QuanLyNhanVien.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhongBan",
                columns: table => new
                {
                    intMaPhongBan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strTenPhongBan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongBan", x => x.intMaPhongBan);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    intMaNhanVien = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strTenNhanVien = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    intNamSinh = table.Column<int>(type: "int", nullable: false),
                    boolGioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    dblLuong = table.Column<double>(type: "float", nullable: false),
                    intTrangThai = table.Column<int>(type: "int", nullable: false),
                    intMaPhongBan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.intMaNhanVien);
                    table.ForeignKey(
                        name: "FK_NhanVien_PhongBan_intMaPhongBan",
                        column: x => x.intMaPhongBan,
                        principalTable: "PhongBan",
                        principalColumn: "intMaPhongBan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PhongBan",
                columns: new[] { "intMaPhongBan", "strTenPhongBan" },
                values: new object[,]
                {
                    { 1, "Phong Ky Thuat" },
                    { 2, "Phong Nhan Su" },
                    { 3, "Phong Ke Toan" },
                    { 4, "Phong Kinh Doanh" }
                });

            migrationBuilder.InsertData(
                table: "NhanVien",
                columns: new[] { "intMaNhanVien", "boolGioiTinh", "dblLuong", "intMaPhongBan", "intNamSinh", "intTrangThai", "strTenNhanVien" },
                values: new object[,]
                {
                    { 1, true, 15000000.0, 1, 1995, 0, "Nguyen Van A" },
                    { 5, true, 18000000.0, 1, 1995, 0, "Hoang Van E" },
                    { 2, false, 12000000.0, 2, 1998, 1, "Tran Thi B" },
                    { 6, false, 14000000.0, 2, 1997, 0, "Ngo Thi F" },
                    { 3, true, 20000000.0, 3, 1990, 0, "Le Van C" },
                    { 4, false, 10000000.0, 4, 2000, 2, "Pham Thi D" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_intMaPhongBan",
                table: "NhanVien",
                column: "intMaPhongBan");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "PhongBan");
        }
    }
}
