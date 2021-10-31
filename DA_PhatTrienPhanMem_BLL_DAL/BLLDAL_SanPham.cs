using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA_PhatTrienPhanMem_BLL_DAL
{
  public  class BLLDAL_SanPham
    {
       QL_LinhKienDataContext qlLinhKien = new QL_LinhKienDataContext();
         public IQueryable LoadSanPham()
       {
           IQueryable sp = from s in qlLinhKien.SANPHAMs select new { s.MASP, s.TENSP, s.MALOAI, s.SOLUONG, s.DONGIANHAP, s.DONGIABAN, s.MANCC, s.TINHTRANG };
           return sp;
       }

        public bool themSP(string pMaSP, string pTenSP, int pSoLuong, double pDGN, double pDGB, string pMaNCC, string pMaLoai)
        {
            try
            {
              SANPHAM sp = new SANPHAM();
              sp.MASP = pMaSP;
              sp.TENSP = pTenSP;
              sp.SOLUONG = pSoLuong;
              sp.MALOAI = pMaLoai;
              sp.DONGIANHAP = pDGN;
              sp.DONGIABAN = pDGB;
              sp.MANCC = pMaNCC;

              if(sp.SOLUONG<=0)
              {
                  sp.TINHTRANG = "Hết hàng";
              }
              else
              {
                  sp.TINHTRANG = "Còn hàng";
              }

              qlLinhKien.SANPHAMs.InsertOnSubmit(sp);
              qlLinhKien.SubmitChanges();
              return true;
          }
          catch
          {
              return false;
          }
        }
      public bool suaSP(string pMaSP, string pTenSP, int pSoLuong, double pDGN, double pDGB, string pMaNCC, string pMaLoai)
      {
          try
          {
              SANPHAM sp = qlLinhKien.SANPHAMs.Where(t=>t.MASP==pMaSP).SingleOrDefault() ;
              if (sp == null)
                  return false;
              sp.MASP = pMaSP;
              sp.TENSP = pTenSP;
              sp.SOLUONG = pSoLuong;           
              sp.DONGIANHAP = pDGN;
              sp.DONGIABAN = pDGB;
              sp.MANCC = pMaNCC;
              sp.MALOAI = pMaLoai;
                
              if (sp.SOLUONG <= 0)
              {
                  sp.TINHTRANG = "Hết hàng";
              }
              else
              {
                  sp.TINHTRANG = "Còn hàng";
              }

              
              qlLinhKien.SubmitChanges();
              return true;
          }
          catch
          {
              return false;
          }
      }
      public bool xoaSP(string pMaSP)
      {
          try
          {
              SANPHAM sp = qlLinhKien.SANPHAMs.Where(t => t.MASP == pMaSP).SingleOrDefault();
              if (sp == null)
                  return false;
              qlLinhKien.SANPHAMs.DeleteOnSubmit(sp);
              qlLinhKien.SubmitChanges();
              return true;
          }
          catch
          {
              return false;
          }
      }

      public bool ktKhoaChinhSP(string pMaSP)
      {
          SANPHAM sp = qlLinhKien.SANPHAMs.Where(t => t.MASP == pMaSP).SingleOrDefault();
          if (sp == null)
              return true;
          return false;

      }
      public IQueryable LoadTenLoai()
      {
          IQueryable loai = from l in qlLinhKien.LOAILINHKIENs select new { l.MALOAI, l.TENLOAI};
          return loai;
      }

        public IQueryable LoadTenNCC()
      {
          IQueryable loai = from l in qlLinhKien.NHACUNGCAPs select new { l.MANCC ,l.TENNCC};
          return loai;
      }
      public string traVeTenLoai(string pMaLoai)
      {
          try
          {
              string kq = "";
              LOAILINHKIEN loai = qlLinhKien.LOAILINHKIENs.Where(t => t.MALOAI == pMaLoai).SingleOrDefault();
              kq = loai.TENLOAI;
              return kq;
          }
          catch
          {
              return null;
          }
      }
      public string traVeTenNCC(string pMaNCC)
      {
          try
          {
              string kq = "";
              NHACUNGCAP ma = qlLinhKien.NHACUNGCAPs.Where(t => t.MANCC == pMaNCC).SingleOrDefault();
              kq = ma.TENNCC;
              return kq;
          }
          catch
          {
              return null;
          }
      }
        public void capNhatTinhTrang()
        {
            qlLinhKien.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, qlLinhKien.SANPHAMs);
            List<SANPHAM> dssp = qlLinhKien.SANPHAMs.ToList();
            foreach (SANPHAM sp in dssp)
            {
                if (sp.SOLUONG == 0)
                {
                    sp.TINHTRANG = "Hết hàng";
                    qlLinhKien.SubmitChanges();
                }
                else
                {
                    sp.TINHTRANG = "Còn";
                    qlLinhKien.SubmitChanges();
                }
            }
        }
        public IQueryable TimKiemSanPham(string pThongTinSP)
      {
          IQueryable ds = from t in qlLinhKien.SANPHAMs where t.TENSP.Contains(pThongTinSP)||t.MASP.Contains(pThongTinSP) select new { t.MASP, t.TENSP, t.MALOAI, t.SOLUONG, t.DONGIABAN, t.DONGIANHAP, t.MANCC, t.TINHTRANG };
                return ds;
      }

     }
 }



