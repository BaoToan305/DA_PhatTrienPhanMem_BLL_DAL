using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA_PhatTrienPhanMem_BLL_DAL
{
   public class BLLDAL_ChucVu
    {
       QL_LinhKienDataContext qliLk = new QL_LinhKienDataContext();

       public IQueryable LoadChucVu()
       {
           IQueryable cv = from c in qliLk.CHUCVUs select new { c.MACV, c.TENCHUCVU };
           return cv;
       }

       public bool ktKhoaChinh(string pMaCV)
       {
           CHUCVU cv = qliLk.CHUCVUs.Where(t => t.MACV == pMaCV).SingleOrDefault();
           if (cv == null)
               return true;
           return false;
       }

       public bool themCV(string pMaCV,string pTenCV)
       {
           try
           {
               CHUCVU cv = new CHUCVU();

               cv.MACV = pMaCV;
               cv.TENCHUCVU = pTenCV;

               qliLk.CHUCVUs.InsertOnSubmit(cv);
               qliLk.SubmitChanges();
               return true;
           }
           catch
           {
               return false;
           }
       }

       public bool xoaCV(string pMaCV)
       {
           try
           {
           CHUCVU cv = qliLk.CHUCVUs.Where(t => t.MACV == pMaCV).SingleOrDefault();
           if (cv == null)
               return false;
           qliLk.CHUCVUs.DeleteOnSubmit(cv);
           qliLk.SubmitChanges();
           return true;
           }
           catch
           {
               return false;
           }
       }

       public bool suaCV(string pMaCV,string pTenCV)
       {
           try
           {
               CHUCVU cv = qliLk.CHUCVUs.Where(t => t.MACV == pMaCV).SingleOrDefault();
               if (cv == null)
                   return false;
               cv.TENCHUCVU = pTenCV;
               qliLk.SubmitChanges();
               return true;
           }
           catch
           {
               return false;
           }
       }
    }
}
