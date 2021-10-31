using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DA_PhatTrienPhanMem_BLL_DAL;

namespace DA_PhatTrienPhanMem_BLL_DAL
{
   public class BLL_DALHoaDon_CTHD
    {
        QL_LinhKienDataContext qlLK = new QL_LinhKienDataContext();

        public IQueryable LoadHoaDon()
        {
            IQueryable hd = from h in qlLK.HOADONs select new { h.MAHD, h.MANV, h.MAKH, h.NGAYLAPHD, h.TONGTIENHD, h.THANHTOAN, h.TINHTRANG };
            return hd;
        }

        public IQueryable LoadCTHD()
        {
            IQueryable cthd = from c in qlLK.CHITIETHOADONs select new {c.MAHD,c.MASP,c.SOLUONG,c.DONGIABAN,c.THANHTIEN};
                return cthd;
        }

        public string traVeTenKhachHang(string pMaKH)
        {
            try
            {
                KHACHHANG kh = qlLK.KHACHHANGs.Where(t => t.MAKH == pMaKH).SingleOrDefault();
                if (kh == null)
                    return null;
                return kh.TENKH;
            }
            catch
            {
                return null;
            }
        }
        public string traVeTenNhanVien(string pMaNV)
        {
            try
            {
                NHANVIEN nv = qlLK.NHANVIENs.Where(t => t.MANV == pMaNV).SingleOrDefault();
                if (nv == null)
                    return null;
                return nv.TENNV;
            }
            catch
            {
                return null;
            }
        }
        public string traVeTenSanPham(string pMaSP)
        {
            try
            {
                SANPHAM sp = qlLK.SANPHAMs.Where(t => t.MASP == pMaSP).SingleOrDefault();
                if (sp == null)
                    return null;
                return sp.TENSP;
            }
            catch
            {
                return null;
            }
        }
        public int? traVeSoLuong(string pTenSP)
        {
            try
            {
                SANPHAM sp = qlLK.SANPHAMs.Where(t => t.TENSP == pTenSP).SingleOrDefault();
                if (sp == null)
                    return null;
                return sp.SOLUONG;
            }
            catch
            {
                return null;
            }
        }

        public bool khoiPhucSoLuongHuy(int pMaHD)
        {
            try
            {
                HOADON hd = qlLK.HOADONs.Where(t => t.MAHD == pMaHD).SingleOrDefault();
                if (hd == null)
                    return false;
                hd.TINHTRANG = "Hủy";
                qlLK.SubmitChanges();

                List<CHITIETHOADON> ds = qlLK.CHITIETHOADONs.Where(t => t.MAHD == pMaHD).ToList();
                for (int i = 0; i < ds.Count; i++)
                {
                    string ma = ds[i].MASP;
                    SANPHAM sp = qlLK.SANPHAMs.Where(t => t.MASP == ma).SingleOrDefault();
                    sp.SOLUONG += ds[i].SOLUONG;
                    qlLK.SubmitChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
