using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA_PhatTrienPhanMem_BLL_DAL
{
   public class BLLDAL_BanHang
    {
        QL_LinhKienDataContext qlLK = new QL_LinhKienDataContext();

        public List<SANPHAM> loadSanPhamCombobox()
        {
            qlLK.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, qlLK.SANPHAMs);
            var ds = new List<SANPHAM>();
            ds = (from sp in qlLK.SANPHAMs select sp).ToList();
            return ds;
        }

        public int? traVeSoLuongSP(string pMaSP)
        {
            try
            {
                SANPHAM sp = qlLK.SANPHAMs.Where(t => t.MASP == pMaSP).SingleOrDefault();
                if (sp == null)
                    return null;
                return sp.SOLUONG;

            }
            catch
            {
                return null;
            }
        }

        public double? traVeDonGiaBan(string pMaSP)
        {
            try
            {
                SANPHAM sp = qlLK.SANPHAMs.Where(t => t.MASP == pMaSP).SingleOrDefault();
                if (sp == null)
                    return null;
                return sp.DONGIABAN;
            }
            catch
            {
                return null;
            }
        }

        public string traVeMaSP(string pMaSP)
        {
            try
            {
                SANPHAM sp = qlLK.SANPHAMs.Where(t => t.MASP == pMaSP).SingleOrDefault();
                if (sp == null)
                    return null;
                return sp.MASP;
            }
            catch
            {

                return null;
            }
        }
        public string traVeTinhTrangSP(string pTinhTrang)
        {
            try
            {
                SANPHAM sp = qlLK.SANPHAMs.Where(t => t.MASP == pTinhTrang).SingleOrDefault();
                if (sp == null)
                    return null;
                return sp.TINHTRANG;
            }
            catch
            {
                return null;
            }
        }
        public string traVeTenSP(string pTenSP)
        {
            try
            {
                SANPHAM sp = qlLK.SANPHAMs.Where(t => t.MASP == pTenSP).SingleOrDefault();
                if (sp == null)
                    return null;
                return sp.TENSP;
            }
            catch
            {
                return null;
            }
        }

        public bool themBanHang(string pMaSP,string pTenSP,string pMaKH,string ptinhTrang,int pSL,double pdonGia,double pthanhTien, List<BanHang> ds)
        {
            try
            {
                BanHang bh = new BanHang();
                bh.MaSP = pMaSP;
                bh.TenSP = pTenSP;
                bh.MaKH = pMaKH;
                bh.TinhTrang = ptinhTrang;
                bh.Soluong = pSL;
                bh.Thanhtien = pthanhTien;
                bh.Dongiaban = pdonGia;
                ds.Add(bh);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool suaBanHang(string pMaSP,string pTenSP, string pMaKH, string ptinhTrang, int pSL, double pdonGia, double pthanhTien, List<BanHang> ds)
        {
            try
            {
                BanHang bh = ds.Where(t => t.MaSP == pMaSP).SingleOrDefault();
                if (bh == null)
                    return false;
                bh.TenSP = pTenSP;
                bh.MaKH = pMaKH;
                bh.TinhTrang = ptinhTrang;
                bh.Soluong = pSL;
                bh.Thanhtien = pthanhTien;
                bh.Dongiaban = pdonGia;
                    


                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ktKhoaChinh(string pMaSP, List<BanHang> ds)
        {
            BanHang bh = ds.Where(t => t.MaSP == pMaSP ).SingleOrDefault();
            if (bh == null)
                return true;
            return false;

        }

        public int? tongSoLuongSPTrongList(string pMaSP, List<BanHang> ds)
        {
            int? soLuong = 0;
            for (int i = 0; i < ds.Count; i++)
            {
                if (ds[i].MaSP == pMaSP)
                {
                    soLuong += ds[i].Soluong;
                }
            }
            return soLuong;
        }

        public IQueryable LoadTenKH()
        {
            IQueryable KH = from l in qlLK.KHACHHANGs select new { l.MAKH, l.TENKH };
            return KH;
        }

        public int? traVeHieuSoSPKhiCapNhat(string pMaSP,  int pSoLuong, List<BanHang> ds)
        {
            int? soLuong = 0;
            int a = pSoLuong;
            for (int i = 0; i < ds.Count; i++)
            {
                if (ds[i].MaSP == pMaSP )
                {
                    soLuong += ds[i].Soluong;
                }
            }
            return a - soLuong;
        }

        public bool xoaBanHang(string pMaSP, string pTenSP, List<BanHang> ds)
        {
            try
            {
                BanHang bh = ds.Where(t => t.MaSP == pMaSP).SingleOrDefault();
                if (bh == null)
                    return false;

                ds.Remove(bh);


                return true;
            }
            catch
            {
                return false;
            }
        }
        public class BanHang
        {
            string tenSP,maKH;
            string maSP, tinhTrang;
            int? soluong;
            double? dongiaban, thanhtien;
            public string MaSP { get => maSP; set => maSP = value; }
            public string TinhTrang { get => tinhTrang; set => tinhTrang = value; }
            public int? Soluong { get => soluong; set => soluong = value; }
            public double? Dongiaban { get => dongiaban; set => dongiaban = value; }
            public double? Thanhtien { get => thanhtien; set => thanhtien = value; }
            public string TenSP { get => tenSP; set => tenSP = value; }
            public string MaKH { get => maKH; set => maKH = value; }
        }
    }
}
