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
    public partial class FormCauHinh : Form
    {
        BLLDAL_QL_NguoiDung CauHinh = new BLLDAL_QL_NguoiDung();
        public FormCauHinh()
        {
            InitializeComponent();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            CauHinh.SaveConfig(cbbServerName.Text, txtUser.Text, txtPass.Text, cbbDBName.Text);

            if (Program.formDangNhap == null || Program.formDangNhap.IsDisposed)
            {
                Program.formDangNhap = new FormDangNhap();
            }
            this.Visible = false;
            Program.formDangNhap.Show();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cbbServerName_DropDown(object sender, EventArgs e)
        {
            cbbServerName.DataSource = CauHinh.GetServerName();
            cbbServerName.DisplayMember = "ServerName";
        }

        private void cbbDBName_DropDown(object sender, EventArgs e)
        {
            cbbDBName.DataSource = CauHinh.GetDBName(cbbServerName.Text, txtUser.Text, txtPass.Text);
            cbbDBName.DisplayMember = "name";
        }
    }
}
