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
    public partial class FormSanPham : Form
    {
        public FormSanPham()
        {
            InitializeComponent();
        }
        BLLDAL_SanPham sp = new BLLDAL_SanPham();
        private void FormSanPham_Load(object sender, EventArgs e)
        {
            dtgvSanPham.DataSource = sp.LoadSanPham();

            cboLoaiLinhKien.DataSource = sp.LoadTenLoai();
            cboLoaiLinhKien.ValueMember = "MALOAI";
            cboLoaiLinhKien.DisplayMember = "TENLOAI";

            cboNhaCungCap.DataSource = sp.LoadTenNCC();
            cboNhaCungCap.ValueMember = "MANCC";
            cboNhaCungCap.DisplayMember = "TENNCC";
        }

        private void FormSanPham_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.formMain.Show();
        }

        private void dtgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dtgvSanPham.CurrentRow!=null)
            {
                txtMaSP.Text = dtgvSanPham.CurrentRow.Cells[0].Value.ToString();
                txtTenSP.Text = dtgvSanPham.CurrentRow.Cells[1].Value.ToString();
                txtSoLuong.Text = dtgvSanPham.CurrentRow.Cells[3].Value.ToString();
                txtDGN.Text = dtgvSanPham.CurrentRow.Cells[4].Value.ToString();
                txtDGB.Text = dtgvSanPham.CurrentRow.Cells[5].Value.ToString();
                cboLoaiLinhKien.Text = sp.traVeTenLoai( dtgvSanPham.CurrentRow.Cells[2].Value.ToString());
                cboNhaCungCap.Text = sp.traVeTenNCC( dtgvSanPham.CurrentRow.Cells[6].Value.ToString());
                txtTinhTrang.Text = dtgvSanPham.CurrentRow.Cells[7].Value.ToString();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            dtgvSanPham.DataSource = sp.TimKiemSanPham(txtTimKiem.Text.Trim());
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.formMain.Show();
        }

        private void btnThemSP_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtMaSP.Text)||String.IsNullOrEmpty(txtTenSP.Text)||String.IsNullOrEmpty(txtDGN.Text)||String.IsNullOrEmpty(txtDGB.Text))
            {
                MessageBox.Show("Thông tin bị thiếu hoặc chưa nhập thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtMaSP.Text.Trim().Length > 10)
            {
                MessageBox.Show("Mã sản phẩm không được vượt quá 10 kí tự", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!sp.ktKhoaChinhSP(txtMaSP.Text.Trim()))
            {
                MessageBox.Show("Mã sản phẩm này đã tồn tại nên không thể thêm! Xin vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            double giaBan = double.Parse(txtDGB.Text);
            double giaNhap = double.Parse(txtDGN.Text);
            int sl = int.Parse(txtSoLuong.Text);
            if(sl<0)
            {
                MessageBox.Show("Số lượng sản phẩm không được nhỏ hơn 0", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(giaBan<giaNhap)
            {

                MessageBox.Show("Giá bán ra không được nhỏ hơn giá nhập vào","Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if(sp.themSP(txtMaSP.Text,txtTenSP.Text,sl,giaNhap,giaBan,cboNhaCungCap.SelectedValue.ToString(),cboLoaiLinhKien.SelectedValue.ToString()))
            {
                dtgvSanPham.DataSource = sp.LoadSanPham();
                MessageBox.Show("Thêm sản phẩm mới thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Thêm sản phẩm mới thất bại", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaSP.Text) || String.IsNullOrEmpty(txtTenSP.Text)|| String.IsNullOrEmpty(txtDGN.Text) || String.IsNullOrEmpty(txtDGB.Text))
            {
                MessageBox.Show("Thông tin không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(sp.xoaSP(txtMaSP.Text))
            {
                dtgvSanPham.DataSource = sp.LoadSanPham();
                MessageBox.Show("Xóa thông tin sản phẩm thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Xóa thông tin sản phẩm thất bại", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnSuaSP_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaSP.Text) || String.IsNullOrEmpty(txtTenSP.Text)|| String.IsNullOrEmpty(txtDGN.Text) || String.IsNullOrEmpty(txtDGB.Text))
            {
                MessageBox.Show("Thông tin bị thiếu hoặc chưa nhập thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtMaSP.Text.Trim().Length > 10)
            {
                MessageBox.Show("Mã sản phẩm không được vượt quá 10 kí tự", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            double giaBan = double.Parse(txtDGB.Text);
            double giaNhap = double.Parse(txtDGN.Text);
            int sl = int.Parse(txtSoLuong.Text);
            if (sl < 0)
            {
                MessageBox.Show("Số lượng sản phẩm không được nhỏ hơn 0", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (giaBan < giaNhap)
            {

                MessageBox.Show("Giá bán ra không được nhỏ hơn giá nhập vào", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (sp.suaSP(txtMaSP.Text, txtTenSP.Text, sl, giaNhap, giaBan, cboNhaCungCap.SelectedValue.ToString(), cboLoaiLinhKien.SelectedValue.ToString()))
            {
                dtgvSanPham.DataSource = sp.LoadSanPham();
                MessageBox.Show("Sửa sản phẩm thành công", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Sửa sản phẩm thất bại", "Thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }






    }
}
