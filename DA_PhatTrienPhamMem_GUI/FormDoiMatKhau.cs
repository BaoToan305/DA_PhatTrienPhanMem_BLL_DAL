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
    public partial class FormDoiMatKhau : Form
    {

        BLLDAL_DoiMatKhau daND = new BLLDAL_DoiMatKhau();
        public FormDoiMatKhau()
        {
            InitializeComponent();
        }

        private void FormDoiMatKhau_Load(object sender, EventArgs e)
        {
            txtTenDangNhap.Text = Program.tenDangNhap;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Program.formMain.Show();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMatKhauCu.Text) || String.IsNullOrEmpty(txtMatKhauMoi.Text) || String.IsNullOrEmpty(txtNhapLaiMatKhau.Text))
            {
                MessageBox.Show("Mật khẩu cũ, mật khẩu mới, nhập lại mật khẩu mới không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!daND.kiemTraMatKhauCu(txtTenDangNhap.Text.Trim(), txtMatKhauCu.Text.Trim()))
            {
                MessageBox.Show("Mật khẩu cũ không đúng! Xin vui lòng kiểm tra lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtMatKhauMoi.Text.Trim() != txtNhapLaiMatKhau.Text.Trim())
            {
                MessageBox.Show("Mật khẩu nhập lại không trùng khớp", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (daND.doiMatKhau(txtTenDangNhap.Text, txtMatKhauMoi.Text))
            {
                MessageBox.Show("Đổi mật khẩu thành công!", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Đổi mật khẩu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormDoiMatKhau_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.formMain.Show();
        }
    }
}
