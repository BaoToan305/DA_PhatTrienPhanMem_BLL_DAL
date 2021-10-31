using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA_PhatTrienPhanMem_BLL_DAL
{
  public class BLLDAL_NhanVien
    {
      QL_LinhKienDataContext qlilk = new QL_LinhKienDataContext();

      public IQueryable LoadNV()
      {
          IQueryable kh = from k in qlilk.NHANVIENs select new { k.MANV, k.TENNV, k.GIOITINH, k.NGAYSINH, k.DIENTHOAI, k.DIACHI, k.MACV, k.NGAYVL };
          return kh;
      }

        public IQueryable timKiemThongTinNV(string pThongTinKH)
        {
            IQueryable nv = from k in qlilk.NHANVIENs where k.TENNV.Contains(pThongTinKH) || k.MANV.Contains(pThongTinKH) select new { k.MANV, k.TENNV, k.GIOITINH, k.NGAYSINH, k.DIENTHOAI, k.DIACHI, k.MACV, k.NGAYVL };
            return nv;
        }

      public bool ktKhoaschinh(string pMaNV)
      {
          NHANVIEN nv = qlilk.NHANVIENs.Where(t => t.MANV == pMaNV).SingleOrDefault();
          if (nv == null)
              return true;
          return false;
      }

      public bool themNV(string pMaNV, string pTenNV, DateTime pNgaySinh, string pGioiTinh, string pSDT, string pMaCV, string pDiaChi,DateTime pNgayVL)
      {
          try
          {

              NHANVIEN nv = new NHANVIEN();
              nv.MANV = pMaNV;
              nv.TENNV = pTenNV;
              nv.NGAYSINH = pNgaySinh;
              nv.GIOITINH = pGioiTinh;
              nv.DIENTHOAI = pSDT;
              nv.MACV = pMaCV;
              nv.DIACHI = pDiaChi;
              nv.NGAYVL = pNgayVL;

              qlilk.NHANVIENs.InsertOnSubmit(nv);
              qlilk.SubmitChanges();
              return true;
          }
          catch
          {
              return false;
          }
      }
      public bool suaNV(string pMaNV, string pTenNV, DateTime pNgaySinh, string pGioiTinh, string pSDT, string pMaCV, string pDiaChi, DateTime pNgayVL)
      {
          try
          {

              NHANVIEN nv = qlilk.NHANVIENs.Where(t => t.MANV== pMaNV).SingleOrDefault();
              if (nv == null)
                  return false;
              nv.MANV = pMaNV;
              nv.TENNV = pTenNV;
              nv.NGAYSINH = pNgaySinh;
              nv.GIOITINH = pGioiTinh;
              nv.DIENTHOAI = pSDT;
              nv.MACV = pMaCV;
              nv.DIACHI = pDiaChi;
              nv.NGAYVL = pNgayVL;

              qlilk.SubmitChanges();
              return true;
          }
          catch
          {
              return false;
          }
      }
      public bool xoaNV(string pMaNV)
      {
          try
          {
              NHANVIEN kh = qlilk.NHANVIENs.Where(t => t.MANV == pMaNV).SingleOrDefault();
              if (kh == null)
                  return false;
              qlilk.NHANVIENs.DeleteOnSubmit(kh);
              qlilk.SubmitChanges();
              return true;
          }
          catch
          {
              return false;
          }
      }
      public IQueryable loadMaCV()
      {
          IQueryable maCV = from cv in qlilk.CHUCVUs select new { cv.TENCHUCVU,cv.MACV };
              return maCV;
      }
      public string traVeTenChucVu(string pMaCV)
      {
          try
          {
              string kq = "";

              CHUCVU cv = qlilk.CHUCVUs.Where(t => t.MACV == pMaCV).SingleOrDefault();
              kq = cv.TENCHUCVU;
              return kq;
          }
          catch
          {
              return null;
          }
      }
    }
}
