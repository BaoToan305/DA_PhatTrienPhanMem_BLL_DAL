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
    public partial class FormThongKe : Form
    {
        BLL_DALThongKe tk = new BLL_DALThongKe();
        
        public FormThongKe()
        {
            InitializeComponent();
        }

        private void FormThongKe_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.formMain.Show();
        }

        private void FormThongKe_Load(object sender, EventArgs e)
        {
            lblNgay.Visible = false;
            lblThang.Visible = false;
            lblNam.Visible = false;
            cboNgay.Visible = false;
            cboThang.Visible = false;
            cboNam.Visible = false;

            cboNam.DataSource = tk.loadDuLieu(1999, 2030);
            cboThang.DataSource = tk.loadDuLieu(1, 12);
        }

        private void rdoNgay_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoNgay.Checked)
            {
                lblNgay.Visible = true;
                lblThang.Visible = true;
                lblNam.Visible = true;
                cboNgay.Visible = true;
                cboThang.Visible = true;
                cboNam.Visible = true;
                cboNgay.Enabled = false;
                cboThang.Enabled = false;
            }
            else
            {
                lblNgay.Visible = false;
                lblThang.Visible = false;
                lblNam.Visible = false;
                cboNgay.Visible = false;
                cboThang.Visible = false;
                cboNam.Visible = false;
            }
        }

        private void rdoThang_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoThang.Checked)
            {
                lblThang.Visible = true;
                lblNam.Visible = true;
                cboThang.Visible = true;
                cboNam.Visible = true;
                cboThang.Enabled = true;
            }
            else
            {
                lblThang.Visible = false;
                lblNam.Visible = false;
                cboThang.Visible = false;
                cboNam.Visible = false;
            }
        }

        private void rdoNam_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoNam.Checked)
            {
                lblNam.Visible = true;
                cboNam.Visible = true;
            }
            else
            {
                lblNam.Visible = false;
                cboNam.Visible = false;
            }
        }

        private void cboNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboThang.Enabled = true;
        }

        private void cboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                cboNgay.Enabled = true;
                int thang = int.Parse(cboThang.SelectedValue.ToString());
                int nam = int.Parse(cboNam.SelectedValue.ToString());
                switch (thang)
                {
                    case 1:
                    case 3:
                    case 5:
                    case 7:
                    case 8:
                    case 10:
                    case 12:
                        cboNgay.DataSource = tk.loadDuLieu(1, 31);
                        break;
                    case 4:
                    case 6:
                    case 9:
                    case 11:
                        cboNgay.DataSource = tk.loadDuLieu(1, 30);
                        break;
                    case 2:
                        cboNgay.DataSource = tk.loadThang2(nam);
                        break;
                }
            }
            catch
            {

            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            int ngay, thang, nam;

            if (rdoNgay.Checked)
            {
                try
                {
                    ngay = int.Parse(cboNgay.SelectedValue.ToString());
                    thang = int.Parse(cboThang.SelectedValue.ToString());
                    nam = int.Parse(cboNam.SelectedValue.ToString());
                    bool kq = tk.kiemTraNgayThangHopLe(ngay, thang, nam);
                    if (!kq)
                    {
                        MessageBox.Show("Ngày tháng năm không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    dtgv_ThongKe.DataSource = tk.thongKeDoanhThu(ngay, thang, nam);
                    string a = String.Format("{0:0,00}", tk.tongTien(ngay, thang, nam));
                    lblTongTien.Text = a;
                }
                catch
                {
                    MessageBox.Show("VUi lòng chọn ngày, tháng, năm hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (rdoThang.Checked)
            {
                try
                {
                    thang = int.Parse(cboThang.SelectedValue.ToString());
                    nam = int.Parse(cboNam.SelectedValue.ToString());
                    dtgv_ThongKe.DataSource = tk.thongKeDoanhThuThangVaNam(thang, nam);
                    string a = String.Format("{0:0,00}", tk.tongTien(thang, nam));
                    lblTongTien.Text = a;
                }
                catch
                {
                    MessageBox.Show("VUi lòng chọn ngày, tháng, năm hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                nam = int.Parse(cboNam.SelectedValue.ToString());
                dtgv_ThongKe.DataSource = tk.thongKeDoanhThuNam(nam);
                string a = String.Format("{0:0,00}", tk.tongTien(nam));
                lblTongTien.Text = a;
            }
        }
    }
}
