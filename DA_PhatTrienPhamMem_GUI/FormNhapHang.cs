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
    public partial class FormNhapHang : Form
    {

        BLLDAL_SanPham sp = new BLLDAL_SanPham();
        BLL_DALNhapHang_CTPN pn = new BLL_DALNhapHang_CTPN();
        BLLDAL_ThanhToan bh = new BLLDAL_ThanhToan();
        public FormNhapHang()
        {
            InitializeComponent();
        }

        private void FormNhapHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.formMain.Show();
        }

        private void FormNhapHang_Load(object sender, EventArgs e)
        {
            dtgv_PhieuNhap.DataSource = pn.LoadPhieuNhap();
            dtgv_CTPN.DataSource = pn.LoadCTPN();

            cboNhanVien.DataSource = pn.LoadTenNV();
            cboNhanVien.ValueMember = "MANV";
            cboNhanVien.DisplayMember = "TENNV";

            cboNhaCungCap.DataSource = sp.LoadTenNCC();
            cboNhaCungCap.ValueMember = "MANCC";
            cboNhaCungCap.DisplayMember = "TENNCC";

            cboTinhTrang.DataSource = pn.LoadTinhTrang();

            cboMaPhieuNhap.DataSource = pn.loadMaPN();
            cboMaPhieuNhap.ValueMember = "MAPN";

            cboSanPham.DataSource = sp.LoadSanPham();
            cboSanPham.ValueMember = "MASP";
            cboSanPham.DisplayMember = "TENSP";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaPhieuNhap.Text))
            {
                MessageBox.Show("Mã phiếu nhập không được để trống!!", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(!pn.ktKhoasChinh(txtMaPhieuNhap.Text))
            {
                MessageBox.Show("Mã phiếu nhập này đã tồn tại!!!Vui lòng nhập mã khác!!", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string maNV = bh.traVeMaNhanVien(Program.tenDangNhap);
            DateTime ngaylap = dpk_NgayLap.Value;
            if(pn.themPhieuNhap(txtMaPhieuNhap.Text, maNV, cboNhaCungCap.SelectedValue.ToString(),ngaylap))
            {
                dtgv_PhieuNhap.DataSource = pn.LoadPhieuNhap();
                cboMaPhieuNhap.DataSource = pn.loadMaPN();
                cboMaPhieuNhap.ValueMember = "MAPN";
                MessageBox.Show("Thêm phiếu nhập thành công", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Thêm phiếu nhập thất bại", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaPhieuNhap.Text.Trim()))
            {
                MessageBox.Show("Mã phiếu nhập không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (pn.khoiPhucSoLuongHuy(txtMaPhieuNhap.Text.Trim()))
            {

                cboMaPhieuNhap.DataSource = pn.LoadPhieuNhap();
                MessageBox.Show("Hủy phiếu nhập thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            else
            {
                MessageBox.Show("Hủy phiếu nhập thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnThemCTPN_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtSoLuong.Text.Trim()) || String.IsNullOrEmpty(txtDonGiaNhap.Text.Trim()))
            {
                MessageBox.Show("Số lượng, đơn giá nhập, không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!pn.ktKhoaChinh(cboMaPhieuNhap.SelectedValue.ToString(), cboSanPham.SelectedValue.ToString()))
            {
                MessageBox.Show("Mã chi tiết phiếu nhập (Mã phiếu nhập và mã sản phẩm) này đã tồn tại! Xin vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!pn.ktTinhTrang(cboMaPhieuNhap.SelectedValue.ToString()))
            {
                MessageBox.Show("Phiếu nhập này đã bị hủy nên không thể thêm chi tiết phiếu nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int soLuong = int.Parse(txtSoLuong.Text.Trim());
            double donGia = double.Parse(txtDonGiaNhap.Text.Trim());
            double thanhTien = double.Parse(txtThanhTien.Text.Trim());
            if (soLuong < 0)
            {
                MessageBox.Show("Số lượng sản phẩm phải lớn hơn 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pn.themChiTietPhieuNhap(cboMaPhieuNhap.SelectedValue.ToString(), cboSanPham.SelectedValue.ToString(), soLuong, donGia, thanhTien))
            {
                dtgv_PhieuNhap.DataSource = pn.LoadPhieuNhap();
                dtgv_CTPN.DataSource = pn.LoadCTPN();
                sp.capNhatTinhTrang();
                MessageBox.Show("Thêm chi tiết phiếu nhập mới thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm chi tiết phiếu nhập mới thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSoLuong.Text.Trim()) && !String.IsNullOrEmpty(txtDonGiaNhap.Text.Trim()))
            {
                int sl = int.Parse(txtSoLuong.Text.Trim());
                double dgb = double.Parse(txtDonGiaNhap.Text.Trim());
                txtThanhTien.Text = sl * dgb + "";
            }
        }

        private void txtDonGiaNhap_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSoLuong.Text.Trim()) && !String.IsNullOrEmpty(txtDonGiaNhap.Text.Trim()))
            {
                int sl = int.Parse(txtSoLuong.Text.Trim());
                double dgb = double.Parse(txtDonGiaNhap.Text.Trim());
                txtThanhTien.Text = sl * dgb + "";
            }
        }

        private void cboSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            double? donGiaBan = pn.traVeDonGiaNhap(cboSanPham.SelectedValue.ToString());
            txtDonGiaNhap.Text = donGiaBan + "";
        }

        private void dtgv_PhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgv_PhieuNhap.CurrentRow != null)
            {
                txtMaPhieuNhap.Text = dtgv_PhieuNhap.CurrentRow.Cells[0].Value.ToString();
                dpk_NgayLap.Text = dtgv_PhieuNhap.CurrentRow.Cells[3].Value.ToString();
                cboNhanVien.Text = pn.traVeTenNV(dtgv_PhieuNhap.CurrentRow.Cells[1].Value.ToString());
                cboNhaCungCap.Text = pn.traVeTenNCC(dtgv_PhieuNhap.CurrentRow.Cells[2].Value.ToString());
                try
                {
                    txtTongTien.Text = dtgv_PhieuNhap.CurrentRow.Cells[4].Value.ToString();
                }
                catch
                {
                    txtTongTien.Text = "";
                }
                dtgv_CTPN.DataSource = pn.loadChiTietHoaDonTheoMa(dtgv_PhieuNhap.CurrentRow.Cells[0].Value.ToString());
                cboTinhTrang.Text = dtgv_PhieuNhap.CurrentRow.Cells[5].Value.ToString();
            }
        }

        private void dtgv_CTPN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgv_CTPN.CurrentRow != null)
            {
                cboMaPhieuNhap.Text = dtgv_CTPN.CurrentRow.Cells[0].Value.ToString();
                cboSanPham.Text = pn.traVeTenSanPham(dtgv_CTPN.CurrentRow.Cells[1].Value.ToString());
                try
                {
                    txtDonGiaNhap.Text = dtgv_CTPN.CurrentRow.Cells[3].Value.ToString();
                    txtThanhTien.Text = dtgv_CTPN.CurrentRow.Cells[4].Value.ToString();
                    txtSoLuong.Text = dtgv_CTPN.CurrentRow.Cells[2].Value.ToString();
                }
                catch
                {
                    txtTongTien.Text = "";
                    txtDonGiaNhap.Text = "";
                    txtThanhTien.Text = "";
                }
            }
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            Program.formMain.Show();
        }

    }
}
