using QuanLyBanVe.DAO;
using QuanLyBanVe.DTO;
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
    public partial class fQLBV : Form
    {
        public fQLBV()
        {
            InitializeComponent();
            LoadXe();
        }

        #region Method
        void LoadXe()
        {
            List<XE> xelist = XeDAO.Instance.LoadXeList();
            foreach(XE item in xelist)
            {
                Button btn = new Button()
                { Width = XeDAO.XEWidth,Height =XeDAO.XEHeight };
                btn.Text = item.Name + Environment.NewLine + item.Sochongoi;
                switch (item.Sochongoi)
                {
                    case 16:
                        btn.BackColor = Color.Green;
                        break;
                    default: btn.BackColor = Color.Red; break;
                }

                flpTableXe.Controls.Add(btn);
            }

        }
        #endregion
        #region Event
        private void đăngXuấtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void đăngXuấtToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
           ThongTinTaiKhoan t =new ThongTinTaiKhoan();
            t.ShowDialog();
        }

        private void adminToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            fAdmin fAd = new fAdmin();
            fAd.ShowDialog();
        }
        #endregion
    }
}
