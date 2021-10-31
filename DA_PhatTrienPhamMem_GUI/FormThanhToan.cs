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
using static DA_PhatTrienPhanMem_BLL_DAL.BLLDAL_BanHang;

namespace DA_PhatTrienPhamMem_GUI
{
    public partial class FormThanhToan : Form
    {
        BLLDAL_ThanhToan tt = new BLLDAL_ThanhToan();
        BLLDAL_BanHang bh = new BLLDAL_BanHang();

        string maKH;

        public string MaKH { get => maKH; set => maKH = value; }

        public FormThanhToan()
        {
            InitializeComponent();
        }


        private void FormThanhToan_Load(object sender, EventArgs e)
        {
            txtMaKH.Text = maKH ;
            lblNhanVien.Text += " " + tt.traVeNhanVienDiemDanh(Program.tenDangNhap);

            List<BanHang> dsBan = Program.ds.Where(t => t.MaKH == txtMaKH.Text).ToList();
            dtgvThanhToan.DataSource = dsBan;

            dtgvThanhToan.DataSource = null;
            dtgvThanhToan.Rows.Clear();
            dtgvThanhToan.Refresh();
            dtgvThanhToan.DataSource = Program.ds;

            txtTongTien.Text = Program.ds.Where(t => t.MaKH == txtMaKH.Text).Sum(t => t.Thanhtien) + "";

            txtThanhTien.Text = txtTongTien.Text;

        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            List<BanHang> ds = Program.ds.Where(t => t.MaKH == txtMaKH.Text).ToList();
            string maNV = tt.traVeMaNhanVien(Program.tenDangNhap);
            double tongtien = double.Parse(txtTongTien.Text.Trim());
            double thanhtien = double.Parse(txtThanhTien.Text.Trim());
            DateTime ngayLap = DateTime.Now;
            if(tt.xacNhanThanhToan(maNV,txtMaKH.Text,ngayLap,tongtien,thanhtien,ds))
            {
                Program.ds = tt.capNhatSauKhiThanhToan(Program.ds,maKH);
                int maHD = tt.traVeMaHD(MaKH);
                tt.thayDoiSoLuongKhiThem(maHD);
                MessageBox.Show("Thanh toán thành công! Hóa đơn đã được lưu lại!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Thanh toán thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void dtgvThanhToan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgvThanhToan.CurrentRow != null)
            {
                txtMaSP.Text = dtgvThanhToan.CurrentRow.Cells[0].Value.ToString();
                txtTenSP.Text = dtgvThanhToan.CurrentRow.Cells[5].Value.ToString();
            }
        }

        private void FormThanhToan_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.formMain.Show();
        }
    }
}
