using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DA_PhatTrienPhanMem_BLL_DAL;

namespace DA_PhatTrienPhamMem_GUI
{
    public partial class FormMain : Form
    {
        BLLDAL_QL_NguoiDung phanQuyen = new BLLDAL_QL_NguoiDung();
        BLLDAL_ThanhToan tt = new BLLDAL_ThanhToan();

        private string _TenDangNhap;

        public string TenDangNhap
        {
            get { return _TenDangNhap; }
            set { _TenDangNhap = value; }
        }
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            List<string> nhomND = phanQuyen.GetMaNhomNguoiDung(_TenDangNhap);
            foreach (string item in nhomND)
            {
                DataTable dsQuyen = phanQuyen.GetMaManHinh(item);
                foreach (DataRow mh in dsQuyen.Rows)
                {
                    FindMenuPhanQuyen(this.menuStrip1.Items, mh[1].ToString(), Convert.ToBoolean(mh[2].ToString()));
                }

            }
            Program.formMain.Text = "Xin chào " + tt.traVeNhanVienDiemDanh(Program.tenDangNhap) ;

        }
        private void FindMenuPhanQuyen(ToolStripItemCollection mnuItems, string PScreenName, bool pEnable)
        {

            foreach (ToolStripItem menu in mnuItems)
            {
                if (menu is ToolStripMenuItem && ((ToolStripMenuItem)(menu)).DropDownItems.Count > 0)
                {
                    FindMenuPhanQuyen(((ToolStripMenuItem)(menu)).DropDownItems, PScreenName, pEnable);
                    menu.Enabled = CheckAllMenuChildVisible(((ToolStripMenuItem)(menu)).DropDownItems);
                    menu.Visible = menu.Enabled;
                }
                else if (string.Equals(PScreenName, menu.Tag))
                {
                    menu.Enabled = pEnable;
                    menu.Visible = pEnable;
                }
            }
        }
        private bool CheckAllMenuChildVisible(ToolStripItemCollection mnuItems)
        {
            foreach (ToolStripItem menuItem in mnuItems)
            {
                if (menuItem is ToolStripMenuItem && menuItem.Enabled)
                {
                    return true;

                }
                else if (menuItem is ToolStripSeparator)
                {
                    continue;
                }
            }
            return false;

        }

        private void phânQuyềnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormPQuyen fm = new FormPQuyen();
            this.Visible = false;
            fm.Show();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void phânQuyềnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDoiMatKhau fm = new FormDoiMatKhau();
            this.Visible = false;
            fm.Show();
        }

        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDangNhap fm = new FormDangNhap();
            this.Visible = false;
            fm.Show();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDangNhap fm = new FormDangNhap();
            this.Visible = false;
            fm.Show();
        }

        private void sảnPhâmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSanPham fm = new FormSanPham();
            this.Visible = false;
            fm.Show();
        }

        private void loạiLinhKiệnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLoaiLinhKien fm = new FormLoaiLinhKien();
            this.Visible = false;
            fm.Show();
        }

        private void nhàCungCấpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNhaCC fm = new FormNhaCC();
            this.Visible = false;
            fm.Show();
        }

        private void chứcVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormChucVu fm = new FormChucVu();
            this.Visible = false;
            fm.Show();
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormKhachHang fm = new FormKhachHang();
            this.Visible = false;
            fm.Show();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNhanVien fm = new FormNhanVien();
            this.Visible = false;
            fm.Show();
        }

        private void sảnPhẩmBàyBánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBanHang fm = new FormBanHang();
            this.Visible = false;
            fm.Show();

        }

        private void thanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormThanhToan fm = new FormThanhToan();
            this.Visible = false;
            fm.Show();
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHoaDon fm = new FormHoaDon();
            this.Visible = false;
            fm.Show();
        }

        private void nhậpHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNhapHang fm = new FormNhapHang();
            fm.Visible = false;
            fm.Show();
        }

        private void doanhThuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormThongKe fm = new FormThongKe();
            fm.Visible = false;
            fm.Show();
        }

        private void thôngTinToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormThongTin fm = new FormThongTin();
            fm.Visible = false;
            fm.Show();
        }
    }
}
