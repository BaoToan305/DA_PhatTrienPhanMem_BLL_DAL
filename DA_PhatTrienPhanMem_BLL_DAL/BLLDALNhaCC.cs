using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA_PhatTrienPhanMem_BLL_DAL
{
   public class BLLDALNhaCC
    {
       QL_LinhKienDataContext qliLinkKien = new QL_LinhKienDataContext();

       public IQueryable loadNCC()
       {
           IQueryable ncc = from n in qliLinkKien.NHACUNGCAPs select new { n.MANCC,n.TENNCC,n.DIENTHOAI,n.DIACHI };
           return ncc;
       }

       public bool ktKhoasChinh(string pMaNCC)
       {
           NHACUNGCAP n = qliLinkKien.NHACUNGCAPs.Where(t => t.MANCC==pMaNCC).SingleOrDefault();
           if (n == null)
               return true;
           return false;
       }

       public bool themNCC(string pMaNCC,string pTenNCC,string pDienThoai,string pDiaChi)
       {
           try
           {
               NHACUNGCAP ncc = new NHACUNGCAP();
               ncc.MANCC = pMaNCC;
               ncc.TENNCC = pTenNCC;
               ncc.DIENTHOAI = pDiaChi;
               ncc.DIACHI = pDiaChi;

               qliLinkKien.NHACUNGCAPs.InsertOnSubmit(ncc);
               qliLinkKien.SubmitChanges();
               return true;
           }
           catch
           {
               return false;
           }
       }
       public bool suaNCC(string pMaNCC, string pTenNCC, string pDienThoai, string pDiaChi)
       {
           try
           {
               NHACUNGCAP n = qliLinkKien.NHACUNGCAPs.Where(t => t.MANCC == pMaNCC).SingleOrDefault();
               if (n == null)
                   return false;
               n.TENNCC = pTenNCC;
               n.DIENTHOAI = pDiaChi;
               n.DIACHI = pDiaChi;

               qliLinkKien.SubmitChanges();
               return true;
           }
           catch
           {
               return false;
           }
       }
       public bool xoaNCC(string pMaNCC)
       {
           try
           {
               NHACUNGCAP n = qliLinkKien.NHACUNGCAPs.Where(t => t.MANCC == pMaNCC).SingleOrDefault();
               if (n == null)
                   return false;

               qliLinkKien.NHACUNGCAPs.DeleteOnSubmit(n);
               qliLinkKien.SubmitChanges();
               return true;
           }
           catch
           {
               return false;
           }
       }
    }
}
