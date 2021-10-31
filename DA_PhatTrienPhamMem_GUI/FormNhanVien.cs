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
    public partial class FormNhanVien : Form
    {
        BLLDAL_NhanVien nv = new BLLDAL_NhanVien();
        public FormNhanVien()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.formMain.Show();
        }

        private void FormNhanVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.formMain.Show();
        }

        private void FormNhanVien_Load(object sender, EventArgs e)
        {
            dtgv_NhanVien.DataSource = nv.LoadNV();

            cbbChucVu.DataSource = nv.loadMaCV();
            cbbChucVu.ValueMember = "MACV";
            cbbChucVu.DisplayMember = "TENCHUCVU";
        }
        public void checkGioiTinh(GroupBox gb, string pGioiTinh)
        {
            for (int i = 0; i < gb.Controls.Count; i++)
            {
                RadioButton rb = (RadioButton)gb.Controls[i];
                if (rb.Text == pGioiTinh)
                {
                    rb.Checked = true;
                }
            }
        }

        public bool ktCheckGioiTinh(GroupBox gb)
        {
            for (int i = 0; i < gb.Controls.Count; i++)
            {
                RadioButton rb = (RadioButton)gb.Controls[i];
                if (rb.Checked)
                {
                    return true;
                }
            }
            return false;
        }

        public string traVeGioiTinh(GroupBox gb)
        {
            string kq = "";
            for (int i = 0; i < gb.Controls.Count; i++)
            {
                RadioButton rb = (RadioButton)gb.Controls[i];
                if (rb.Checked)
                {
                    kq = rb.Text;
                    return kq;
                }
            }
            return kq;
        }
        private void dtgv_NhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dtgv_NhanVien.CurrentRow!=null)
            {
                txtMaNV.Text = dtgv_NhanVien.CurrentRow.Cells[0].Value.ToString();
                txtTenNV.Text = dtgv_NhanVien.CurrentRow.Cells[1].Value.ToString();
                checkGioiTinh(gbGioiTinh, dtgv_NhanVien.CurrentRow.Cells[2].Value.ToString());
                dpk_NgaySinh.Text = dtgv_NhanVien.CurrentRow.Cells[3].Value.ToString();
                txtDienThoai.Text = dtgv_NhanVien.CurrentRow.Cells[4].Value.ToString();
                txtDiaChi.Text = dtgv_NhanVien.CurrentRow.Cells[5].Value.ToString();
                cbbChucVu.Text =nv.traVeTenChucVu( dtgv_NhanVien.CurrentRow.Cells[6].Value.ToString());
                dpk_NgayVL.Text = dtgv_NhanVien.CurrentRow.Cells[7].Value.ToString();
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtMaNV.Text)||String.IsNullOrEmpty(txtTenNV.Text)||String.IsNullOrEmpty(txtDienThoai.Text)||String.IsNullOrEmpty(txtDiaChi.Text))
            {
                MessageBox.Show("Thông tin cần thêm còn thiếu vui lòng nhập thêm!","LỖI!!!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if(txtDienThoai.Text.Length>11)
            {
                MessageBox.Show("Số điện thoại không được quá 11 số","LỖI!!!",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if(txtMaNV.Text.Length>10)
            {
                MessageBox.Show("Mã nhân viên không được quá 10 ký tự", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtTenNV.Text.Length > 50)
            {
                MessageBox.Show("Tên nhân viên quá dài", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(txtDiaChi.Text.Length>100)
            {
                MessageBox.Show("Thông tin địa chỉ quá 100 ký tự", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ktCheckGioiTinh(gbGioiTinh))
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(!nv.ktKhoaschinh(txtMaNV.Text))
            {
                MessageBox.Show("Mã nhân viên này đã tồn tại! Xin vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaNV.Focus();
                return;
            }
            DateTime ns = DateTime.Parse(dpk_NgaySinh.Text);
            DateTime nvl = DateTime.Parse(dpk_NgayVL.Text);
            if (nv.themNV(txtMaNV.Text,txtTenNV.Text,ns,traVeGioiTinh(gbGioiTinh),txtDienThoai.Text,cbbChucVu.SelectedValue.ToString(),txtDiaChi.Text,nvl))
            {
                dtgv_NhanVien.DataSource = nv.LoadNV();
                MessageBox.Show("Thêm nhân viên mới thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Thêm nhân viên mới thất bại!", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaNV.Text) || String.IsNullOrEmpty(txtTenNV.Text) || String.IsNullOrEmpty(txtDienThoai.Text) || String.IsNullOrEmpty(txtDiaChi.Text))
            {
                MessageBox.Show("Thông tin cần thêm còn thiếu vui lòng nhập thêm!", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (nv.xoaNV(txtMaNV.Text))
            {
                dtgv_NhanVien.DataSource = nv.LoadNV();
                MessageBox.Show("Xóa nhân viên  thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Xoá nhân viên thất bại!", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaNV.Text) || String.IsNullOrEmpty(txtTenNV.Text) || String.IsNullOrEmpty(txtDienThoai.Text) || String.IsNullOrEmpty(txtDiaChi.Text))
            {
                MessageBox.Show("Thông tin cần thêm còn thiếu vui lòng nhập thêm!", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtDienThoai.Text.Length > 11)
            {
                MessageBox.Show("Số điện thoại không được quá 11 số", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtMaNV.Text.Length > 10)
            {
                MessageBox.Show("Mã nhân viên không được quá 10 ký tự", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtTenNV.Text.Length > 50)
            {
                MessageBox.Show("Tên nhân viên quá dài", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtDiaChi.Text.Length > 100)
            {
                MessageBox.Show("Thông tin địa chỉ quá 100 ký tự", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ktCheckGioiTinh(gbGioiTinh))
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DateTime ns = DateTime.Parse(dpk_NgaySinh.Text);
            DateTime nvl = DateTime.Parse(dpk_NgayVL.Text);
            if (nv.suaNV(txtMaNV.Text, txtTenNV.Text, ns, traVeGioiTinh(gbGioiTinh), txtDienThoai.Text, cbbChucVu.SelectedValue.ToString(), txtDiaChi.Text, nvl))
            {
                dtgv_NhanVien.DataSource = nv.LoadNV();
                MessageBox.Show("Sửa thông tin nhân viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Sửa thông tin nhân viên thất bại!", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaNV.ResetText();
            txtTenNV.ResetText();
            txtDienThoai.ResetText();
            txtDiaChi.ResetText();
            txtMaNV.Focus();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            dtgv_NhanVien.DataSource = nv.timKiemThongTinNV(txtTimKiem.Text.Trim());
        }
    }
}
