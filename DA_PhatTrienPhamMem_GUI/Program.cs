using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DA_PhatTrienPhanMem_BLL_DAL;
using static DA_PhatTrienPhanMem_BLL_DAL.BLLDAL_BanHang;

namespace DA_PhatTrienPhamMem_GUI
{
    static class Program
    {

        public static FormMain formMain = null;
        public static FormDangNhap formDangNhap = null;
        public static FormCauHinh formCauHinh = null;
        public static FormPQuyen formPQuyen = null;
        public static FormDoiMatKhau formDoiMatKhau = null;
        public static FormSanPham formSanPham = null;
        public static FormLoaiLinhKien formLoaiLinhKien = null;
        public static FormNhaCC formNhaCC = null;
        public static FormChucVu formChucVu = null;
        public static FormNhanVien fomNhanVien = null;
        public static FormKhachHang formKhachHang = null;
        public static FormBanHang formBanHang = null;
        public static FormThanhToan formThanhToan = null;
        public static FormHoaDon formHoaDon = null;
        public static FormThongKe formThongKe = null;
        public static FormNhapHang formNhapHang = null;
        public static FormThongTin formThongTin = null;

        public static List<BanHang> ds = new List<BanHang>();

        public static string tenDangNhap = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            formDangNhap = new FormDangNhap();
            formCauHinh = new FormCauHinh();
            Application.Run(formDangNhap);
        }
    }
}
