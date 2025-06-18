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
    public partial class fTaiXe : Form
    {
        private string id;
        public fTaiXe(string s)
        {
            InitializeComponent();
            this.Width = 1000;
            this.Height = 700;
            this.id = s;
            this.Tag = false;
        }
        
        private void fTaiXe_Load(object sender, EventArgs e)
        {
            rdb_tatca.Checked = true;
            string s = $"select * from ChuyenXe where IdTaiXe = '{id}'";
            DataTable dt = DataProvider.Instance.ExcuteQuery(s);
            dscx.DataSource = dt;
            string st = $"select * from TAIXE where IdTaiXe = '{id}'";
            DataTable t = DataProvider.Instance.ExcuteQuery(st);
            DataRow dr = t.Rows[0];
            txt_id.Text = dr["IdTaiXe"].ToString();
            txt_hoten.Text = dr["HoTen"].ToString();
            txt_diachi.Text = dr["DiaChi"].ToString();
            txt_sdt.Text = dr["SoDT"].ToString();
            txt_ngaysinh.Text = Convert.ToDateTime(dr["NgaySinh"]).ToString("dd/MM/yyyy");
        }

        private void btn_xem_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "";

                if (rdb_tatca.Checked)
                {
                    fTaiXe_Load(sender, e);
                }
                else if (rdb_dk.Checked)
                {
                    query = $"SELECT * FROM ChuyenXe WHERE IdTaiXe = '{id}' AND TinhTrang = N'Dự kiến'";

                    DataTable dt = DataProvider.Instance.ExcuteQuery(query);
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy chuyến xe dự kiến");
                        return;
                    }
                    dscx.DataSource = dt;
                }
                else if (rdb_dahuy.Checked)
                {
                    query = $"SELECT * FROM ChuyenXe WHERE IdTaiXe =  '{id}' AND TinhTrang = N'Đã hủy'";
                    DataTable dt = DataProvider.Instance.ExcuteQuery(query);
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy chuyến xe đã hủy");
                        return;
                    }
                    dscx.DataSource = dt;
                }
                else if (rdb_dangchay.Checked)
                {
                    query = $"SELECT * FROM ChuyenXe WHERE IdTaiXe =  '{id}' AND TinhTrang = N'Đang chạy'";
                    DataTable dt = DataProvider.Instance.ExcuteQuery(query);
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy chuyến xe đang chạy");
                        return;
                    }
                    dscx.DataSource = dt;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_ttcanhan_Click(object sender, EventArgs e)
        {
            ThongTinTaiKhoan f = new ThongTinTaiKhoan(txt_id.Text, txt_hoten.Text);
            f.ShowDialog();

        }

        private void btn_tttaikhoan_Click(object sender, EventArgs e)
        {
            TTTaiKhoan f = new TTTaiKhoan(txt_id.Text);

            f.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "EXEC UP_CHUYENXE @IDCHUYEN";
                int kt = DataProvider.Instance.ExcuteNonQuery(sql,new object[] {txt_idchuyenxe.Text});
                if (kt > 0)
                {
                    MessageBox.Show("Đã khởi hành chuyến xe " + txt_idchuyenxe.Text);
                }
            }
             catch(Exception ex)
            {
                MessageBox.Show("Lỗi:" +ex.Message);
            } 
             
            

        }

        private void dscx_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dscx.Rows.Count)
                {

                    DataGridViewRow selectedRow = dscx.Rows[e.RowIndex];
                    string masotuyen = selectedRow.Cells["idChuyen"].Value.ToString();
                    txt_idchuyenxe.Text = masotuyen;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Tag = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Tag = false;
            this.Close();
        }
    }
}
