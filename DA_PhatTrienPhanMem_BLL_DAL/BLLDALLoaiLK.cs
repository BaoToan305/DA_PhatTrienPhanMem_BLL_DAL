using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA_PhatTrienPhanMem_BLL_DAL
{
   public class BLLDALLoaiLK
    {
       QL_LinhKienDataContext qlLinhKien = new QL_LinhKienDataContext();

       public IQueryable LoadLinhKien()
       {
           IQueryable lk = from l in qlLinhKien.LOAILINHKIENs select new { l.MALOAI, l.TENLOAI };
           return lk;
       }
       public bool ktKhoasChinh(string pMaLoai)
       {
           LOAILINHKIEN lk = qlLinhKien.LOAILINHKIENs.Where(t=>t.MALOAI==pMaLoai).SingleOrDefault();
           if(lk== null)
               return true;
           return false;
       }

       public bool themLoaiLK(string pMaLoai, string pTenLoai)
       {
           try
           {
               LOAILINHKIEN lk = new LOAILINHKIEN();
               lk.MALOAI = pMaLoai;
               lk.TENLOAI = pTenLoai;
               qlLinhKien.LOAILINHKIENs.InsertOnSubmit(lk);
               qlLinhKien.SubmitChanges();
               return true;
           }
           catch
           {
               return false;
           }
       }

       public bool suaLoaiLK(string pMaLoai, string pTenLoai)
       {
           try
           {
               LOAILINHKIEN lk = qlLinhKien.LOAILINHKIENs.Where(t => t.MALOAI == pMaLoai).SingleOrDefault();
               if (lk == null)
                   return false;
               
               lk.TENLOAI = pTenLoai;
               
               qlLinhKien.SubmitChanges();
               return true;
           }
           catch
           {
               return false;
           }
       }


       public bool xoaLoaiLK(string pMaLoai)
       {
           try
           {
               LOAILINHKIEN lk = qlLinhKien.LOAILINHKIENs.Where(t => t.MALOAI == pMaLoai).SingleOrDefault();
               if (lk == null)
                   return false;
               qlLinhKien.LOAILINHKIENs.DeleteOnSubmit(lk);
               qlLinhKien.SubmitChanges();
               return true;
           }
           catch
           {
               return false;
           }
       }

    }
}
