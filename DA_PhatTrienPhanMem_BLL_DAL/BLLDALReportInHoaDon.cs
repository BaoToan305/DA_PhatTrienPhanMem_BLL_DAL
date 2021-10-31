using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DA_PhatTrienPhanMem_BLL_DAL
{
  public  class BLLDALReportInHoaDon
    {
        QL_LinhKienDataContext qlLK = new QL_LinhKienDataContext();

        public DataTable xuatHoaDon(int pMaHD)
        {
            var ds = (from k in qlLK.HOADONs
                      join cthd in qlLK.CHITIETHOADONs on k.MAHD equals cthd.MAHD
                      join sp in qlLK.SANPHAMs on cthd.MASP equals sp.MASP
                      join nv in qlLK.NHANVIENs on k.MANV equals nv.MANV
                      join kh in qlLK.KHACHHANGs on k.MAKH equals kh.MAKH
                      where k.MAHD == pMaHD
                      select new
                      {
                          k.MAHD,
                          k.NGAYLAPHD,
                          k.TONGTIENHD,
                          kh.TENKH,
                          nv.TENNV,
                          sp.TENSP,
                          cthd.SOLUONG,
                          cthd.DONGIABAN,
                          cthd.THANHTIEN,
                      }).ToList();
            DataTable dt = new DataTable();
            dt = ToDataTable(ds);
            return dt;
        }
        private DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dt = new DataTable();
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                dt.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {//inserting property values to datatable rows
                    values[i] = props[i].GetValue(item, null);
                }
                dt.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dt;
        }
        public DateTime? traVeNgayLap(int pMaHD)
        {
            HOADON hd = qlLK.HOADONs.Where(t => t.MAHD == pMaHD).SingleOrDefault();
            if (hd == null)
                return null;
            return hd.NGAYLAPHD;
        }
    }
}
