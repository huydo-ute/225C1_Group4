using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyNhanVien.Enums;
using QuanLyNhanVien.Models;
using QuanLyNhanVien.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyNhanVien.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly AppDbContext objDbContext;

        public NhanVienController(AppDbContext objDbContext)
        {
            this.objDbContext = objDbContext;
        }

        // GET: NhanVien
        public async Task<IActionResult> Index(
            string strTen,
            int? intMaPhongBan,
            int? intNamSinh,
            bool? boolGioiTinh,
            string strSortOrder = "ten_asc")
        {
            var strTenLoc = string.IsNullOrWhiteSpace(strTen) ? null : strTen.Trim();
            var strSortOrderHopLe = strSortOrder == "ten_desc" ? "ten_desc" : "ten_asc";

            var lstNhanVien = await TaoQueryIndex(strTenLoc, intMaPhongBan, intNamSinh, boolGioiTinh, strSortOrderHopLe)
                .ToListAsync();

            var objViewModel = new NhanVienIndexViewModel
            {
                strTen = strTenLoc,
                intMaPhongBan = intMaPhongBan,
                intNamSinh = intNamSinh,
                boolGioiTinh = boolGioiTinh,
                strSortOrder = strSortOrderHopLe,
                lstNhanVien = lstNhanVien
            };

            await NapDropdownLoc(intMaPhongBan, boolGioiTinh);

            return View(objViewModel);
        }

        // GET: NhanVien/Details/5
        public async Task<IActionResult> Details(int? intId)
        {
            if (intId == null)
            {
                return NotFound();
            }

            var objNhanVien = await objDbContext.NhanVien
                .AsNoTracking()
                .Include(objNv => objNv.objPhongBan)
                .FirstOrDefaultAsync(objNv => objNv.intMaNhanVien == intId);

            if (objNhanVien == null)
            {
                return NotFound();
            }

            return View(objNhanVien);
        }

        // GET: NhanVien/Create
        public async Task<IActionResult> Create()
        {
            await NapDropdownForm();
            return View();
        }

        // POST: NhanVien/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("strTenNhanVien,intNamSinh,boolGioiTinh,dblLuong,intTrangThai,intMaPhongBan")] NhanVien objNhanVien)
        {
            if (ModelState.IsValid)
            {
                objDbContext.Add(objNhanVien);
                await objDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            await NapDropdownForm(objNhanVien.intMaPhongBan, objNhanVien.intTrangThai);
            return View(objNhanVien);
        }

        // GET: NhanVien/Edit/5
        public async Task<IActionResult> Edit(int? intId)
        {
            if (intId == null)
            {
                return NotFound();
            }

            var objNhanVien = await objDbContext.NhanVien
                .AsNoTracking()
                .FirstOrDefaultAsync(objNv => objNv.intMaNhanVien == intId);

            if (objNhanVien == null)
            {
                return NotFound();
            }

            await NapDropdownForm(objNhanVien.intMaPhongBan, objNhanVien.intTrangThai);
            return View(objNhanVien);
        }

        // POST: NhanVien/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int intId,
            [Bind("intMaNhanVien,strTenNhanVien,intNamSinh,boolGioiTinh,dblLuong,intTrangThai,intMaPhongBan")] NhanVien objNhanVien)
        {
            if (intId != objNhanVien.intMaNhanVien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var objNhanVienCu = await objDbContext.NhanVien.FindAsync(intId);
                if (objNhanVienCu == null)
                {
                    return NotFound();
                }

                objNhanVienCu.strTenNhanVien = objNhanVien.strTenNhanVien;
                objNhanVienCu.intNamSinh = objNhanVien.intNamSinh;
                objNhanVienCu.boolGioiTinh = objNhanVien.boolGioiTinh;
                objNhanVienCu.dblLuong = objNhanVien.dblLuong;
                objNhanVienCu.intTrangThai = objNhanVien.intTrangThai;
                objNhanVienCu.intMaPhongBan = objNhanVien.intMaPhongBan;

                await objDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            await NapDropdownForm(objNhanVien.intMaPhongBan, objNhanVien.intTrangThai);
            return View(objNhanVien);
        }

        // GET: NhanVien/Delete/5
        public async Task<IActionResult> Delete(int? intId)
        {
            if (intId == null)
            {
                return NotFound();
            }

            var objNhanVien = await objDbContext.NhanVien
                .AsNoTracking()
                .Include(objNv => objNv.objPhongBan)
                .FirstOrDefaultAsync(objNv => objNv.intMaNhanVien == intId);

            if (objNhanVien == null)
            {
                return NotFound();
            }

            return View(objNhanVien);
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int intId)
        {
            var objNhanVien = await objDbContext.NhanVien.FindAsync(intId);
            if (objNhanVien == null)
            {
                return NotFound();
            }

            objDbContext.NhanVien.Remove(objNhanVien);
            await objDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private IQueryable<NhanVien> TaoQueryIndex(
            string strTen,
            int? intMaPhongBan,
            int? intNamSinh,
            bool? boolGioiTinh,
            string strSortOrder)
        {
            var objQuery = objDbContext.NhanVien
                .AsNoTracking()
                .Include(objNv => objNv.objPhongBan)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(strTen))
            {
                objQuery = objQuery.Where(objNv => objNv.strTenNhanVien.Contains(strTen));
            }

            if (intMaPhongBan.HasValue)
            {
                objQuery = objQuery.Where(objNv => objNv.intMaPhongBan == intMaPhongBan.Value);
            }

            if (intNamSinh.HasValue)
            {
                objQuery = objQuery.Where(objNv => objNv.intNamSinh == intNamSinh.Value);
            }

            if (boolGioiTinh.HasValue)
            {
                objQuery = objQuery.Where(objNv => objNv.boolGioiTinh == boolGioiTinh.Value);
            }

            if (strSortOrder == "ten_desc")
            {
                return objQuery
                    .OrderByDescending(objNv => objNv.strTenNhanVien)
                    .ThenBy(objNv => objNv.intMaNhanVien);
            }

            return objQuery
                .OrderBy(objNv => objNv.strTenNhanVien)
                .ThenBy(objNv => objNv.intMaNhanVien);
        }

        private async Task<SelectList> TaoSelectListPhongBan(int? intMaPhongBan = null)
        {
            var lstPhongBan = await objDbContext.PhongBan
                .AsNoTracking()
                .OrderBy(objPb => objPb.strTenPhongBan)
                .ToListAsync();

            return new SelectList(lstPhongBan, "intMaPhongBan", "strTenPhongBan", intMaPhongBan);
        }

        private SelectList TaoSelectListTrangThai(int? intTrangThai = null)
        {
            var lstTrangThai = Enum.GetValues(typeof(TrangThaiNhanVien))
                .Cast<TrangThaiNhanVien>()
                .Select(objTt => new { intGiaTri = (int)objTt, strTen = objTt.ToString() });

            return new SelectList(lstTrangThai, "intGiaTri", "strTen", intTrangThai);
        }

        private SelectList TaoSelectListGioiTinh(bool? boolGioiTinh = null)
        {
            var lstGioiTinh = new List<object>
            {
                new { boolGiaTri = true, strTen = "Nam" },
                new { boolGiaTri = false, strTen = "Nu" }
            };

            return new SelectList(lstGioiTinh, "boolGiaTri", "strTen", boolGioiTinh);
        }

        private async Task NapDropdownForm(int? intMaPhongBan = null, int? intTrangThai = null)
        {
            ViewBag.objSelectPhongBan = await TaoSelectListPhongBan(intMaPhongBan);
            ViewBag.objSelectTrangThai = TaoSelectListTrangThai(intTrangThai);
        }

        private async Task NapDropdownLoc(int? intMaPhongBan = null, bool? boolGioiTinh = null)
        {
            ViewBag.objSelectPhongBan = await TaoSelectListPhongBan(intMaPhongBan);
            ViewBag.objSelectGioiTinh = TaoSelectListGioiTinh(boolGioiTinh);
        }
    }
}
