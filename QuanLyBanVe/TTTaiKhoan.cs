using QuanLyBanVe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanVe
{
    public partial class TTTaiKhoan : Form
    {
        public TTTaiKhoan(string idnd)
        {
            InitializeComponent();
            this.idnd = idnd;
            loadtt(idnd);
        }
        string idnd;
        void loadtt(string idnd)
        {
            try
            {
                string sql = "select HoTen from NhanVien where IdNhanVien= '" + idnd + "'";
                txt_hoten.Text = (string)DataProvider.Instance.ExcuteScalar(sql);
                string sql1 = "select GioiTinh from NhanVien where IdNhanVien= '" + idnd + "'";
                txt_gioitinh.Text = (string)DataProvider.Instance.ExcuteScalar(sql1);
                string sql2 = "select diachi from NhanVien where IdNhanVien= '" + idnd + "'";
                txt_diachi.Text = (string)DataProvider.Instance.ExcuteScalar(sql2);
                string sql3 = "select NgaySinh from NhanVien where IdNhanVien= '" + idnd + "'";
                DateTime dt = (DateTime)DataProvider.Instance.ExcuteScalar(sql3);
                txt_ngaysinh.Text = dt.ToString("dd/MM/yyyy");
                string sql4 = "select sodt from NhanVien where IdNhanVien= '" + idnd + "'";
                txt_sodt.Text = (string)DataProvider.Instance.ExcuteScalar(sql4);
                string sql5 = "select luongcoban from NhanVien where IdNhanVien= '" + idnd + "'";

                int luong = (int)DataProvider.Instance.ExcuteScalar(sql5);
                txt_luongcb.Text=luong.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:" + ex.Message);
            }
            
        }
        private void btn_CapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "update NhanVien set DiaChi ='" + txt_diachi.Text + "', SoDT='" + txt_sodt.Text + "' where IdNhanVien='" + idnd + "'";
                int kt = DataProvider.Instance.ExcuteNonQuery(sql);
                if (kt > 0)
                {
                    MessageBox.Show("Cập nhật thành công");
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Lỗi:"+ex.Message);
            }
            
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
