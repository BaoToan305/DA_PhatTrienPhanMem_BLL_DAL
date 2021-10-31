using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA_PhatTrienPhanMem_BLL_DAL
{
   public class BLLDAL_KhachHang
    {
       QL_LinhKienDataContext qlilk = new QL_LinhKienDataContext();
       public IQueryable LoadKH()
       {
           IQueryable kh = from k in qlilk.KHACHHANGs select new {k.MAKH,k.TENKH,k.NGAYSINH,k.GIOITINH,k.SDT,k.EMAIL,k.DIACHI };
           return kh;
       }

        public IQueryable timKiemThongTinKH(string pThongTinKH)
        {
            IQueryable nv = from k in qlilk.KHACHHANGs where k.TENKH.Contains(pThongTinKH) || k.MAKH.Contains(pThongTinKH) select new { k.MAKH, k.TENKH, k.NGAYSINH, k.GIOITINH, k.SDT, k.EMAIL, k.DIACHI };
            return nv;
        }
        public bool ktKhoaschinh(string pMaKH)
       {
           KHACHHANG kh = qlilk.KHACHHANGs.Where(t => t.MAKH == pMaKH).SingleOrDefault();
           if (kh == null)
               return true;
           return false;
       }
       public bool themKH(string pMaKh,string pTenKh,DateTime pNgaySinh,string pGioiTinh,string pSDT,string pEmail,string pDiaChi )
       {
           try
           {
            
               KHACHHANG kh = new KHACHHANG();
               kh.MAKH = pMaKh;
               kh.TENKH = pTenKh;
               kh.NGAYSINH = pNgaySinh;
               kh.GIOITINH = pGioiTinh;
               kh.SDT = pSDT;
               kh.EMAIL = pEmail;
               kh.DIACHI = pDiaChi;
               qlilk.KHACHHANGs.InsertOnSubmit(kh);
               qlilk.SubmitChanges();
               return true;
           }
           catch
           {
               return false;
           }
       }
       public bool suaKH(string pMaKh, string pTenKh, DateTime pNgaySinh, string pGioiTinh, string pSDT, string pEmail, string pDiaChi)
       {
           try
           {

               KHACHHANG kh = qlilk.KHACHHANGs.Where(t => t.MAKH == pMaKh).SingleOrDefault();
               if (kh == null)
                   return false;
               kh.MAKH = pMaKh;
               kh.TENKH = pTenKh;
               kh.NGAYSINH = pNgaySinh;
               kh.GIOITINH = pGioiTinh;
               kh.SDT = pSDT;
               kh.EMAIL = pEmail;
               kh.DIACHI = pDiaChi;

               qlilk.SubmitChanges();
               return true;
           }
           catch
           {
               return false;
           }
       }
       public bool xoaKH(string pMaKh)
       {
           try
           {
               KHACHHANG kh = qlilk.KHACHHANGs.Where(t => t.MAKH == pMaKh).SingleOrDefault();
               if (kh == null)
                   return false;
               qlilk.KHACHHANGs.DeleteOnSubmit(kh);
               qlilk.SubmitChanges();
               return true;
           }
           catch
           {
               return false;
           }
       }
    }
}
