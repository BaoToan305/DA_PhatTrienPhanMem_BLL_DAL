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
using System.Text.RegularExpressions;

namespace DA_PhatTrienPhamMem_GUI
{
    public partial class FormKhachHang : Form
    {
        BLLDAL_KhachHang kh = new BLLDAL_KhachHang();
        public FormKhachHang()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.formMain.Show();
        }

        private void FormKhachHang_Load(object sender, EventArgs e)
        {
            dtgvKH.DataSource = kh.LoadKH();
        }

        private void FormKhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.formMain.Show();
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
        private void dtgvKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dtgvKH.CurrentRow!=null)
            {
                txtMaKH.Text = dtgvKH.CurrentRow.Cells[0].Value.ToString();
                txtTenKH.Text = dtgvKH.CurrentRow.Cells[1].Value.ToString();
                dpk_NgaySinh.Text = dtgvKH.CurrentRow.Cells[2].Value.ToString(); 
                checkGioiTinh(gbGioiTinh,dtgvKH.CurrentRow.Cells[3].Value.ToString());
                txtSDT.Text = dtgvKH.CurrentRow.Cells[4].Value.ToString();
                txtEmail.Text = dtgvKH.CurrentRow.Cells[5].Value.ToString();
                txtDiaChi.Text = dtgvKH.CurrentRow.Cells[6].Value.ToString(); ;

            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaKH.ResetText();
            txtTenKH.ResetText();
            txtEmail.ResetText();
            txtSDT.ResetText();
            txtDiaChi.ResetText();
            txtMaKH.Focus();
        }

        public static bool isEmail(string inputEmail)
        {
            inputEmail = inputEmail ?? string.Empty;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtMaKH.Text)||String.IsNullOrEmpty(txtTenKH.Text)||String.IsNullOrEmpty(txtSDT.Text)||String.IsNullOrEmpty(txtEmail.Text)||String.IsNullOrEmpty(txtDiaChi.Text))
            {
                MessageBox.Show("Thông tin cần thêm còn thiếu vui lòng nhập thêm!", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtSDT.Text.Length > 11)
            {
                MessageBox.Show("Số điện thoại không được quá 11 số", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtTenKH.Text.Length > 100)
            {
                MessageBox.Show("Tên khách hàng quá dài", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtDiaChi.Text.Length > 100)
            {
                MessageBox.Show("Thông tin địa chỉ quá 100 ký tự", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(!isEmail(txtEmail.Text))
            {
                MessageBox.Show("Đinh dạng email không phù hơp!! Vui lòng nhập lại!", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ktCheckGioiTinh(gbGioiTinh))
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(!kh.ktKhoaschinh(txtMaKH.Text))
            {
                MessageBox.Show("Mã khách hàng này đã tồn tại! Xin vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaKH.Focus();
                return;
            }
            DateTime ns = DateTime.Parse(dpk_NgaySinh.Text);
            if(kh.themKH(txtMaKH.Text,txtTenKH.Text,ns,traVeGioiTinh(gbGioiTinh),txtSDT.Text,txtEmail.Text,txtDiaChi.Text))
            {
                dtgvKH.DataSource = kh.LoadKH();
                MessageBox.Show("Thêm khách hàng mới thành công", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Thêm khách hàng mới thất bại", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaKH.Text) || String.IsNullOrEmpty(txtTenKH.Text) || String.IsNullOrEmpty(txtSDT.Text) || String.IsNullOrEmpty(txtEmail.Text) || String.IsNullOrEmpty(txtDiaChi.Text))
            {
                MessageBox.Show("Thông tin cần thêm còn thiếu vui lòng nhập thêm!", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (kh.xoaKH(txtMaKH.Text))
            {
                dtgvKH.DataSource = kh.LoadKH();
                MessageBox.Show("Xóa khách hàng thành công", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Xóa khách hàng thất bại", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaKH.Text) || String.IsNullOrEmpty(txtTenKH.Text) || String.IsNullOrEmpty(txtSDT.Text) || String.IsNullOrEmpty(txtEmail.Text) || String.IsNullOrEmpty(txtDiaChi.Text))
            {
                MessageBox.Show("Thông tin cần thêm còn thiếu vui lòng nhập thêm!", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtSDT.Text.Length > 11)
            {
                MessageBox.Show("Số điện thoại không được quá 11 số", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtTenKH.Text.Length > 100)
            {
                MessageBox.Show("Tên khách hàng quá dài", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtDiaChi.Text.Length > 100)
            {
                MessageBox.Show("Thông tin địa chỉ quá 100 ký tự", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!isEmail(txtEmail.Text))
            {
                MessageBox.Show("Đinh dạng email không phù hơp!! Vui lòng nhập lại!", "LỖI!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!ktCheckGioiTinh(gbGioiTinh))
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DateTime ns = DateTime.Parse(dpk_NgaySinh.Text);
            if (kh.suaKH(txtMaKH.Text, txtTenKH.Text, ns, traVeGioiTinh(gbGioiTinh), txtSDT.Text, txtEmail.Text, txtDiaChi.Text))
            {
                dtgvKH.DataSource = kh.LoadKH();
                MessageBox.Show("Sửa thông tin khách hàng thành công", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Sửa thông tin khách hàng thất bại", "Thất Bại", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            dtgvKH.DataSource = kh.timKiemThongTinKH(txtTimKiem.Text.Trim());
        }
    }
}
