using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA_PhatTrienPhanMem_BLL_DAL
{
  public  class BLLDAL_DoiMatKhau
    {
      QL_LinhKienDataContext qlLinhKien = new QL_LinhKienDataContext();
      public bool kiemTraMatKhauCu(string pTenDangNhap, string pMatKhau)
      {
          NGUOIDUNG nv = qlLinhKien.NGUOIDUNGs.Where(t => t.TENDN == pTenDangNhap && t.MATKHAU == pMatKhau).SingleOrDefault();
          if (nv == null)
              return false;
          return true;
      }

      public bool doiMatKhau(string pTenDangNhap, string pMatKhauMoi)
      {
          try
          {
              NGUOIDUNG nd = qlLinhKien.NGUOIDUNGs.Where(t => t.TENDN == pTenDangNhap).SingleOrDefault();
              if (nd == null)
                  return false;
              nd.MATKHAU = pMatKhauMoi;
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
