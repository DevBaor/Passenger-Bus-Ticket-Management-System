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
    public partial class fThongKe : Form
    {
        public fThongKe()
        {
            
            InitializeComponent();
            this.Width = 1200;
            this.Height = 500;
            DataTable dt = DataProvider.Instance.ExcuteQuery("select dbo.TinhTongDoanhThu () as tong");
            int tong = 0;
            foreach (DataRow dr in dt.Rows)
            {
                tong = int.Parse(dr["tong"].ToString());
            }
            textBox1.Text = tong.ToString();
            cmb_thongke.SelectedIndex = 5;

            DataTable da = DataProvider.Instance.ExcuteQuery("select TenTuyen as N'Tên', DoanhThu as N'Doanh thu' from ThongKeDoanhThuTheoTuyen");
            dataGridView1.DataSource = da;
        }
        private void btn_dtnam_Click(object sender, EventArgs e)
        {
            try
            {
                int year = int.Parse(comboBox1.SelectedItem.ToString());
                DataTable dt = DataProvider.Instance.ExcuteQuery($"SELECT dbo.TinhTongDoanhThuNam( {year}) AS TongDoanhThu;");
                int tong = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    tong = int.Parse(dr["TongDoanhThu"].ToString());
                }
                textBox2.Text = tong.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("lỗi:" + ex.Message);
            }
        }
        private void btn_dtthang_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime selectedDate = dtpk_NgayBD.Value;

                // Lấy tháng và năm
                int month = selectedDate.Month; // Tháng
                int year = selectedDate.Year;   // Năm
                DataTable dt = DataProvider.Instance.ExcuteQuery($"SELECT dbo.TinhTongDoanhThuThang({month}, {year}) AS TongDoanhThu;");
                int tong = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    tong = int.Parse(dr["TongDoanhThu"].ToString());
                }
                textBox4.Text = tong.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("lỗi:" + ex.Message);
            }
        }


        private void btn_lnthang_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime selectedDate = dateTimePicker1.Value;

                // Lấy tháng và năm
                int month = selectedDate.Month; // Tháng
                int year = selectedDate.Year;   // Năm
                DataTable dt = DataProvider.Instance.ExcuteQuery($"SELECT dbo.TinhLoiNhuanTheoThang({month}, {year}) AS TongDoanhThu;");
                int tong = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    tong = int.Parse(dr["TongDoanhThu"].ToString());
                }
                textBox6.Text = tong.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("lỗi:" + ex.Message);
            }
        }

        private void btn_lnnam_Click(object sender, EventArgs e)
        {
            try
            {
                int year = int.Parse(comboBox2.SelectedItem.ToString());
                DataTable dt = DataProvider.Instance.ExcuteQuery($"SELECT dbo.TinhLoiNhuanTheoNam( {year}) AS TongDoanhThu;");
                int tong = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    tong = int.Parse(dr["TongDoanhThu"].ToString());
                }
                textBox5.Text = tong.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("lỗi:" + ex.Message);
            }
        }

        private void cmb_thongke_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_thongke.SelectedIndex == 0)
            {
                DataTable da = DataProvider.Instance.ExcuteQuery("select * from SoLuongChuyenXeTheoTuyen");
                dataGridView1.DataSource = da;
                return;
            }
            if (cmb_thongke.SelectedIndex == 1)
            {
                DataTable da = DataProvider.Instance.ExcuteQuery("select * from ThongKeVeBanTheoThang");
                dataGridView1.DataSource = da;
                return;
            }
            if (cmb_thongke.SelectedIndex == 2)
            {
                DataTable da = DataProvider.Instance.ExcuteQuery("select * from SoVeDaBanTheoTuyen");
                dataGridView1.DataSource = da;
                return;
            }
            if (cmb_thongke.SelectedIndex == 3)
            {
                DataTable da = DataProvider.Instance.ExcuteQuery("select * from ThongKeVeBanTheoNam");
                dataGridView1.DataSource = da;
                return;
            }
            if (cmb_thongke.SelectedIndex == 4)
            {
                DataTable da = DataProvider.Instance.ExcuteQuery("select * from ThongKeVeDaBanTheoChuyenXe");
                dataGridView1.DataSource = da;
                return;
            }

        }
       
       
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime tu = dt_tu.Value;
            DateTime den = dt_den.Value;
            // Đường dẫn kết nối đến cơ sở dữ liệu của bạn
            string connectionString = DataProvider.Instance.connectionstring;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("BaoCaoDoanhThu", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Truyền các tham số vào proc
                        command.Parameters.Add(new SqlParameter("@TuNgay", dt_tu.Value));
                        command.Parameters.Add(new SqlParameter("@DenNgay", dt_den.Value));

                        // Thực thi stored procedure và nhận kết quả
                        var result = command.ExecuteScalar();
                        textBox3.Text = result.ToString();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }
    }
}
