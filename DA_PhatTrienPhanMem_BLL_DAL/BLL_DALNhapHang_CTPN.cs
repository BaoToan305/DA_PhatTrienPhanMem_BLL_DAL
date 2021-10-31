using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA_PhatTrienPhanMem_BLL_DAL
{
    public class BLL_DALNhapHang_CTPN
    {

        QL_LinhKienDataContext qlLK = new QL_LinhKienDataContext();

        public IQueryable LoadPhieuNhap()
        {
            IQueryable pn = from p in qlLK.PHIEUNHAPs select new { p.MAPN, p.MANV, p.MANCC, p.NGAYLAPPN, p.TONGTIENPN, p.TINHTRANG };
            return pn;
        }

        public IQueryable loadMaPN()
        {
            IQueryable pn = from p in qlLK.PHIEUNHAPs select new { p.MAPN };
            return pn;
        }
        public bool ktKhoasChinh(string pMaPN)
        {
            PHIEUNHAP pn = qlLK.PHIEUNHAPs.Where(t => t.MAPN == pMaPN).SingleOrDefault();
            if (pn == null)
                return true;
            return false;
        }

        public bool themPhieuNhap(string pMaPN, string pMaNV, string pMaNCC, DateTime pNgayLap)
        {
            try
            {
                PHIEUNHAP pn = new PHIEUNHAP();
                pn.MAPN = pMaPN;
                pn.MANV = pMaNV;
                pn.MANCC = pMaNCC;
                pn.NGAYLAPPN = pNgayLap;
                pn.TONGTIENPN = 0;
                pn.TINHTRANG = "Có hiệu lực";

                qlLK.PHIEUNHAPs.InsertOnSubmit(pn);
                qlLK.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool khoiPhucSoLuongHuy(string pMaPN)
        {
            try
            {
                PHIEUNHAP pn = qlLK.PHIEUNHAPs.Where(t => t.MAPN == pMaPN).SingleOrDefault();
                if (pn == null)
                    return false;
                pn.TINHTRANG = "Hủy";
                qlLK.SubmitChanges();

                List<CHITIETPHIEUNHAP> ds = qlLK.CHITIETPHIEUNHAPs.Where(t => t.MAPN == pMaPN).ToList();
                for (int i = 0; i < ds.Count; i++)
                {
                    string ma = ds[i].MASP;
                    SANPHAM sp = qlLK.SANPHAMs.Where(t => t.MASP == ma).SingleOrDefault();
                    sp.SOLUONG -= ds[i].SOLUONG;
                    qlLK.SubmitChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ktKhoaChinh(string pMaPN, string pMaSP)
        {
            CHITIETPHIEUNHAP pn = qlLK.CHITIETPHIEUNHAPs.Where(t => t.MAPN == pMaPN && t.MASP == pMaSP).SingleOrDefault();
            if (pn == null)
                return true;
            return false;
        }
        public bool ktTinhTrang(string pMaPN)
        {
            PHIEUNHAP pn = qlLK.PHIEUNHAPs.Where(t => t.MAPN == pMaPN).SingleOrDefault();
            if (pn == null)
                return false;
            if (pn.TINHTRANG == "Hủy")
            {
                return false;
            }
            return true;
        }

        public bool xoaPhieuNhap(string pMaPN)
        {
            try
            {
                PHIEUNHAP pn = qlLK.PHIEUNHAPs.Where(t => t.MAPN == pMaPN).SingleOrDefault();
                if (pn == null)
                    return false;

                qlLK.PHIEUNHAPs.DeleteOnSubmit(pn);
                qlLK.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool suaPhieuNhap(string pMaPN, string pMaNV, string pMaNCC, DateTime pNgayLap, double pTongTien)
        {
            try
            {
                PHIEUNHAP pn = qlLK.PHIEUNHAPs.Where(t => t.MAPN == pMaPN).SingleOrDefault();
                if (pn == null)
                    return false;
                pn.MANV = pMaNV;
                pn.MANCC = pMaNCC;
                pn.NGAYLAPPN = pNgayLap;
                pn.TONGTIENPN = pTongTien;

                qlLK.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<string> LoadTinhTrang()
        {
            List<string> ds = new List<string>();
            ds.Add("Có hiệu lực");
            ds.Add("Hủy");
            return ds;
        }

        public bool themChiTietPhieuNhap(string pMaPN, string pMaSP, int pSoLuong, double pDonGia, double pThanhTien)
        {
            try
            {
                CHITIETPHIEUNHAP ctpn = new CHITIETPHIEUNHAP();
                ctpn.MAPN = pMaPN;
                ctpn.MASP = pMaSP;
                ctpn.SOLUONG = pSoLuong;
                ctpn.DONGIANHAP = pDonGia;
                ctpn.THANHTIEN = pThanhTien;

                qlLK.CHITIETPHIEUNHAPs.InsertOnSubmit(ctpn);
                qlLK.SubmitChanges();

                SANPHAM sp = qlLK.SANPHAMs.Where(t => t.MASP == pMaSP).SingleOrDefault();
                if (sp == null)
                    return false;
                sp.SOLUONG += pSoLuong;
                qlLK.SubmitChanges();

                PHIEUNHAP pn = qlLK.PHIEUNHAPs.Where(t => t.MAPN == pMaPN).SingleOrDefault();
                pn.TONGTIENPN += pSoLuong * pDonGia;
                qlLK.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public double? traVeDonGiaNhap(string pMaSP)
        {
            SANPHAM sp = qlLK.SANPHAMs.Where(t => t.MASP == pMaSP).SingleOrDefault();
            if (sp == null)
                return -1;
            return sp.DONGIANHAP;
        }

        public IQueryable LoadCTPN()
        {
            IQueryable ctpn = from ct in qlLK.CHITIETPHIEUNHAPs select new { ct.MAPN, ct.MASP, ct.SOLUONG, ct.DONGIANHAP, ct.THANHTIEN };
            return ctpn;
        }

        public IQueryable LoadTenNV()
        {
            IQueryable nv = from n in qlLK.NHANVIENs select new { n.MANV, n.TENNV };
            return nv;
        }

        public string traVeTenNV(string pMaNV)
        {
            try
            {

                string kq = "";
                NHANVIEN nv = qlLK.NHANVIENs.Where(t => t.MANV == pMaNV).SingleOrDefault();
                kq = nv.TENNV;
                return kq;
            }
            catch
            {
                return "";
            }
        }
        public string traVeTenNCC(string pMaNCC)
        {
            try
            {
                NHACUNGCAP nv = qlLK.NHACUNGCAPs.Where(t => t.MANCC == pMaNCC).SingleOrDefault();
                if (nv == null)
                    return null;
                return nv.TENNCC;
            }
            catch
            {
                return null;
            }
        }
        public IQueryable loadChiTietHoaDonTheoMa(string pMaPN)
        {
            IQueryable ds = from k in qlLK.CHITIETPHIEUNHAPs where k.MAPN == pMaPN select new { k.MAPN, k.MASP, k.SOLUONG, k.DONGIANHAP, k.THANHTIEN };
            return ds;
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
    }
}
