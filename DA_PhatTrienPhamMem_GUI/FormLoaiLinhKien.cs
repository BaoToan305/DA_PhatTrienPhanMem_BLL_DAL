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
    public partial class FormLoaiLinhKien : Form
    {
        public FormLoaiLinhKien()
        {
            InitializeComponent();
        }
        BLLDALLoaiLK lk = new BLLDALLoaiLK();
        private void FormLoaiLinhKien_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            Program.formMain.Show();
        }

        private void FormLoaiLinhKien_Load(object sender, EventArgs e)
        {
            dtgvLoaiLK.DataSource = lk.LoadLinhKien();

        }

        private void dtgvLoaiLK_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dtgvLoaiLK.CurrentRow!=null)
            {
                txtMaLoai.Text = dtgvLoaiLK.CurrentRow.Cells[0].Value.ToString();
                txtTenLoai.Text = dtgvLoaiLK.CurrentRow.Cells[1].Value.ToString();
            }
        }

        private void bntThem_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtMaLoai.Text)||String.IsNullOrEmpty(txtTenLoai.Text))
            {
                MessageBox.Show("Thông tin không được để trống","Lỗi!!!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if(!lk.ktKhoasChinh(txtMaLoai.Text))
            {
                MessageBox.Show("Mã loại linh kiện này đã có, vui lòng nhập mã khác","Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if(lk.themLoaiLK(txtMaLoai.Text,txtTenLoai.Text))
            {
                dtgvLoaiLK.DataSource = lk.LoadLinhKien();
                MessageBox.Show("Thêm loại linh kiện mới thành công","Thành Công",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Thêm loại linh kiện mới thất bại", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaLoai.Text) || String.IsNullOrEmpty(txtTenLoai.Text))
            {
                MessageBox.Show("Thông tin không được để trống", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (lk.xoaLoaiLK(txtMaLoai.Text))
            {
                dtgvLoaiLK.DataSource = lk.LoadLinhKien();
                txtMaLoai.ResetText();
                txtTenLoai.ResetText();
                txtMaLoai.Focus();
                MessageBox.Show("Xóa loại linh kiện thành công", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Xóa loại linh kiện mới thất bại", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void bntSua_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaLoai.Text) || String.IsNullOrEmpty(txtTenLoai.Text))
            {
                MessageBox.Show("Thông tin không được để trống", "Lỗi!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (lk.suaLoaiLK(txtMaLoai.Text, txtTenLoai.Text))
            {
                dtgvLoaiLK.DataSource = lk.LoadLinhKien();
                MessageBox.Show("Sửa loại linh kiện thành công", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Sửa loại linh kiện mới thất bại", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaLoai.ResetText();
            txtTenLoai.ResetText();
            txtMaLoai.Focus();
        }


    }
}
