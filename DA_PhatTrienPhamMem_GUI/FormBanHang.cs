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
using static DA_PhatTrienPhanMem_BLL_DAL.BLLDAL_BanHang;

namespace DA_PhatTrienPhamMem_GUI
{
    public partial class FormBanHang : Form
    {
        public FormBanHang()
        {
            InitializeComponent();
        }

        BLLDAL_BanHang bh = new BLLDAL_BanHang();


        private void FormBanHang_Load(object sender, EventArgs e)
        {
            loadDataGridView();

            cbbTenSP.DataSource = bh.loadSanPhamCombobox();
            cbbTenSP.ValueMember = "MASP";
            cbbTenSP.DisplayMember = "TENSP";

            cbbTenKH.DataSource = bh.LoadTenKH();
            cbbTenKH.ValueMember = "MAKH";
            cbbTenKH.DisplayMember = "TENKH";

            btnThanhToan.Enabled = false;
            
        }

        public void loadDataGridView()
        {
            List<BanHang> ds = Program.ds.Where(t => t.MaKH == cbbTenKH.SelectedValue.ToString()).ToList();
            dtgvBanHang.DataSource = ds;

        }

        private void FormBanHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.formMain.Show();
        }

        private void cbbTenSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            double? dongiaban = bh.traVeDonGiaBan(cbbTenSP.SelectedValue.ToString());
            txtGiaBan.Text = dongiaban + "";
            txtSLCo.Text = bh.traVeSoLuongSP(cbbTenSP.SelectedValue.ToString())+"";
            txtMaSP.Text = bh.traVeMaSP(cbbTenSP.SelectedValue.ToString()) + "";
            txtTinhTrang.Text = bh.traVeTinhTrangSP(cbbTenSP.SelectedValue.ToString()) + "";
        }

        private void txtSLMua_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtGiaBan.Text.Trim()) && !String.IsNullOrEmpty(txtSLMua.Text.Trim()))
            {
                int sl = int.Parse(txtSLMua.Text.Trim());
                double dgb = double.Parse(txtGiaBan.Text.Trim());
                txtThanhTien.Text = sl * dgb + "";
            }
        }


        private void dtgvBanHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dtgvBanHang.CurrentRow!=null)
            {
                txtMaSP.Text = dtgvBanHang.CurrentRow.Cells[0].ToString();
                cbbTenSP.Text = dtgvBanHang.CurrentRow.Cells[1].ToString();
                txtGiaBan.Text = dtgvBanHang.CurrentRow.Cells[3].ToString();
                txtSLMua.Text = dtgvBanHang.CurrentRow.Cells[2].ToString();
                txtTinhTrang.Text = dtgvBanHang.CurrentRow.Cells[4].ToString();
                txtThanhTien.Text= dtgvBanHang.CurrentRow.Cells[5].ToString();
                cbbTenKH.Text = dtgvBanHang.CurrentRow.Cells[6].ToString();
            }    
        }

        private void btnThemSanPham_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtSLMua.Text)||String.IsNullOrEmpty(txtThanhTien.Text)||String.IsNullOrEmpty(txtGiaBan.Text))
            {
                MessageBox.Show("Số lượng mua, thành tiền và thông tin giá bán không được để trống","LỖII", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int slmua = int.Parse(txtSLMua.Text);
            int? slhienco = int.Parse(txtSLCo.Text);
            double thanhtien = double.Parse(txtThanhTien.Text);
            double dongiaban = double.Parse(txtGiaBan.Text);
            
            if(!bh.ktKhoaChinh(txtMaSP.Text,Program.ds))
            {
                MessageBox.Show("Mã về sản phẩm này đã có vui lòng thử lại", "LỖIII", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(slhienco<0)
            {
                MessageBox.Show("Sản phẩm này hiện tại đã hết hàng!! Vui lòng chọn sản phẩm khác", "LỖIII", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }    
            if(slmua<0||thanhtien<0)
            {
                MessageBox.Show("Số lượng mua và thành tiền không được bằng 0", "LỖIII", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (slmua > slhienco)
            {
                MessageBox.Show("Số lượng mua nhập vào hiện tại lớn hơn số lương sản phẩm hiện có", "LỖIII", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int tongslList =(int) bh.tongSoLuongSPTrongList(cbbTenSP.SelectedValue.ToString(), Program.ds);
            if(tongslList+slmua>slhienco)
            {
                MessageBox.Show("Tổng số lượng sản phẩm này trong danh sách đang lớn hơn số lượng sản phẩm hiện có! Xin vui lòng kiểm tra lại ở sản phẩm này ở các phòng khác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (bh.themBanHang(txtMaSP.Text,bh.traVeTenSP(cbbTenSP.SelectedValue.ToString()),cbbTenKH.SelectedValue.ToString(),txtTinhTrang.Text,slmua,dongiaban,thanhtien,Program.ds))
            {
                dtgvBanHang.DataSource = null;
                dtgvBanHang.Rows.Clear();
                dtgvBanHang.Refresh();
                dtgvBanHang.DataSource = Program.ds;
                MessageBox.Show("Thêm sản phẩm mua thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnThanhToan.Enabled = true;
              

            }
            else
            {
                MessageBox.Show("Thêm sản phẩm mua thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtMaSP.Text)||String.IsNullOrEmpty(cbbTenSP.SelectedValue.ToString()))
            {
                MessageBox.Show("Chưa chọn sản phầm cần xóa!!! Vui lòng chọn lại", "LỖIII", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int slmua = int.Parse(txtSLMua.Text);
            int slhienco = int.Parse(txtSLCo.Text);
            int slsaukhixoa = slmua + slhienco;
            if (bh.xoaBanHang(txtMaSP.Text,cbbTenSP.SelectedValue.ToString(),Program.ds))
            {
                txtSLCo.Text = Convert.ToString(slsaukhixoa);
                loadDataGridView();
                MessageBox.Show("Xóa sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Xóa sản phẩm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnCapNhatSP_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtSLMua.Text) || String.IsNullOrEmpty(txtThanhTien.Text) || String.IsNullOrEmpty(txtGiaBan.Text))
            {
                MessageBox.Show("Số lượng mua, thành tiền và thông tin giá bán không được để trống", "LỖII", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int slmua = int.Parse(txtSLMua.Text);
            int? slhienco = int.Parse(txtSLCo.Text);
            double thanhtien = double.Parse(txtThanhTien.Text);
            double dongiaban = double.Parse(txtGiaBan.Text);
            if (slhienco < 0)
            {
                MessageBox.Show("Sản phẩm này hiện tại đã hết hàng!! Vui lòng chọn sản phẩm khác", "LỖIII", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (slmua < 0 || thanhtien < 0)
            {
                MessageBox.Show("Số lượng mua và thành tiền không được bằng 0", "LỖIII", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (slmua > slhienco)
            {
                MessageBox.Show("Số lượng mua nhập vào hiện tại lớn hơn số lương sản phẩm hiện có", "LỖIII", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int tongslList = (int)bh.tongSoLuongSPTrongList(cbbTenSP.SelectedValue.ToString(), Program.ds);
            int hieuSo = (int)bh.traVeHieuSoSPKhiCapNhat(cbbTenSP.SelectedValue.ToString(), slmua, Program.ds);
            if (tongslList + hieuSo > slhienco)
            {
                MessageBox.Show("Tổng số lượng sản phẩm này trong danh sách đang lớn hơn số lượng sản phẩm hiện có! Xin vui lòng kiểm tra lại ở sản phẩm này ở các phòng khác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(bh.suaBanHang(txtMaSP.Text, cbbTenSP.SelectedValue.ToString(), cbbTenKH.SelectedValue.ToString(), txtTinhTrang.Text,slmua,dongiaban,thanhtien,Program.ds))
            {
                loadDataGridView();
                MessageBox.Show("Cập nhật dịch vụ thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cập nhật dịch vụ thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            double? thanhtoan = Program.ds.Where(t => t.MaKH == cbbTenKH.SelectedValue.ToString()).Sum(t => t.Thanhtien);
            FormThanhToan fm = new FormThanhToan();
            fm.MaKH = cbbTenKH.SelectedValue.ToString();
            fm.ShowDialog();
            this.Close();

        }
    }
}
