using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DA_PhatTrienPhanMem_BLL_DAL
{
  public class BLL_DALThongKe
    {

        QL_LinhKienDataContext qlLK = new QL_LinhKienDataContext();


        public List<int> loadDuLieu(int pBatDau, int pKetThuc)
        {
            List<int> ds = new List<int>();
            for (int i = pBatDau; i <= pKetThuc; i++)
            {
                ds.Add(i);
            }
            return ds;
        }

        public bool kiemTraNamNhuan(int pNam)
        {
            if (pNam % 400 == 0 || (pNam % 4 == 0 && pNam % 100 != 0))
            {
                return true;
            }
            return false;
        }

        public bool kiemTraNgayThangHopLe(int pNgay, int pThang, int pNam)
        {
            if (pNgay == 31 && (pThang == 4 || pThang == 6 || pThang == 9 || pThang == 11))
                return false;
            if (kiemTraNamNhuan(pNam))
            {
                if (pThang == 2 && (pNgay == 30 || pNgay == 31))
                    return false;
            }
            else
            {
                if (pThang == 2 && (pNgay == 29 || pNgay == 30 || pNgay == 31))
                    return false;
            }
            return true;
        }

        public List<int> loadThang2(int pNam)
        {
            List<int> ds = new List<int>();
            int soNgay = 0;
            if (kiemTraNamNhuan(pNam))
            {
                soNgay = 29;
            }
            else
            {
                soNgay = 28;
            }
            ds = loadDuLieu(1, soNgay);
            return ds;
        }

        public IQueryable thongKeDoanhThuThangVaNam(int pThang, int pNam)
        {
            IQueryable ts = from k in qlLK.HOADONs
                            where k.NGAYLAPHD.Value.Month == pThang && k.NGAYLAPHD.Value.Year == pNam
                            select new { k.MAHD, k.MANV, k.MAKH, k.NGAYLAPHD, k.TONGTIENHD, k.THANHTOAN };
            return ts;
        }
        public IQueryable thongKeDoanhThuNam(int pNam)
        {
            IQueryable ts = from k in qlLK.HOADONs
                            where k.NGAYLAPHD.Value.Year == pNam
                            select new { k.MAHD, k.MANV, k.MAKH, k.NGAYLAPHD, k.TONGTIENHD, k.THANHTOAN };
            return ts;
        }
        public IQueryable thongKeDoanhThu(int pNgay, int pThang, int pNam)
        {
            IQueryable ts = from k in qlLK.HOADONs
                            where k.NGAYLAPHD.Value.Month == pThang && k.NGAYLAPHD.Value.Year == pNam && k.NGAYLAPHD.Value.Day == pNgay
                            select new { k.MAHD, k.MANV, k.MAKH, k.NGAYLAPHD, k.TONGTIENHD, k.THANHTOAN };
            return ts;
        }
        public double? tongTien(int pThang, int pNam)
        {
            List<ThongKeHD> ts = (from k in qlLK.HOADONs
                                where k.NGAYLAPHD.Value.Month == pThang && k.NGAYLAPHD.Value.Year == pNam
                                select new ThongKeHD
                                {
                                    MaHD = k.MAHD,
                                    MaNV = k.MANV,
                                    MaKH = k.MAKH,
                                    NgayLap = k.NGAYLAPHD,
                                    TongTien = k.TONGTIENHD,
                                    ThanhToan = k.THANHTOAN,
                                }).ToList();
            var tong = ts.Sum(t => t.ThanhToan);
            return tong;
        }

        public double? tongTien(int pNgay, int pThang, int pNam)
        {
            List<ThongKeHD> ts = (from k in qlLK.HOADONs
                                where k.NGAYLAPHD.Value.Month == pThang && k.NGAYLAPHD.Value.Year == pNam && k.NGAYLAPHD.Value.Day == pNgay
                                select new ThongKeHD
                                {
                                    MaHD = k.MAHD,
                                    MaNV = k.MANV,
                                    MaKH = k.MAKH,
                                    NgayLap = k.NGAYLAPHD,
                                    TongTien = k.TONGTIENHD,
                                    ThanhToan = k.THANHTOAN,
                                }).ToList();
            var tong = ts.Sum(t => t.ThanhToan);
            return tong;
        }
        public double? tongTien(int pNam)
        {
            List<ThongKeHD> ts = (from k in qlLK.HOADONs
                                where k.NGAYLAPHD.Value.Year == pNam
                                select new ThongKeHD
                                {
                                    MaHD = k.MAHD,
                                    MaNV = k.MANV,
                                    MaKH = k.MAKH,                                  
                                    NgayLap = k.NGAYLAPHD,
                                    TongTien = k.TONGTIENHD,
                                    ThanhToan = k.THANHTOAN,
                                }).ToList();
            var tong = ts.Sum(t => t.ThanhToan);
            return tong;
        }

        public class ThongKeHD
        {
            int? maHD;
            string maKH, maNV;
            DateTime? ngayLap;
            double? tongTien, thanhToan;

            public int? MaHD { get => maHD; set => maHD = value; }
            public string MaKH { get => maKH; set => maKH = value; }
            public string MaNV { get => maNV; set => maNV = value; }
            public DateTime? NgayLap { get => ngayLap; set => ngayLap = value; }
            public double? TongTien { get => tongTien; set => tongTien = value; }
            public double? ThanhToan { get => thanhToan; set => thanhToan = value; }
        }

    }
}
