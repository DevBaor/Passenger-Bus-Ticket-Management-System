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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace QuanLyBanVe
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void btn_DangNhap_Click(object sender, EventArgs e)
        {
            string username=txt_Ten.Text;
            string password = txt_Mk.Text;
            if (login(username,password))
            {
                string tem = username.ToLower();
                if (tem.StartsWith("tx"))
                {

                    fTaiXe f = new fTaiXe(username);
                    this.Hide();
                    f.ShowDialog();
                    bool kt = (bool)f.Tag;

                    if (kt)
                        Application.Exit();
                    this.Show();
                    this.Show();

                }
                else
                {
                    fMain f = new fMain(username);

                    this.Hide();
                    f.ShowDialog();
                    bool kt = (bool)f.Tag;

                    if (kt)
                        Application.Exit();
                    this.Show();
                }
                
            }
            else { MessageBox.Show("Sai tên tk hoặc mật khẩu!!"); }
        }
        bool login(string userName, string passWord)
            { return AccountDAO.Instance.Login(userName,passWord); 
        }
        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo!", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            //{
            //    e.Cancel = true;
            //}
        }

        
    }
}
