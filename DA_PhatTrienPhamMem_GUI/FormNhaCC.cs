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
using txtChiNhapSo;
using DA_PhatTrienPhanMem_BLL_DAL;

namespace DA_PhatTrienPhamMem_GUI
{
    public partial class FormNhaCC : Form
    {
        public FormNhaCC()
        {
            InitializeComponent();
            
        }
        BLLDALNhaCC ncc = new BLLDALNhaCC();
        private void FormNhaCC_Load(object sender, EventArgs e)
        {
            dtgvNCC.DataSource = ncc.loadNCC();
        }

        private void FormNhaCC_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.formMain.Show();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaNCC.ResetText();
            txtTenNCC.ResetText();
            txtSDT.ResetText();
            txtDiaChi.ResetText();
            txtMaNCC.Focus();
        }

        private void dtgvNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dtgvNCC.CurrentRow!= null)
            {
                txtMaNCC.Text = dtgvNCC.CurrentRow.Cells[0].Value.ToString();
                txtTenNCC.Text=dtgvNCC.CurrentRow.Cells[1].Value.ToString();
                txtSDT.Text=dtgvNCC.CurrentRow.Cells[2].Value.ToString();
                txtDiaChi.Text = dtgvNCC.CurrentRow.Cells[3].Value.ToString();
            }
        }

        private void bntThem_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtMaNCC.Text)||String.IsNullOrEmpty(txtTenNCC.Text)||String.IsNullOrEmpty(txtDiaChi.Text)||String.IsNullOrEmpty(txtSDT.Text))
            {
                MessageBox.Show("Thông tin nhà cung cấp không được thiếu hoặc để trống","LỖI",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if(!ncc.ktKhoasChinh(txtMaNCC.Text))
            {
                MessageBox.Show("Mã nhà cung cấp này đã có,Vui lòng nhập mã nhà cung cấp khác", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(ncc.themNCC(txtMaNCC.Text,txtTenNCC.Text,txtSDT.Text,txtDiaChi.Text))
            {
                dtgvNCC.DataSource = ncc.loadNCC();
                MessageBox.Show("Thêm thông tin nhà cung cấp mới thành công", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Thêm thông tin nhà cung cấp mới thất bại", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void bntSua_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaNCC.Text) || String.IsNullOrEmpty(txtTenNCC.Text) || String.IsNullOrEmpty(txtDiaChi.Text) || String.IsNullOrEmpty(txtSDT.Text))
            {
                MessageBox.Show("Thông tin nhà cung cấp không được thiếu hoặc để trống", "LỖI", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ncc.suaNCC(txtMaNCC.Text, txtTenNCC.Text, txtSDT.Text, txtDiaChi.Text))
            {
                dtgvNCC.DataSource = ncc.loadNCC();
                MessageBox.Show("Sửa thông tin nhà cung cấp mới thành công", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Sửa thông tin nhà cung cấp mới thất bại", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaNCC.Text) || String.IsNullOrEmpty(txtTenNCC.Text) || String.IsNullOrEmpty(txtDiaChi.Text) || String.IsNullOrEmpty(txtSDT.Text))
            {
                MessageBox.Show("Thông tin nhà cung cấp không được thiếu hoặc để trống", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ncc.xoaNCC(txtMaNCC.Text))
            {
                dtgvNCC.DataSource = ncc.loadNCC();
                MessageBox.Show("Xóa thông tin nhà cung cấp mới thành công", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Xóa thông tin nhà cung cấp mới thất bại", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }



    }
}
