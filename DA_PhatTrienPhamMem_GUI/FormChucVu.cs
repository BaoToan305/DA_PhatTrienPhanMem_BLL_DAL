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
    public partial class FormChucVu : Form
    {
        BLLDAL_ChucVu cv = new BLLDAL_ChucVu();
        public FormChucVu()
        {
            InitializeComponent();
        }

        private void FormChucVu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.formMain.Show();
        }

        private void FormChucVu_Load(object sender, EventArgs e)
        {
            dtgvChucVu.DataSource = cv.LoadChucVu();

        }

        private void dtgvChucVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dtgvChucVu.CurrentRow!=null)
            {
                txtMaCV.Text = dtgvChucVu.CurrentRow.Cells[0].Value.ToString();
                txtTenCV.Text = dtgvChucVu.CurrentRow.Cells[1].Value.ToString();
            }
        }

        private void bntThem_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtMaCV.Text)||String.IsNullOrEmpty(txtTenCV.Text))
            {
                MessageBox.Show("Thông tin không được để trống", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(!cv.ktKhoaChinh(txtMaCV.Text))
            {
                MessageBox.Show("Mã chức vụ này đã có vui lòng nhập mã khác","LỖI!!!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if(cv.themCV(txtMaCV.Text,txtTenCV.Text))
            {
                dtgvChucVu.DataSource = cv.LoadChucVu();
                MessageBox.Show("Thêm thông tin chức vụ thành công", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Thêm thông tin chức vụ thất bại", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaCV.Text) || String.IsNullOrEmpty(txtTenCV.Text))
            {
                MessageBox.Show("Thông tin không được để trống", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 
            if (cv.xoaCV(txtMaCV.Text))
            {
                dtgvChucVu.DataSource = cv.LoadChucVu();
                MessageBox.Show("Xóa thông tin chức vụ thành công", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Xóa thông tin chức vụ thất bại", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaCV.ResetText();
            txtTenCV.ResetText();
            txtMaCV.Focus();
        }

        private void bntSua_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaCV.Text) || String.IsNullOrEmpty(txtTenCV.Text))
            {
                MessageBox.Show("Thông tin không được để trống", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cv.suaCV(txtMaCV.Text, txtTenCV.Text))
            {
                dtgvChucVu.DataSource = cv.LoadChucVu();
                MessageBox.Show("Sửa chức vụ thành công", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Xóa thông tin chức vụ thất bại", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
