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
using DevExpress.XtraReports.UI;
using DA_PhatTrienPhamMem_GUI.XtraReport;

namespace DA_PhatTrienPhamMem_GUI
{
    public partial class FormHoaDon : Form
    {
        BLLDALReportInHoaDon rp = new BLLDALReportInHoaDon();
        BLL_DALHoaDon_CTHD hd = new BLL_DALHoaDon_CTHD();
        public FormHoaDon()
        {
            InitializeComponent();
        }

        private void FormHoaDon_Load(object sender, EventArgs e)
        {
            dtgv_HoaDon.DataSource = hd.LoadHoaDon();

            dtgv_CTHD.DataSource = hd.LoadCTHD();
        }

        private void dtgv_HoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgv_HoaDon.CurrentRow != null)
            {
                txtMaHoaDon.Text = dtgv_HoaDon.CurrentRow.Cells[0].Value.ToString();
                dpk_NgayLap.Text= dtgv_HoaDon.CurrentRow.Cells[3].Value.ToString();
                txtTenNV.Text=hd.traVeTenNhanVien(dtgv_HoaDon.CurrentRow.Cells[1].Value.ToString());
                txtTenKH.Text= hd.traVeTenKhachHang(dtgv_HoaDon.CurrentRow.Cells[2].Value.ToString());
                txtTinhTrang.Text= dtgv_HoaDon.CurrentRow.Cells[6].Value.ToString();
                txtTongTien.Text= dtgv_HoaDon.CurrentRow.Cells[4].Value.ToString();
                txtThanhToan.Text= dtgv_HoaDon.CurrentRow.Cells[5].Value.ToString();
            }
        }

        private void dtgv_CTHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dtgv_CTHD.CurrentRow!=null)
            {
                txtMAHD.Text = dtgv_CTHD.CurrentRow.Cells[0].Value.ToString();
                txtTenSP.Text = hd.traVeTenSanPham(txtMAHD.Text = dtgv_CTHD.CurrentRow.Cells[1].Value.ToString());
                txtSoLuong.Text = txtMAHD.Text = dtgv_CTHD.CurrentRow.Cells[2].Value.ToString();
                txtDonGiaBan.Text = txtMAHD.Text = dtgv_CTHD.CurrentRow.Cells[3].Value.ToString();
                txtThanhTienCTHD.Text = txtMAHD.Text = dtgv_CTHD.CurrentRow.Cells[4].Value.ToString();
            }    
        }

        private void txtTenSP_TextChanged(object sender, EventArgs e)
        {
            txtSoLuongSP.Text = hd.traVeSoLuong(txtTenSP.Text) + "";
        }

        private void FormHoaDon_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.formMain.Show();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaHoaDon.Text.Trim()))
            {
                MessageBox.Show("Mã hóa đơn không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int maHD = int.Parse(txtMaHoaDon.Text.Trim());
            if (hd.khoiPhucSoLuongHuy(maHD))
            {
                dtgv_HoaDon.DataSource = hd.LoadHoaDon();
                MessageBox.Show("Hủy hóa đơn thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else
            {
                MessageBox.Show("Hủy hóa đơn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if(dtgv_HoaDon.CurrentRow!= null)
            {
                int maHD = int.Parse(dtgv_HoaDon.CurrentRow.Cells[0].Value.ToString());
                ReportInHoaDon rpt = new ReportInHoaDon();


                rpt.DataSource = rp.xuatHoaDon(maHD);
                rpt.ShowPreviewDialog();

            }
        }
    }
}
