using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using DA_PhatTrienPhanMem_BLL_DAL.QL_LINHKIENDataSetTableAdapters;


namespace DA_PhatTrienPhanMem_BLL_DAL
{
    public class BLLDAL_QL_NguoiDung
    {

        public int Check_Config()
        {
            if (Properties.Settings.Default.LTWNCConn == string.Empty)
                return 1; 
            SqlConnection sqlConn = new SqlConnection(Properties.Settings.Default.LTWNCConn);
            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Closed)
                    sqlConn.Open();
                return 0; 
            }
            catch
            {
                return 2; 
            }
        }

        public int Check_user(string user, string pass)
        {
            SqlDataAdapter da_User = new SqlDataAdapter("Select * from NguoiDung where TenDN='" + user + "' and MatKhau ='" + pass + "'", Properties.Settings.Default.LTWNCConn);
            DataTable dt = new DataTable();
            da_User.Fill(dt);
            if (dt.Rows.Count == 0)
            {
                return 1;   
            }
            else if (dt.Rows[0][2] == null || dt.Rows[0][2].ToString() == "False")
            {
                return 2;   
            }
            return 0; 
        }


        public DataTable GetServerName()
        {
            DataTable dt = new DataTable();
            dt = SqlDataSourceEnumerator.Instance.GetDataSources();
            return dt;
        }

        public DataTable GetDBName(string serverName, string user, string pass)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select name from sys.Databases",
            "Data Source=" + serverName + ";Initial Catalog=master;User ID=" + user + ";pwd = " +
            pass + "");
            da.Fill(dt);
            return dt;
        }

        public void SaveConfig(string pServer, string pUser, string pPass, string pDBName)
        {
            DA_PhatTrienPhanMem_BLL_DAL.Properties.Settings.Default.LTWNCConn = "Data Source=" + pServer + ";Initial Catalog=" + pDBName + ";User ID=" + pUser + ";Password=" + pPass + "";
            DA_PhatTrienPhanMem_BLL_DAL.Properties.Settings.Default.Save();
        }

        public List<string> GetMaNhomNguoiDung(string pTenDangNhap)
        {

            List<string> kqMNND = new List<string>();

            QL_NGUOIDUNGNHOMNGUOIDUNGTableAdapter da = new QL_NGUOIDUNGNHOMNGUOIDUNGTableAdapter();

            DataTable dtkq = da.GetDataByDSNND(pTenDangNhap);

            for (int i = 0; i < dtkq.Rows.Count; i++)
            {
                kqMNND.Add(dtkq.Rows[i].ItemArray[1].ToString());
            }
            return kqMNND;

        }

        public DataTable GetMaManHinh(string pMaNhomND)
        {
            PHANQUYENTableAdapter da = new PHANQUYENTableAdapter();

            return da.GetDataByDSMH(pMaNhomND);
        }

    }
}
