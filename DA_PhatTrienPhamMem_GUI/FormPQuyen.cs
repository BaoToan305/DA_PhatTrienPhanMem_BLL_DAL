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
    public partial class FormPQuyen : Form
    {
        BLLDAL_PQuyen pq = new BLLDAL_PQuyen();
        public FormPQuyen()
        {
            InitializeComponent();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void LoaiDSNguoiDung()
        {
            dtgvQLNgDung.DataSource = pq.LoadQLNguoiDung();
        }
        private void FormPQuyen_Load(object sender, EventArgs e)
        {
            dtgvNguoiDung.DataSource = pq.LoadNguoiDung();
            dtgvNNDung.DataSource = pq.LoadNNDung();
            dtgvMaHinh.DataSource = pq.LoadManHinh();
            dtgvPhanQuyen.DataSource = pq.LoadPhanQuyen();

            LoaiDSNguoiDung();

            cbbTenDN.DataSource = pq.LoadTenDN();
            cbbTenDN.DisplayMember = "TenDN";
            cbbTenDN.ValueMember = "TenDN";

            cbbMaNhom.DataSource = pq.LoadMaNhom();
            cbbMaNhom.DisplayMember = "TENNHOM";
            cbbMaNhom.ValueMember = "MANHOM";

            cbbMaNV.DataSource = pq.LoadMaNV();
            cbbMaNV.ValueMember = "MANV";
            cbbMaNV.DisplayMember = "TENNV";

        }

        private void FormPQuyen_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.formMain.Show();
        }

        private void dtgvNguoiDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dtgvNguoiDung.CurrentRow != null)
            {
                txtTENDN.Text = dtgvNguoiDung.CurrentRow.Cells[0].Value.ToString();
                txtMATKHAU.Text = dtgvNguoiDung.CurrentRow.Cells[1].Value.ToString();
                cbbMaNV.Text = dtgvNguoiDung.CurrentRow.Cells[2].Value.ToString();
                checkQuyen(ckbQuyen, dtgvNguoiDung.CurrentRow.Cells[3].Value.ToString());          
            }
        }

        public void checkQuyen(CheckBox ck,string quyen)
        {
                  if(Convert.ToBoolean(quyen) == true)
                  {
                      ck.Checked = true;
                  }                    
                  else
                  {
                      ck.Checked = false;
                  }
               
        }

        private void dtgvQLNgDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgvQLNgDung != null)
            {
                try
                {
                    cbbTenDN.Text = dtgvQLNgDung.CurrentRow.Cells[0].Value.ToString();
                    cbbMaNhom.Text = pq.traVeTenNhom(dtgvQLNgDung.CurrentRow.Cells[1].Value.ToString());
                    txtGhiChu.Text = dtgvQLNgDung.CurrentRow.Cells[2].Value.ToString();
                }
                catch
                {
                    txtGhiChu.Text = "";
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!pq.ktKhoaChinhQLNguoiDung(cbbTenDN.SelectedValue.ToString(), cbbMaNhom.SelectedValue.ToString()))
            {
                MessageBox.Show("Người dùng này đã được phân vào nhóm người dùng nên không thể thêm! Xin vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtGhiChu.Text.Trim().Length > 200)
            {
                MessageBox.Show("Độ dài của ghi chú không được vượt quá 200 kí tự!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pq.ThemQLNguoiDung(cbbTenDN.SelectedValue.ToString(), cbbMaNhom.SelectedValue.ToString(), txtGhiChu.Text.Trim()))
            {
                LoaiDSNguoiDung();
                MessageBox.Show("Thêm người dùng mới vào nhóm người dùng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm người dùng mới vào nhóm người dùng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pq.ktKhoaChinhQLNguoiDung(cbbTenDN.SelectedValue.ToString(), cbbMaNhom.SelectedValue.ToString()))
            {
                MessageBox.Show("Người dùng này không tồn tại nên không thể xóa! Xin vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pq.xoaQLNguoiDung(cbbTenDN.SelectedValue.ToString(), cbbMaNhom.SelectedValue.ToString()))
            {
                LoaiDSNguoiDung();
                MessageBox.Show("Xóa người dùng ra khỏi nhóm người dùng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Xóa người dùng ra khỏi nhóm người dùng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void dtgvNNDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dtgvNNDung!=null)
            {
                try
                {
                    txtMaNhom.Text = dtgvNNDung.CurrentRow.Cells[0].Value.ToString();
                    txtTenNhom.Text = dtgvNNDung.CurrentRow.Cells[1].Value.ToString();
                    txttGhiChu.Text = dtgvNNDung.CurrentRow.Cells[2].Value.ToString();
                }
                catch
                {
                    txttGhiChu.Text = "";
                }

            }
        }
        public bool ktCheckBox(CheckBox ck)
        {            
            if(ck.Checked==true)
            {
                ck.Checked = true;
                return true;
            }
            else
            {
                ck.Checked = false;
                return false;
            }           
        }

        private void bntThem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtTENDN.Text) || String.IsNullOrEmpty(txtMATKHAU.Text))
            {
                MessageBox.Show("Thông tin không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if(!pq.ktKhoaChinhNguoiDung(txtTENDN.Text))
            {
                MessageBox.Show("Tên đăng nhập này đã có nên không thể thêm! Xin vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pq.themNguoiDung(txtTENDN.Text.Trim(), txtMATKHAU.Text.Trim(), cbbMaNV.SelectedValue.ToString(), ktCheckBox(ckbQuyen)))
            {
                dtgvNguoiDung.DataSource = pq.LoadNguoiDung();
                MessageBox.Show("Thêm thông tin người dùng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm thông tin người dùng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtTENDN.Text))
            {
                MessageBox.Show("Thông tin không được để trống","Lỗi",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if(pq.xoaNguoiDung(cbbMaNV.SelectedValue.ToString()))
                {
                    dtgvNguoiDung.DataSource = pq.LoadNguoiDung();
                    MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void bntSua_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtTENDN.Text)||String.IsNullOrEmpty(txtMATKHAU.Text))
            {
                MessageBox.Show("Thông tin không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (pq.suaNguoiDung(txtTENDN.Text.Trim(), txtMATKHAU.Text.Trim(), cbbMaNV.SelectedValue.ToString(), ktCheckBox(ckbQuyen)))
                {
                    dtgvNguoiDung.DataSource = pq.LoadNguoiDung();
                    MessageBox.Show("Cập nhật thông tin người dùng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin người dùng thất bại!", "THất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void btnThemNND_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaNhom.Text) || String.IsNullOrEmpty(txtTenNhom.Text))
            {
                MessageBox.Show("Thông tin không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!pq.ktNNguoiDung(txtMaNhom.Text.Trim()))
            {
                MessageBox.Show("Mã nhóm này đã có nên không thể thêm! Xin vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(pq.themNNguoiDung(txtMaNhom.Text.Trim(),txtTenNhom.Text.Trim(),txttGhiChu.Text.Trim()))
            {
                dtgvNNDung.DataSource = pq.LoadNNDung();
                MessageBox.Show("Thêm thông tin nhóm người dùng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm thông tin nhóm người dùng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnXoaNND_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaNhom.Text))
            {
                MessageBox.Show("Thông tin không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (pq.xoaNNguoiDung(txtMaNhom.Text))
                {
                    dtgvNNDung.DataSource = pq.LoadNNDung();
                    MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSuaNND_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaNhom.Text) || String.IsNullOrEmpty(txtTenNhom.Text))
            {
                MessageBox.Show("Thông tin không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (pq.ktNNguoiDung(txtMaNhom.Text.Trim()))
            {
                MessageBox.Show("Mã nhóm này không có nên không thể sửa! Xin vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pq.suaNNguoiDung(txtMaNhom.Text.Trim(), txtTenNhom.Text.Trim(), txttGhiChu.Text.Trim()))
            {
                dtgvNNDung.DataSource = pq.LoadNNDung();
                MessageBox.Show("Thêm thông tin nhóm người dùng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm thông tin nhóm người dùng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void dtgvMaHinh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dtgvMaHinh!= null)
            {
                txtMaMH.Text = dtgvMaHinh.CurrentRow.Cells[0].Value.ToString();
                txtTenMH.Text = dtgvMaHinh.CurrentRow.Cells[1].Value.ToString();
            }
        }

        private void btnThemMH_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaMH.Text) || String.IsNullOrEmpty(txtTenMH.Text))
            {
                MessageBox.Show("Thông tin không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!pq.ktKhoaChinhManhHinh(txtMaMH.Text))
            {
                MessageBox.Show("Thông tin này đã có nên không thể thêm! Xin vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pq.themManHinh(txtMaMH.Text,txtTenMH.Text))
            {
                dtgvMaHinh.DataSource = pq.LoadManHinh();
                MessageBox.Show("Thêm thông tin màn hình thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Thêm thông tin màn hình thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnXoaMH_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaMH.Text))
            {
                MessageBox.Show("Thông tin không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (pq.xoaManHinh(txtMaMH.Text))
                {
                    dtgvMaHinh.DataSource = pq.LoadManHinh();
                    dtgvPhanQuyen.DataSource = pq.LoadPhanQuyen();
                    MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSuaMH_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaMH.Text) || String.IsNullOrEmpty(txtTenMH.Text))
            {
                MessageBox.Show("Thông tin không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (pq.suaManHinh(txtMaMH.Text.Trim(), txtTenMH.Text.Trim()))
                {
                    dtgvMaHinh.DataSource = pq.LoadManHinh();
                    MessageBox.Show("Cập nhật thông tin người dùng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin người dùng thất bại!", "THất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void dtgvPhanQuyen_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dtgvPhanQuyen!=null)
            {
                txtMAMHPQ.Text = dtgvPhanQuyen.CurrentRow.Cells[0].Value.ToString();
                txtMaNhomPQ.Text = dtgvPhanQuyen.CurrentRow.Cells[1].Value.ToString();
                checkQuyen(ckkPQ, dtgvPhanQuyen.CurrentRow.Cells[2].Value.ToString());
            }
        }

        private void btnThemPQ_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMaNhomPQ.Text) || String.IsNullOrEmpty(txtMAMHPQ.Text))
            {
                MessageBox.Show("Thông tin không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!pq.ktKhoaChinhPQ(txtMAMHPQ.Text, txtMaNhomPQ.Text))
            {

                MessageBox.Show("Thông tin này đã có nên không thể thêm! Xin vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (pq.themPQ(txtMaNhomPQ.Text.Trim(), txtMAMHPQ.Text.Trim(), ktCheckBox(ckkPQ)))
                {
                    dtgvPhanQuyen.DataSource = pq.LoadPhanQuyen();
                    MessageBox.Show("Thêm phân quyền thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Thêm phân quyền thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }


        private void btnSuaPQ_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMAMHPQ.Text) || String.IsNullOrEmpty(txtMaNhomPQ.Text))
            {
                MessageBox.Show("Thông tin không được để trống", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
            }
            else
            {
                if (pq.suaPQ(txtMaNhomPQ.Text.Trim(), txtMAMHPQ.Text.Trim(), ktCheckBox(ckkPQ)))
                {
                    dtgvPhanQuyen.DataSource = pq.LoadPhanQuyen();
                    MessageBox.Show("Cập nhật thông tin thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin thất bại!", "THất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

    }
}
