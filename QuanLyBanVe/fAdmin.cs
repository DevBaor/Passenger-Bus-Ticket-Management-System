using QuanLyBanVe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanVe
{
    public partial class fAdmin : Form
    { 
        public fAdmin()
        {
            InitializeComponent();
            dtgv_TaiKhoan.DataSource = DataProvider.Instance.ExcuteQuery("select * from dbo.NguoiDung");
         
        }
        private void fAdmin_Load(object sender, EventArgs e)
        {

        }
    }
}
