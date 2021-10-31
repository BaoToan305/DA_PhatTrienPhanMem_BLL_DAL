using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DA_PhatTrienPhanMem_BLL_DAL.BLLDAL_BanHang;

namespace DA_PhatTrienPhanMem_BLL_DAL
{
  public class BLLDAL_ThanhToan
    {
        QL_LinhKienDataContext qlLK = new QL_LinhKienDataContext();

        public IQueryable LoadTenKH()
        {
            IQueryable kh = from k in qlLK.KHACHHANGs select new { k.TENKH };
            return kh;
        }

        public string traVeNhanVienDiemDanh(string pTenDN)
        {
            try
            {
                NGUOIDUNG ng = qlLK.NGUOIDUNGs.Where(t => t.TENDN == pTenDN).SingleOrDefault();
                if (ng == null)
                    return null;
                NHANVIEN nv = qlLK.NHANVIENs.Where(t => t.MANV == ng.MANV).SingleOrDefault();
                if (nv == null)
                    return null;
                return nv.TENNV;
            }
            catch
            {
                return null;
            }
        }
        public bool ktThanhToan1Lan(string pMaKH)
        {
            try
            {
                HOADON hd = qlLK.HOADONs.Where(t => t.MAKH == pMaKH ).SingleOrDefault();
                if (hd == null)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool xacNhanThanhToan(string pMaNV, string pMaKH, DateTime pNgayLap, double pTongTienHD, double pThanhToan, List<BanHang> ds)
        {
            try
            {
                HOADON hd = new HOADON();
                hd.MANV = pMaNV;
                hd.MAKH = pMaKH;
                hd.NGAYLAPHD = pNgayLap;
                hd.TONGTIENHD = pTongTienHD;
                hd.THANHTOAN = pThanhToan;
                hd.TINHTRANG = "Có hiệu lực";

                qlLK.HOADONs.InsertOnSubmit(hd);
                qlLK.SubmitChanges();
                for (int i = 0; i < ds.Count; i++)
                {
                    CHITIETHOADON cthd = new CHITIETHOADON();
                    cthd.MAHD = hd.MAHD;
                    cthd.MASP = ds[i].MaSP;
                    cthd.SOLUONG = ds[i].Soluong;
                    cthd.DONGIABAN = ds[i].Dongiaban;
                    cthd.THANHTIEN = ds[i].Thanhtien;

                    qlLK.CHITIETHOADONs.InsertOnSubmit(cthd);
                    qlLK.SubmitChanges();
                }
                ds.Clear();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string traVeMaNhanVien(string pTenDN)
        {
            try
            {
                NGUOIDUNG ng = qlLK.NGUOIDUNGs.Where(t => t.TENDN == pTenDN).SingleOrDefault();
                if (ng == null)
                    return null;
                NHANVIEN nv = qlLK.NHANVIENs.Where(t => t.MANV == ng.MANV).SingleOrDefault();
                if (nv == null)
                    return null;
                return nv.MANV;
            }
            catch
            {
                return null;
            }
        }

        //xóa danh sách ảo tạo trước đó
        public List<BanHang> capNhatSauKhiThanhToan(List<BanHang> ds,string pMaKH)
        {
            for (int i = ds.Count - 1; i >= 0; i--)
            {
                if (ds[i].MaKH == pMaKH)
                {
                    ds.Remove(ds[i]);
                }
            }
            return ds;
        }
        public bool thayDoiSoLuongKhiThem(int pMaHD)
        {
            try
            {
                List<CHITIETHOADON> ds = qlLK.CHITIETHOADONs.Where(t => t.MAHD == pMaHD).ToList();
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
        public int traVeMaHD(string pMaKH)
        {
            try
            {
                HOADON hd = qlLK.HOADONs.Where(t => t.MAKH == pMaKH).SingleOrDefault();
                if (hd == null)
                    return -1;
                return hd.MAHD;
            }
            catch
            {
                return -1;
            }
        }
    }
}
