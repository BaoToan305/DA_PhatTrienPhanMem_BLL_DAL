using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA_PhatTrienPhanMem_BLL_DAL
{
  public class BLLDAL_PQuyen
    {
      QL_LinhKienDataContext qllikien = new QL_LinhKienDataContext();

      public IQueryable LoadNguoiDung()
      {

          IQueryable ds = from ng in qllikien.NGUOIDUNGs select new { ng.TENDN, ng.MATKHAU, ng.MANV, ng.QUYEN };
          return ds;

      }

        public IQueryable LoadMaNV()
        {
            IQueryable ma = from nv in qllikien.NHANVIENs select new { nv.MANV };
            return ma;
        }

      //them nguoi dung
      public bool themNguoiDung(string pTenDN, string pMatKhau, string pMaNV, Boolean pQuyen)
      {
          try
          {
              NGUOIDUNG ng = new NGUOIDUNG();
              ng.TENDN = pTenDN;
              ng.MATKHAU = pMatKhau;
              ng.MANV = pMaNV;
              ng.QUYEN = pQuyen;
              qllikien.NGUOIDUNGs.InsertOnSubmit(ng);
              qllikien.SubmitChanges();

              return true;
          }
          catch
          {
              return false;
          }
      }
      //sua nguoi dung
      public bool suaNguoiDung(string pTenDN, string pMatKhau, string pMaNV, Boolean pQuyen)
      {
          try
          {
              NGUOIDUNG ng = qllikien.NGUOIDUNGs.Where(t => t.TENDN == pTenDN).SingleOrDefault();
              if (ng == null)
                  return false;      
              ng.MATKHAU = pMatKhau;
              ng.MANV = pMaNV;
              ng.QUYEN = pQuyen;

              qllikien.SubmitChanges();

              return true;
          }
          catch
          {
              return false;
          }
      }
      //xóa nguoi dung
      public bool xoaNguoiDung(string pMaNV)
      {
          try
          {
              NGUOIDUNG ng =   qllikien.NGUOIDUNGs.Where(t => t.MANV == pMaNV).SingleOrDefault();
              if (ng == null)
                  return false;
              qllikien.NGUOIDUNGs.DeleteOnSubmit(ng);
              qllikien.SubmitChanges();

              return true;
          }
          catch
          {
              return false;
          }
      }

      //kiem tra khoa chinh nguoi dung
      public bool ktKhoaChinhNguoiDung(string pTenDN)
      {
          QL_NGUOIDUNGNHOMNGUOIDUNG ng = qllikien.QL_NGUOIDUNGNHOMNGUOIDUNGs.Where(t => t.TENDN == pTenDN).SingleOrDefault();
          if (ng == null)
              return true;
          return false;
      }

      //nhom nguoi dung
      public IQueryable LoadNNDung()
      {
          IQueryable ds = from nnd in qllikien.NHOMNGUOIDUNGs select new { nnd.MANHOM, nnd.TENNHOM, nnd.GHICHU };
          return ds;
      }

      //them nhom nguoi dung

      public bool themNNguoiDung(string pMaNhom,string pTenNhom,string pGhiChu)
      {
          try
          {
              NHOMNGUOIDUNG nnd = new NHOMNGUOIDUNG();
              nnd.MANHOM = pMaNhom;
              nnd.TENNHOM = pTenNhom;
              nnd.GHICHU = pGhiChu;
              qllikien.NHOMNGUOIDUNGs.InsertOnSubmit(nnd);
              qllikien.SubmitChanges();
              return true;
          }
          catch
          {
              return false;
          }
      }
      //Sua nhom nguoi dung
       public bool suaNNguoiDung(string pMaNhom,string pTenNhom,string pGhiChu)
      {
          try
          {
              NHOMNGUOIDUNG nnd = qllikien.NHOMNGUOIDUNGs.Where(t => t.MANHOM == pMaNhom).SingleOrDefault();
              if (nnd == null)
                  return false;
              nnd.TENNHOM = pTenNhom;
              nnd.GHICHU = pGhiChu;
              qllikien.SubmitChanges();
              return true;

          }
          catch
          {
              return false;
          }
      }
      //Xoa nhom nguoi dung
       public bool xoaNNguoiDung(string pMaNhom)
       {
           try
           {
               NHOMNGUOIDUNG nnd = qllikien.NHOMNGUOIDUNGs.Where(t => t.MANHOM == pMaNhom).SingleOrDefault();
               if (nnd == null)
                   return false;
               qllikien.NHOMNGUOIDUNGs.DeleteOnSubmit(nnd);
               qllikien.SubmitChanges();
               return true;
           }
           catch
           {
               return false;
           }
       }

      //kt khoa chinh nhom nguoi dung
      public bool ktNNguoiDung(string pMaNhom)
       {
           NHOMNGUOIDUNG nnd = qllikien.NHOMNGUOIDUNGs.Where(t => t.MANHOM == pMaNhom).FirstOrDefault();
           if (nnd == null)
               return true;
           return false;
       }
      //Nguoi dung nhom nguoi dung
      public IQueryable LoadNDNhomNDung()
      {
          IQueryable ds = from ngdungNNDung in qllikien.QL_NGUOIDUNGNHOMNGUOIDUNGs select new { ngdungNNDung.MANHOM, ngdungNNDung.TENDN, ngdungNNDung.GHICHU };
          return ds;
      }
      // Nguoi dung nhom nguoi dung 
      public IQueryable LoadQLNguoiDung()
      {

          IQueryable ng = from k in qllikien.QL_NGUOIDUNGNHOMNGUOIDUNGs select new { k.TENDN, k.MANHOM, k.GHICHU };
          return ng;

      }
      // lay ma nhomnguoi dung
      public IQueryable LoadMaNhom()
      {
          IQueryable ma = from m in qllikien.NHOMNGUOIDUNGs select new { m.MANHOM, m.TENNHOM };
          return ma;

      }
      //lay ten dn
      public IQueryable LoadTenDN()
      {
          IQueryable ten = from t in qllikien.NGUOIDUNGs select new { t.TENDN };
          return ten;
      }

      public string traVeTenNhom(string pMaNhom)
      {
          NHOMNGUOIDUNG ng = qllikien.NHOMNGUOIDUNGs.Where(t => t.MANHOM == pMaNhom).SingleOrDefault();
          if (ng == null)
              return null;
          return ng.TENNHOM;
      }
      //them quan ly nguoi dung nhom nguoi dung
      public bool ThemQLNguoiDung(string _TenDN, string _MaNhom, string _GhiChu)
      {
          try
          {
              QL_NGUOIDUNGNHOMNGUOIDUNG ngdung = new QL_NGUOIDUNGNHOMNGUOIDUNG();
              ngdung.TENDN = _TenDN;
              ngdung.MANHOM = _MaNhom;
              ngdung.GHICHU = _GhiChu;

              qllikien.QL_NGUOIDUNGNHOMNGUOIDUNGs.InsertOnSubmit(ngdung);
              qllikien.SubmitChanges();

              return true;
          }
          catch
          {
              return false;
          }

      }
      //xoa quan ly nguoi dung nhom nguoi dung
      public bool xoaQLNguoiDung(string pTenDN, string pMaNhom)
      {
          try
          {
              QL_NGUOIDUNGNHOMNGUOIDUNG nv = qllikien.QL_NGUOIDUNGNHOMNGUOIDUNGs.Where(t => t.TENDN == pTenDN && t.MANHOM == pMaNhom).SingleOrDefault();
              if (nv == null)
                  return false;

              qllikien.QL_NGUOIDUNGNHOMNGUOIDUNGs.DeleteOnSubmit(nv);
              qllikien.SubmitChanges();
              return true;
          }
          catch
          {
              return false;
          }
      }
      //kiem tra co nguoi dung da co nhom hay chua
      public bool ktKhoaChinhQLNguoiDung(string pTenDN, string pMaNhom)
      {
          QL_NGUOIDUNGNHOMNGUOIDUNG ng = qllikien.QL_NGUOIDUNGNHOMNGUOIDUNGs.Where(t => t.TENDN == pTenDN && t.MANHOM == pMaNhom).SingleOrDefault();
          if (ng == null)
              return true;
          return false;
      }

      //load man hinh
      public IQueryable LoadManHinh()
      {
          IQueryable mh = from h in qllikien.MANHINHs select new { h.MAMH, h.TENMH };
          return mh;
      }
      //them man hinh
      public bool themManHinh(string pMaMH,string pTenMH)
      {
          try
          {
              MANHINH mh = new MANHINH();
              mh.MAMH = pMaMH;
              mh.TENMH = pTenMH;
              qllikien.MANHINHs.InsertOnSubmit(mh);
              qllikien.SubmitChanges();
              return true;
          }
          catch
          {
              return false;
          }
      }
      //sua man hinh
      public bool suaManHinh(string pMaMH, string pTenMH)
      {
          try
          {
              MANHINH mh = qllikien.MANHINHs.Where(t => t.MAMH == pMaMH).SingleOrDefault();
              if (mh == null)
                  return false;
              mh.TENMH = pTenMH;              
              qllikien.SubmitChanges();
              return true;
          }
          catch
          {
              return false;
          }
      }
      //xoa man hinh
      public bool xoaManHinh(string pMaMH)
      {
          try
          {
              MANHINH mh = qllikien.MANHINHs.Where(t => t.MAMH == pMaMH).SingleOrDefault();
              if (mh == null)
                  return false;
              qllikien.MANHINHs.DeleteOnSubmit(mh);
              qllikien.SubmitChanges();
              return true;
          }
          catch
          {
              return false;
          }
      }
      //kt khoa chinh man hinh
      public bool ktKhoaChinhManhHinh(string pMaMH)
      {
          MANHINH mh = qllikien.MANHINHs.Where(t => t.MAMH==pMaMH).SingleOrDefault();
          if (mh == null)
              return true;
          return false;
      }
      
      //load phan quyen
      public IQueryable LoadPhanQuyen()
      {
          IQueryable pq = from q in qllikien.PHANQUYENs select new { q.MAMH,q.MANHOM,q.COQUYEN};
          return pq;
      }
      //them phan quyen
      public bool themPQ(string pMaNhom, string pMaMH,Boolean Quyen)
      {
          try
          {
              PHANQUYEN pq = new PHANQUYEN();
              pq.MANHOM = pMaNhom;
              pq.MAMH = pMaMH;
              pq.COQUYEN = Quyen;
              qllikien.PHANQUYENs.InsertOnSubmit(pq);
              qllikien.SubmitChanges();
              return true;
          }
          catch
          {
              return false;
          }
      }

      //sua phan quyen
      public bool suaPQ(string pMaNhom, string pMaMH, Boolean Quyen)
      {
          try
          {
              PHANQUYEN pq = qllikien.PHANQUYENs.Where(t => t.MANHOM == pMaNhom &&t.MAMH==pMaMH).SingleOrDefault();
              if (pq == null)
                  return false;
              pq.MAMH = pMaMH;
              pq.COQUYEN = Quyen;             
              qllikien.SubmitChanges();
              return true;
          }
          catch
          {
              return false;
          }
      }
      //xoa phan quyen
      public bool xoaPQ(string pMaNhom, string pMaMH)
      {
          try
          {
              PHANQUYEN pq = qllikien.PHANQUYENs.Where(t => t.MANHOM == pMaNhom && t.MAMH == pMaMH).SingleOrDefault();
              if (pq == null)
                  return false;
              qllikien.PHANQUYENs.DeleteOnSubmit(pq);
              qllikien.SubmitChanges();
              return true;
          }
          catch
          {
              return false;
          }
      }
      //kt khoa chinh PQ
      public bool ktKhoaChinhPQ(string pMaNhom, string pMaMH)
      {
          PHANQUYEN pq = qllikien.PHANQUYENs.Where(t => t.MANHOM == pMaNhom && t.MAMH == pMaMH).SingleOrDefault();
          if (pq == null)
              return true;
          return false;
      }
    }
}
