using QuanLyBanVe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanVe
{
    public partial class ThongTinTaiKhoan : Form
    {
        public ThongTinTaiKhoan(string idnv,string ten)
        {
            InitializeComponent();
            this.Width = 600;
            this.Height = 700;
            txt_tendn.Text = idnv;
            txt_TenHT.Text = ten;
        }
        void loadTT(string id,string mk,string mkm)
        {
            string sql = "EXEC USP_UpDtaeMK @IDND , @MK , @MKMOI";
            int kt = DataProvider.Instance.ExcuteNonQuery(sql,new object[] {id,mk,mkm});
            if (kt > 0) 
            {
                MessageBox.Show("Đổi mật khẩu thành công!");
            }
            else MessageBox.Show("Sai mật khẩu!");
        }
        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_CapNhat_Click(object sender, EventArgs e)
        {
            loadTT(txt_tendn.Text, txt_MatKhau.Text, txt_mkmoi.Text);
        }
    }
}
