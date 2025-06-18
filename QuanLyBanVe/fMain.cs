using QuanLyBanVe.DAO;
using QuanLyBanVe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanVe
{
    public partial class fMain : Form
    {
        string idnd;
        public fMain(string mand)
        {
            InitializeComponent();
            this.idnd = mand;
            this.Width = 1200;
            this.Height = 950;
            this.Tag = false;
            DisEnabletabpage();
            loadND(mand);
            
        }
        #region PhanQuyenDangnhap
        void loadND(string mand)
        {
            List<Quyen> listquyen;
            this.mand = mand;
            string sql = "select l.TenLoaiND from NguoiDung n join LoaiNguoiDung l on n.IdLoaiND=l.IdLoaiND where IdND='" + mand + "'";
            string lnd = (string)DataProvider.Instance.ExcuteScalar(sql);
            lbe_loaind.Text = lnd;
            if (lnd == "Administrator")
            {
                EnableAlltabpage();
                btn_tttaikhoan.Enabled = false;
                //btn_thongke.Enabled = true;
                btn_datve.Enabled = false;// admin k có mã nv nên k bán vé dc
            }
            else if (lnd == "Nhân Viên" || lnd == "Quản Lý")
            {
                btn_ttcanhan.Enabled = true;
                string sql1 = "select nv.HoTen from NguoiDung n join NhanVien nv on n.IdND=nv.IdNhanVien where IdND='" + mand + "'";
                lbe_tennd.Text = (string)DataProvider.Instance.ExcuteScalar(sql1);
                listquyen = QuyenDAO.Instance.LoadListQuyen(mand);
                Enabletabpage(listquyen);
            }
            else
            {
                string sql1 = "select nv.HoTen from NguoiDung n join TaiXe nv on n.IdND=nv.IdTaiXe where IdND='" + mand + "'";
                lbe_tennd.Text = (string)DataProvider.Instance.ExcuteScalar(sql1);
                listquyen = QuyenDAO.Instance.LoadListQuyen(mand);
                Enabletabpage(listquyen);
            }


        }
        void DisEnabletabpage()
        {
            foreach (TabPage tab in tab_ngdung.TabPages)
            { tab.Enabled = false; }
        }
        void EnableAlltabpage()
        {
            foreach (TabPage tab in tab_ngdung.TabPages)
            { tab.Enabled = true; }
            //--tab người dùng
            LoadLoaiNguoiDung();
            //--Tab bán vé
            loadTuyenxe();
            //--tab xe
            LoadXe();
            //--tab địa điểm
            LOADDIADIEM();
            //--tab tuyến xe
            LOADTUYENXE();
            //--tab phần quyền
            loadNguoiDung_fpq();
            //--tab chuyến xe
            LoadTuyen();
            btn_thongke.Enabled = true;
        }
        void Enabletabpage(List<Quyen> lq)
        {
            foreach (Quyen quyen in lq)
            {
                if (quyen.idphanquyen == "PQ001")//quản lý người dùng
                {
                    //--tab người dùng
                    LoadLoaiNguoiDung();
                    motab(tab_ngdung.TabPages[0]);
                }    
                if (quyen.idphanquyen == "PQ002")//quản lý chuyến xe
                {
                    //--tab chuyến xe
                    LoadTuyen();
                    motab(tab_ngdung.TabPages[3]);
                }    
                    
                if (quyen.idphanquyen == "PQ003")//quản lý vé
                {
                    //--Tab bán vé
                    loadTuyenxe();
                    motab(tab_ngdung.TabPages[4]);
                }    
                    
                if (quyen.idphanquyen == "PQ004")//quản lý xe
                {
                    //--tab xe
                    LoadXe();
                    motab(tab_ngdung.TabPages[1]);
                }    
                    
                if (quyen.idphanquyen == "PQ005")//quản lý địa điểm
                {
                    //--tab địa điểm
                    LOADDIADIEM();
                    motab(tab_ngdung.TabPages[6]);
                }    
                    
                if (quyen.idphanquyen == "PQ006")//quản lý tuyến xe
                {
                    //--tab tuyến xe
                    LOADTUYENXE();
                    motab(tab_ngdung.TabPages[2]);
                }    
                    
                if (quyen.idphanquyen == "PQ007")//Phân quyền
                {
                    //--tab phần quyền
                    loadNguoiDung_fpq();
                    motab(tab_ngdung.TabPages[5]);
                }    
                    
                if (quyen.idphanquyen == "PQ008")//Thống kê
                {
                    btn_thongke.Enabled = true;
                }    
                    

            }
        }
        void motab(TabPage tab)
        {
            tab.Enabled = true;
        }
        #endregion
        #region tabguoidung
        #region Load

        private void LoadLoaiNguoiDung()
        {
            string query = "SELECT IdLoaiND FROM dbo.LOAINGUOIDUNG where IdLoaiND='nhan_vien' or IdLoaiND='tai_xe' ";
            DataTable data = DataProvider.Instance.ExcuteQuery(query);

            cbo_LoaiNGDung.DataSource = data;
            cbo_LoaiNGDung.ValueMember = "idLoaiND";
        }
        
        #endregion
        #region GetDSTK

        public DataTable GetListAccount()
        {
            return DataProvider.Instance.ExcuteQuery("select IdND,IdLoaiND from dbo.NGUOIDUNG");
        }
        #endregion
        #region KETNOISQL

        private string connectionString = DataProvider.Instance.connectionstring;
        private SqlConnection connection;

        public fMain(SqlConnection connection)
        {
            this.connection = connection;
        }
        #endregion
        #region TIMKIEM
        private void btn_TimKiem_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (cbo_LoaiNGDung.SelectedValue != null)
                {
                    string idLoaiND = cbo_LoaiNGDung.SelectedValue.ToString();
                    if (idLoaiND == "Nhan_Vien" || idLoaiND == "Quan_Ly")
                    {
                        string query = "SELECT * FROM dbo.NHANVIEN ";
                        lb_gc.Enabled = false;
                        lb_tt.Enabled = false;
                        txt_GhiChu.Enabled = false;
                        txt_TìnhTrang.Enabled = false;
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            using (SqlCommand cmd = new SqlCommand(query, connection))
                            {
                                //cmd.Parameters.AddWithValue("@idLoaiND", idLoaiND);
                                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                                {
                                    DataTable dt = new DataTable();
                                    adapter.Fill(dt);
                                    dtgv_NguoiDung.DataSource = dt;
                                }
                            }
                        }
                    }
                    else if (idLoaiND == "Tai_Xe")
                    {
                        string query = "SELECT * FROM dbo.TAIXE ";
                        lb_gc.Enabled = true;
                        lb_tt.Enabled = true;
                        txt_GhiChu.Enabled = true;
                        txt_TìnhTrang.Enabled = true;
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            using (SqlCommand cmd = new SqlCommand(query, connection))
                            {
                                //cmd.Parameters.AddWithValue("@idLoaiND", idLoaiND);
                                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                                {
                                    DataTable dt = new DataTable();
                                    adapter.Fill(dt);
                                    dtgv_NguoiDung.DataSource = dt;
                                }
                            }
                        }

                    }
                    else
                    {
                        string query = "SELECT * FROM dbo.TAIXE,dbo.NHANVIEN ";

                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            using (SqlCommand cmd = new SqlCommand(query, connection))
                            {
                                //cmd.Parameters.AddWithValue("@idLoaiND", idLoaiND);
                                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                                {
                                    DataTable dt = new DataTable();
                                    adapter.Fill(dt);
                                    dtgv_NguoiDung.DataSource = dt;
                                }
                            }
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn loại người dùng.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        #endregion
        #region CLICKVAODTGV
        private void dtgv_NguoiDung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string idLoaiND = cbo_LoaiNGDung.SelectedValue.ToString();
            if (idLoaiND == "Nhan_Vien" || idLoaiND == "Quan_Ly")
            {
                
                try
                {
                    if (e.RowIndex >= 0 && e.RowIndex < dtgv_NguoiDung.Rows.Count)
                    {
                        selectedIdNguoiDung = dtgv_NguoiDung.Rows[e.RowIndex].Cells["IdNhanVien"].Value.ToString();
                    }
                    if (e.RowIndex >= 0 && e.RowIndex < dtgv_NguoiDung.Rows.Count)
                    {
                        DataGridViewRow selectedRow = dtgv_NguoiDung.Rows[e.RowIndex];
                        string idNhanVien = selectedRow.Cells["IdNhanVien"].Value.ToString();
                        string hoTen = selectedRow.Cells["HoTen"].Value.ToString();
                        DateTime ngaySinh = Convert.ToDateTime(selectedRow.Cells["NgaySinh"].Value);
                        string gioiTinh = selectedRow.Cells["GioiTinh"].Value.ToString();
                        string diaChi = selectedRow.Cells["DiaChi"].Value.ToString();
                        string soDT = selectedRow.Cells["SoDT"].Value.ToString();
                        int lcb = int.Parse(selectedRow.Cells["LuongCoBan"].Value.ToString());
                        txt_ID.Text = idNhanVien;
                        txt_Hoten.Text = hoTen;
                        dtpNgaySinh.Value = ngaySinh;

                        if (gioiTinh == "Nam")
                        {
                            rdo_nam.Checked = true;
                        }
                        else
                        {
                            rdo_nu.Checked = true;
                        }


                        txt_DiaChi.Text = diaChi;
                        txt_SDT.Text = soDT;
                        txt_LCB.Text = Convert.ToString(lcb);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
            else if (idLoaiND == "Tai_Xe")
            {
                
                try
                {
                    if (e.RowIndex >= 0 && e.RowIndex < dtgv_NguoiDung.Rows.Count)
                    {
                        selectedIdNguoiDung = dtgv_NguoiDung.Rows[e.RowIndex].Cells["IdTaiXe"].Value.ToString();
                    }
                    if (e.RowIndex >= 0 && e.RowIndex < dtgv_NguoiDung.Rows.Count)
                    {
                        DataGridViewRow selectedRow = dtgv_NguoiDung.Rows[e.RowIndex];
                        string idNhanVien = selectedRow.Cells["IdTaiXe"].Value.ToString();
                        string hoTen = selectedRow.Cells["HoTen"].Value.ToString();
                        DateTime ngaySinh = Convert.ToDateTime(selectedRow.Cells["NgaySinh"].Value);
                        string gioiTinh = selectedRow.Cells["GioiTinh"].Value.ToString();
                        string diaChi = selectedRow.Cells["DiaChi"].Value.ToString();
                        string soDT = selectedRow.Cells["SoDT"].Value.ToString();
                        int lcb = int.Parse(selectedRow.Cells["LuongCoBan"].Value.ToString());
                        string tt = selectedRow.Cells["TinhTrang"].Value.ToString();
                        string gc = selectedRow.Cells["GhiChu"].Value.ToString();
                        txt_ID.Text = idNhanVien;
                        txt_Hoten.Text = hoTen;
                        dtpNgaySinh.Value = ngaySinh;

                        if (gioiTinh == "Nam")
                        {
                            rdo_nam.Checked = true;
                        }
                        else
                        {
                            rdo_nu.Checked = true;
                        }


                        txt_DiaChi.Text = diaChi;
                        txt_SDT.Text = soDT;
                        txt_LCB.Text = Convert.ToString(lcb);
                        txt_TìnhTrang.Text = tt;
                        txt_GhiChu.Text = gc;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        #endregion
        #region UPDATE
        private void btn_SetupMK_Click_1(object sender, EventArgs e)
        {
            try
            {
                string id = txt_ID.Text.ToLower();
                string sql = "";
                if (id.Contains("nv") || id.Contains("ql"))
                {
                    sql = "update NguoiDung set PassND= '123' where IdND='" + txt_ID.Text + "'";
                    int kt = DataProvider.Instance.ExcuteNonQuery(sql);
                    if (kt > 0)
                    {
                        MessageBox.Show("Cấp lại mật khẩu thành công:MK là 123");
                    }
                    else { MessageBox.Show("Lỗi ID"); }
                }
                else
                {
                    sql = "update NguoiDung set PassND= '12345' where IdND='" + txt_ID.Text + "'";
                    int kt = DataProvider.Instance.ExcuteNonQuery(sql);
                    if (kt > 0)
                    {
                        MessageBox.Show("Cấp lại mật khẩu thành công:MK là 12345");
                    }
                    else { MessageBox.Show("Lỗi ID"); }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi", ex.Message);
            }
        }

        #endregion
        #region THEMND
        private void btn_ThemND_Click_1(object sender, EventArgs e)
        {
            try
            {
                string idLoaiND = cbo_LoaiNGDung.SelectedValue.ToString();
                if (idLoaiND == "Nhan_Vien" || idLoaiND == "Quan_Ly")
                {
                    string idNhanVien = txt_ID.Text;
                    string hoTen = txt_Hoten.Text;
                    DateTime ngaySinh = dtpNgaySinh.Value;
                    string gioiTinh = rdo_nam.Checked ? rdo_nam.Text : "Nữ";
                    string diaChi = txt_DiaChi.Text;
                    string soDT = txt_SDT.Text;
                    string lcb = txt_LCB.Text;

                    string query = "INSERT INTO dbo.NHANVIEN (idNhanVien, HoTen, NgaySinh, GioiTinh, DiaChi, SoDT,LuongCoBan) VALUES ( @idNhanVien , @HoTen , @NgaySinh , @GioiTinh , @DiaChi , @SoDT , @LuongCoBan )";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            //cmd.Parameters.AddWithValue("@IdNhanVien", idNhanVien);
                            //cmd.Parameters.AddWithValue("@HoTen", hoTen);
                            //cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                            //cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                            //cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                            //cmd.Parameters.AddWithValue("@SoDT", soDT);
                            //cmd.Parameters.AddWithValue("@LuongCoBan", lcb);

                            //int kt = cmd.ExecuteNonQuery();
                            int kt = DataProvider.Instance.ExcuteNonQuery(query, new object[] { idNhanVien, hoTen, ngaySinh, gioiTinh, diaChi, soDT, lcb });
                            if (kt >= 2)
                            {
                                MessageBox.Show("Thêm Thành công");
                                txt_ID.Clear();
                                txt_Hoten.Clear();
                                txt_DiaChi.Clear();
                                txt_SDT.Clear();
                                txt_LCB.Clear();
                                loadNguoiDung_fpq();

                            }    
                        }
                    }
                   
                }
                else if (idLoaiND == "Tai_Xe")
                {

                    string idNhanVien = txt_ID.Text;
                    string hoTen = txt_Hoten.Text;
                    DateTime ngaySinh = dtpNgaySinh.Value;
                    string gioiTinh = rdo_nam.Checked ? rdo_nam.Text : rdo_nu.Text;
                    string diaChi = txt_DiaChi.Text;
                    string soDT = txt_SDT.Text;
                    string lcb = txt_LCB.Text;
                    string tt = txt_TìnhTrang.Text;
                    string gc = txt_GhiChu.Text;



                    string query = "INSERT INTO dbo.Taixe (Idtaixe, HoTen, NgaySinh, GioiTinh, DiaChi, SoDT,LuongCoBan,TinhTrang,GhiChu) " +
                                   "VALUES (@IdNhanVien, @HoTen, @NgaySinh, @GioiTinh, @DiaChi, @SoDT,@LuongCoBan,@TinhTrang,@GhiChu)";


                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@IdNhanVien", idNhanVien);
                            cmd.Parameters.AddWithValue("@HoTen", hoTen);
                            cmd.Parameters.AddWithValue("@NgaySinh", ngaySinh);
                            cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                            cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                            cmd.Parameters.AddWithValue("@SoDT", soDT);
                            cmd.Parameters.AddWithValue("@LuongCoBan", lcb);
                            cmd.Parameters.AddWithValue("@TinhTrang", tt);
                            cmd.Parameters.AddWithValue("@GhiChu", gc);
                            cmd.ExecuteNonQuery();
                            loadNguoiDung_fpq();
                            loadIdTaixe();
                        }
                    }
                }

                LoadDuLieuVaoDataGridView();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message);
            }
        }
        
        #endregion
        #region LOADSQLQUAFORM

        private void LoadDuLieuVaoDataGridView()
        {
            try
            {
                string query;
                if (cbo_LoaiNGDung.SelectedValue.ToString() == "Nhan_Vien")
                {
                     query = "SELECT * FROM dbo.NHANVIEN";
                }    
                else  query = "SELECT * FROM dbo.taixe";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dtgv_NguoiDung.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message);
            }
        }
        private string selectedIdNguoiDung = null;
        #endregion
        #region XOAND
        private void btn_XoaND_Click_1(object sender, EventArgs e)
        {
            try
            {

                if (selectedIdNguoiDung != null)
                {

                    DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa người dùng có ID {selectedIdNguoiDung}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {

                        string idLoaiND = cbo_LoaiNGDung.SelectedValue.ToString();
                        if (idLoaiND == "Nhan_Vien" || idLoaiND == "Quan_Ly")
                        {
                            string query = "exec xoaNV @IdNhanVien";
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                using (SqlCommand cmd = new SqlCommand(query, connection))
                                {
                                    cmd.Parameters.AddWithValue("@IdNhanVien", selectedIdNguoiDung);
                                    int rowsAffected = cmd.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("Xóa thành công.");
                                        LoadDuLieuVaoDataGridView();

                                        loadNguoiDung_fpq();
                                        
                                        selectedIdNguoiDung = null;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Không tìm thấy người dùng cần xóa.");
                                    }
                                }
                            }
                        }
                        else if (idLoaiND == "Tai_Xe")
                        {
                            string query = "DELETE FROM dbo.TAIXE WHERE IdTaiXe = @IdTaiXe";
                            using (SqlConnection connection = new SqlConnection(connectionString))
                            {
                                connection.Open();
                                using (SqlCommand cmd = new SqlCommand(query, connection))
                                {
                                    cmd.Parameters.AddWithValue("@IdTaiXe", selectedIdNguoiDung);
                                    int rowsAffected = cmd.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        MessageBox.Show("Xóa người dùng thành công.");
                                        LoadDuLieuVaoDataGridView();

                                        loadNguoiDung_fpq();
                                        loadIdTaixe();
                                        selectedIdNguoiDung = null;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Không tìm thấy người dùng cần xóa.");
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn người dùng cần xóa trong DataGridView.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message);
            }
        }

        #endregion
        #region HUYND
        private void btn_Huy_Click(object sender, EventArgs e)
        {
            txt_ID.Clear();
            txt_Hoten.Clear();
            rdo_nam.Checked = false;
            rdo_nu.Checked = false;
            txt_SDT.Clear();
            txt_DiaChi.Clear();
            txt_LCB.Clear();
            txt_TìnhTrang.Clear();
            txt_GhiChu.Clear();
        }

        #endregion
        #region SUATTND
        private void btn_SuaND_Click(object sender, EventArgs e)
        {
            try
            {
                connection = new SqlConnection(connectionString);
                string updateQuery = "";

                string idLoaiND = cbo_LoaiNGDung.SelectedValue.ToString();
                string id = txt_ID.Text;
                string hoTen = txt_Hoten.Text;
                DateTime ngaySinh = dtpNgaySinh.Value;
                string gioiTinh = rdo_nam.Checked ? "Nam" : "Nữ";
                string diaChi = txt_DiaChi.Text;
                string soDT = txt_SDT.Text;
                decimal luongCoBan = Convert.ToDecimal(txt_LCB.Text);
                string tt = txt_TìnhTrang.Text;
                string gc = txt_GhiChu.Text;




                if (idLoaiND == "Nhan_Vien" || idLoaiND == "Quan_Ly")
                {

                    updateQuery = @"UPDATE NHANVIEN  SET 
                           HoTen=@HoTen,
                        DiaChi = @DiaChi, SoDT = @SoDT, LuongCoBan = @LuongCoBan
                        WHERE IdNhanVien = @IdNhanVien";


                }
                else if (idLoaiND == "Tai_Xe")
                {
                    updateQuery = @"UPDATE TAIXE SET 
                         DiaChi = @DiaChi, SoDT = @SoDT, LuongCoBan = @LuongCoBan,
                        TinhTrang = @TinhTrang, GhiChu = @GhiChu  WHERE IdTaiXe = @IdTaiXe";
                }
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    if (idLoaiND == "Nhan_Vien")
                    {
                        command.Parameters.AddWithValue("@IdNhanVien", id);
                        command.Parameters.AddWithValue("@HoTen", hoTen);
                        command.Parameters.AddWithValue("@DiaChi", diaChi);
                        command.Parameters.AddWithValue("@SoDT", soDT);
                        command.Parameters.AddWithValue("@LuongCoBan", luongCoBan);
                    }

                    else if (idLoaiND == "Tai_Xe")
                    {
                        command.Parameters.AddWithValue("@IdTaiXe", id);
                        command.Parameters.AddWithValue("@DiaChi", diaChi);
                        command.Parameters.AddWithValue("@SoDT", soDT);
                        command.Parameters.AddWithValue("@LuongCoBan", luongCoBan);
                        command.Parameters.AddWithValue("@TinhTrang", tt);
                        command.Parameters.AddWithValue("@GhiChu", gc);
                    }
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();

                    foreach (DataGridViewRow row in dtgv_NguoiDung.Rows)
                    {
                        if (row.Cells["IdNhanVien"].Value.ToString() == id)
                        {
                            row.Cells["HoTen"].Value = hoTen;
                            row.Cells["DiaChi"].Value = diaChi;
                            row.Cells["SoDT"].Value = soDT;
                            row.Cells["LuongCoBan"].Value = luongCoBan;
                            if (idLoaiND == "Tai_Xe")
                            {
                                row.Cells["TinhTrang"].Value = tt;
                                row.Cells["GhiChu"].Value = gc;
                            }

                            break;
                        }
                    }

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật thành công!");
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        #endregion
        #endregion
        #region TABXE
        
        #region LOADXE
        void LoadXe()
        {
            flp_XE.Controls.Clear();
            List<XE> xelist = XeDAO.Instance.LoadXeList2();
            foreach (XE item in xelist)
            {
                Button btn = new Button()
                { Width = XeDAO.XEWidth, Height = XeDAO.XEHeight };
                btn.Text = item.Soxe + Environment.NewLine + item.Tinhtrang + Environment.NewLine + item.Sochongoi;
                btn.Tag = item;
                if (item.Tinhtrang == "Đang hoạt động")
                {
                    btn.BackColor = Color.Aqua;
                }
                else if (item.Tinhtrang == "Đang bảo trì")
                {
                    btn.BackColor = Color.Yellow;
                }
                else btn.BackColor = Color.Red;

                btn.Click += btn_Click1;
                flp_XE.Controls.Add(btn);

            }
        }
        #endregion
        #region LOADBTNLENGROUPBOX
        private void btn_Click1(object sender, EventArgs e)
        {
            XE xe = (sender as Button).Tag as XE;
            txt_sx.Text = xe.Soxe.ToString();
            txt_hx.Text = xe.HieuXe.ToString();
            txt_cn.Text = xe.Sochongoi.ToString();
            txt_tt.Text = xe.Tinhtrang;

        }


        #endregion
        #region THEMXE
        private void btn_ThemXe_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_sx.Text) ||
            string.IsNullOrEmpty(txt_hx.Text) ||
            string.IsNullOrEmpty(txt_cn.Text) ||
            string.IsNullOrEmpty(txt_tt.Text))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            int sochongoi;
            if (!int.TryParse(txt_cn.Text, out sochongoi))
            {
                MessageBox.Show("Invalid number of seats.");
                return;
            }
            XE newXe = new XE(txt_sx.Text, txt_hx.Text, sochongoi, txt_tt.Text);

            try
            {
                string query = "INSERT INTO XE (So_Xe, Hieu_Xe, So_Cho_Ngoi, TinhTrang) " +
                               "VALUES (@So_Xe, @Hieu_Xe, @So_Cho_Ngoi, @TinhTrang)";

                using (SqlConnection connection = new SqlConnection(DataProvider.Instance.connectionstring))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@So_Xe", newXe.Soxe);
                    command.Parameters.AddWithValue("@Hieu_Xe", newXe.HieuXe);
                    command.Parameters.AddWithValue("@So_Cho_Ngoi", newXe.Sochongoi);
                    command.Parameters.AddWithValue("@TinhTrang", newXe.Tinhtrang);

                    connection.Open();
                    command.ExecuteNonQuery();
                    loadSoXe();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding vehicle to database: " + ex.Message);
                return;
            }
            //Button newBtn = CreateXeButton(newXe);
            //flp_XE.Controls.Add(newBtn);
            LoadXe();
            txt_sx.Text = "";
            txt_hx.Text = "";
            txt_cn.Text = "";
            txt_tt.Text = "";
        }


        #endregion
        #region XOAXE
        private void btn_XoaXe_Click(object sender, EventArgs e)
        {
            try
            {

                string sql = "delete from Xe where So_Xe='" + txt_sx.Text + "'";
                int kt = DataProvider.Instance.ExcuteNonQuery(sql);
                if (kt > 0)
                {
                    MessageBox.Show("Xóa Thành Công!");
                }
                txt_sx.Clear();
                txt_hx.Clear();
                txt_cn.Clear();
                txt_tt.Clear();

                flp_XE.Controls.Clear();
                LoadXe();
                loadSoXe();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
        #region HUYTHAOTAC
        private void btn_HuyThaoTac_Click(object sender, EventArgs e)
        {
            txt_sx.Clear();
            txt_hx.Clear();
            txt_cn.Clear();
            txt_tt.Clear();
            txt_sx.Focus();
        }

        #endregion
        #region SUATTXE

        private void btn_SuaTTXe_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "Update Xe Set TinhTrang=N'" + txt_tt.Text + "' where So_Xe='" + txt_sx.Text + "'";
                int kt = DataProvider.Instance.ExcuteNonQuery(sql);
                if (kt > 0)
                {
                    MessageBox.Show("Sửa thông tin thành công");
                }
                LoadXe();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
        #endregion
        #region TABDIADIEM
        #region LOADDIADIEM
        public void LOADDIADIEM()
        {
            try
            {
                string query = "SELECT * FROM dbo.DIADIEM";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dtgv_DiaDiem.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message);
            }
        }
        #endregion
        #region CLICKDTGVDIADIEM
        private string selectedIDDiaDiem = null;
        private void dtgv_DiaDiem_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string iddiadiem = txt_IdDiaDiemChuyen.Text.ToString();
            if (e.RowIndex >= 0 && e.RowIndex < dtgv_DiaDiem.Rows.Count)
            {
                selectedIDDiaDiem = dtgv_DiaDiem.Rows[e.RowIndex].Cells["IdDiaDiem"].Value.ToString();
            }
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dtgv_DiaDiem.Rows.Count)
                {

                    DataGridViewRow selectedRow = dtgv_DiaDiem.Rows[e.RowIndex];
                    string iddd = selectedRow.Cells["IdDiaDiem"].Value.ToString();
                    string tendiadiem = selectedRow.Cells["TenDiaDiem"].Value.ToString();
                    txt_IdDiaDiemChuyen.Text = iddd;
                    txt_TenDiaDiemChuyen.Text = tendiadiem;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
       
        #endregion
        #region THEMDIADIEM
        private void btn_ThemChuyen_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txt_IdDiaDiemChuyen.Text;
                string tendiadiem = txt_TenDiaDiemChuyen.Text;
                string query = "INSERT INTO dbo.DIADIEM (IdDiaDiem,TenDiaDiem) " +
                               "VALUES (@IdDiaDiem,@TenDiaDiem)";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@IdDiaDiem", id);
                        cmd.Parameters.AddWithValue("@TenDiaDiem", tendiadiem);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Thêm thành công@!@");
                LoadDuLieuVaodtgvDIADIEM();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message);
            }
        }
        
        private void LoadDuLieuVaodtgvDIADIEM()
        {
            try
            {
                string query = "SELECT * FROM dbo.DIADIEM";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dtgv_DiaDiem.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message);
            }
        }
        #endregion
        #region XOADIADIEM
        private void btn_XoaDiaDiem_Click(object sender, EventArgs e)
        {
            try
            {

                string sql = "delete from DIADIEM where IdDiaDiem='" + txt_IdDiaDiemChuyen.Text + "'";
                int kt = DataProvider.Instance.ExcuteNonQuery(sql);
                if (kt > 0)
                {
                    MessageBox.Show("Xóa Thành Công!");
                }
                txt_IdDiaDiemChuyen.Clear();
                txt_TenDiaDiemChuyen.Clear();
                LOADDIADIEM();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
        #region HUYDIADIEM
        private void btn_HuyThaoTacDiaDiem_Click(object sender, EventArgs e)
        {
            txt_IdDiaDiemChuyen.Clear();
            txt_TenDiaDiemChuyen.Clear();
        }


        #endregion
        #endregion
        #region TABTUYENXE
        #region LOADTUYENXE
        public void LOADTUYENXE()
        {
            try
            {
                string query = "SELECT * FROM dbo.TUYENXE";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dtgv_TuyenXe.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message);
            }
        }
        #endregion
        #region HUYTHAOTACTRENFORMTUYENXE
        private void btn_HUYTHAOTACTUYEN_Click(object sender, EventArgs e)
        {
            txt_TENTUYENXE.Clear();
            txt_DIADIEMDI.Clear();
            txt_DIADIEMDEN.Clear();
        }
        
        #endregion
        #region XEMCHITIETTUYEN
        private void button24_Click1(object sender, EventArgs e)
        {
            try
            {
                if (txt_MASOTUYEN.Text != null)
                {
                    string idtuyen = txt_MASOTUYEN.Text.ToString();

                    string query = "select * from TuyenXe where IdTuyen='" + idtuyen + "'";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand(query, connection))
                        {
                            cmd.Parameters.AddWithValue("@idtuyen", idtuyen);
                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                adapter.Fill(dt);
                                dtgv_TuyenXe.DataSource = dt;
                            }
                        }
                    }
                }

                else
                {
                    MessageBox.Show("Vui lòng chọn loại người dùng.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

        }
        #endregion
        #region CLICKVAODTGVTUYEN
        private string selectedIdTuyen = null;
        private void dtgv_TuyenXe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string idTuyen = txt_MASOTUYEN.Text.ToString();
            if (e.RowIndex >= 0 && e.RowIndex < dtgv_TuyenXe.Rows.Count)
            {
                selectedIdTuyen = dtgv_TuyenXe.Rows[e.RowIndex].Cells["IdTuyen"].Value.ToString();
            }
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dtgv_TuyenXe.Rows.Count)
                {

                    DataGridViewRow selectedRow = dtgv_TuyenXe.Rows[e.RowIndex];
                    string masotuyen = selectedRow.Cells["IdTuyen"].Value.ToString();
                    string tentuyen = selectedRow.Cells["TenTuyen"].Value.ToString();
                    string dddi = selectedRow.Cells["IdDiaDiemDi"].Value.ToString();
                    string ddden = selectedRow.Cells["IdDiaDiemDen"].Value.ToString();
                    txt_TENTUYENXE.Text = tentuyen;
                    txt_DIADIEMDI.Text = dddi;
                    txt_DIADIEMDEN.Text = ddden;
                    txt_MASOTUYEN.Text = masotuyen;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        #endregion
        #region THEMTUYENXE
        private void btn_THEMTUYENXE_Click(object sender, EventArgs e)
        {
            try
            {
                string masotuyen = txt_MASOTUYEN.Text;
                string tentuyen = txt_TENTUYENXE.Text;
                string ddi = txt_DIADIEMDI.Text;
                string dden = txt_DIADIEMDEN.Text;
                string query = "INSERT INTO dbo.TUYENXE (IdTuyen,TenTuyen,IdDiaDiemDi,IdDiaDiemDen) " +
                               "VALUES (@IdTuyen,@TenTuyen,@IdDiaDiemDi,@IdDiaDiemDen)";


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@IdTuyen", masotuyen);
                        cmd.Parameters.AddWithValue("@TenTuyen", tentuyen);
                        cmd.Parameters.AddWithValue("@IdDiaDiemDi", ddi);
                        cmd.Parameters.AddWithValue("@IdDiaDiemDen", dden);

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Thêm thành công@!@");
                LoadDuLieuVaodtgvTuyen();
                loadTuyenxe();
                LoadTuyen();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm: " + ex.Message);
            }
        }
        
        private void LoadDuLieuVaodtgvTuyen()
        {
            try
            {
                string query = "SELECT * FROM dbo.TUYENXE";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            dtgv_TuyenXe.DataSource = dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message);
            }
        }
        #endregion
        #region XOATUYENXE
        private void btn_XOATUYENXE_Click(object sender, EventArgs e)
        {
            try
            {

                string sql = "delete from TUYENXE where IdTuyen='" + txt_MASOTUYEN.Text + "'";
                int kt = DataProvider.Instance.ExcuteNonQuery(sql);
                if (kt > 0)
                {
                    MessageBox.Show("Xóa Thành Công!");
                }
                txt_MASOTUYEN.Clear();
                txt_TENTUYENXE.Clear();
                txt_DIADIEMDI.Clear();
                txt_DIADIEMDEN.Clear();
                LoadTuyen();
                loadTuyenxe();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        #endregion
        #endregion
        #region tabBanVe
        string mand;
        string hoadon;
        int gia;
        
        
        void loadTuyenxe()
        {
            string sql = "select * from TuyenXe";
            DataTable dt = DataProvider.Instance.ExcuteQuery(sql);
            cbo_tuyenxe.DataSource = dt;
            cbo_tuyenxe.DisplayMember = "TenTuyen";
            cbo_tuyenxe.ValueMember = "IdTuyen";
        }
        

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        #region eventMethod
        private void button3_Click(object sender, EventArgs e)
        {
            tab_ngdung.SelectedIndex = 0;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tab_ngdung.SelectedIndex = 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tab_ngdung.SelectedIndex = 2;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tab_ngdung.SelectedIndex = 3;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tab_ngdung.SelectedIndex = 4;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo!", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                this.Tag = true;
                this.Close();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Tag = false;
            this.Close();

        }

        private void btn_timchuyenxe_Click(object sender, EventArgs e)
        {

            loadChuyenxe(cbo_tuyenxe.SelectedValue.ToString(), dtpk_ngaydi.Value);
        }
        void loadChuyenxe(string idtuyen, DateTime ngaydi)
        {
            flpn_chuyenxe.Controls.Clear();
            List<Chuyenxe> tablelist = ChuyenxeDAO.Instance.LoadCXeList(idtuyen, ngaydi);
            if (tablelist.Count <= 0)
            {
                MessageBox.Show("Không có chuyến xe nào!");
                flpn_ghe.Controls.Clear();
                lbe_ngaydi.Text = "...";
                lbe_giodi.Text = "...";
                lbe_giave.Text = "...";
                lbe_soxe.Text = "...";
                return;
            }
            foreach (Chuyenxe item in tablelist)
            {
                Button btn = new Button() { Width = ChuyenxeDAO.XEWidth, Height = ChuyenxeDAO.XEHeight };
                btn.Text = "Xe" + Environment.NewLine + item.Soxe + Environment.NewLine + Environment.NewLine + item.Tinhtrang;
                btn.Click += Btn_Click; //tạo sự kiện click cho button
                btn.Tag = item; //Lưu table 

                switch (item.Tinhtrang)
                {
                    case "Dự kiến":
                        btn.BackColor = Color.Aqua;
                        break;
                    case "Đã chạy":
                        btn.BackColor = Color.Yellow;
                        break;
                    case "Đã hủy":
                        btn.BackColor = Color.Red;
                        break;
                    default:
                        btn.BackColor = Color.Red;
                        break;
                }

                flpn_chuyenxe.Controls.Add(btn);
            }
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            Chuyenxe cxe = (sender as Button).Tag as Chuyenxe;
            lbe_ngaydi.Text = cxe.Ngaydi.ToString("dd/MM/yyyy");
            lbe_giodi.Text = cxe.Gio.ToString();
            lbe_soxe.Text = cxe.Soxe.ToString();
            CultureInfo culture = new CultureInfo("vi-VN"); //tạo culture là việt nam

            //Thread.CurrentThread.CurrentCulture = culture; //chuyển main thread hiện tại sang culture

            // setting thread đang chạy sang culture
            lbe_giave.Text = cxe.Giave.ToString("c", culture);
            gia = cxe.Giave;
            loadGhengoi(cxe.Idchuyen);
            if (cxe.Tinhtrang != "Dự kiến")
            {
                foreach (Control ctr in flpn_ghe.Controls)
                {
                    ctr.Enabled = false;
                }
            }
        }

        void loadGhengoi(string idchuyen)
        {
            flpn_ghe.Controls.Clear();

            List<Chongoi> tablelist = ChongoiDAO.Instance.LoadChongoiList(idchuyen);

            foreach (Chongoi item in tablelist)
            {
                Button btn1 = new Button() { Width = ChongoiDAO.XEWidth, Height = ChongoiDAO.XEHeight };
                btn1.Text = item.Tenchongoi + Environment.NewLine + item.Tinhtrang;
                btn1.Click += BtnChongoi_Click; //tạo sự kiện click cho button
                btn1.Tag = item; //Lưu chỗ ngồi

                switch (item.Tinhtrang)
                {
                    case "Đã đặt":
                        btn1.BackColor = Color.Gray;

                        break;

                    default:
                        btn1.BackColor = Color.Pink;
                        break;
                }

                flpn_ghe.Controls.Add(btn1);
            }
        }

        private void BtnChongoi_Click(object sender, EventArgs e)
        {

            Button btn = sender as Button;
            if (btn != null) //Luôn luôn kiểm tra xem có phải null!
            {
                if (btn.BackColor == Color.Pink)
                {
                    btn.BackColor = Color.Yellow;

                }
                else if (btn.BackColor == Color.Yellow) //Đã thêm else if
                {
                    btn.BackColor = Color.Pink;

                }
                else if (btn.BackColor == Color.Gray)
                {
                    btn.BackColor = Color.Red;

                }
                else if (btn.BackColor == Color.Red)
                {
                    btn.BackColor = Color.Gray;

                }
            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            tab_ngdung.SelectedIndex = 4;
        }
        bool ktdatve()
        {
            foreach (Button btn in flpn_ghe.Controls)
            {
                if (btn.BackColor == Color.Yellow)
                {
                    return true;
                }
            }
            return false;
        }
        bool ktHuyVe()
        {
            foreach (Button btn in flpn_ghe.Controls)
            {
                if (btn.BackColor == Color.Red)
                {
                    return true;
                }
            }
            return false;
        }
        private void btn_datve_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_tenkhachhang.Text.Trim().Length == 0 && txt_sodtkhach.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Vui long nhập đầy đủ thông tin");
                    return;
                }
                if (!ktdatve())
                {
                    MessageBox.Show("Vui lòng chọn vé!");
                    return;
                }
                //Tạo khách
                string sql1 = "select IdKhachHang from KhachHang where TenKhachHang=N'" + txt_tenkhachhang.Text + "' and SoDT='" + txt_sodtkhach.Text + "'";
                string idkh = (string)DataProvider.Instance.ExcuteScalar(sql1);

                if (idkh == null || idkh == "")
                    idkh = TaoMa.Instance.TaoMaKH();
                string sql = "EXEC USP_ThemKhachHang @IDKH , @TenKhachHang , @SoDT";
                int kt = DataProvider.Instance.ExcuteNonQuery(sql, new object[] { idkh, txt_tenkhachhang.Text, txt_sodtkhach.Text });

                txt_tenkhachhang.Tag = (string)DataProvider.Instance.ExcuteScalar(sql1);
                //Tạo hóa đơn
                string mahd = TaoMa.Instance.TaoMaHD();
                string sql2 = "EXEC TaoHoaDon @IDHD , @IdKhachHang , @IdNhanVien";
                int kt1 = DataProvider.Instance.ExcuteNonQuery(sql2, new object[] { mahd, idkh, mand });
                //Tạo vé
                int demve = 0;
                string idchuyenxe = "";
                string hoadon = "--------- HÓA ĐƠN ---------"
                    + Environment.NewLine + "Khách hàng: " + txt_tenkhachhang.Text
                    + Environment.NewLine + "Số điện thoại: " + txt_sodtkhach.Text
                    + Environment.NewLine + "Tuyến xe: " + cbo_tuyenxe.SelectedItem.ToString()
                    + Environment.NewLine + "Số xe: " + lbe_soxe.Text
                    + Environment.NewLine + "Ngày đi: " + lbe_ngaydi.Text
                    + Environment.NewLine + "Giờ đi: " + lbe_giodi.Text
                    + Environment.NewLine + "---------------------" + Environment.NewLine + "Thông tin vé:"
                    + Environment.NewLine + "Tên ghế       Giá";

                foreach (Button btn in flpn_ghe.Controls)
                {
                    if (btn.BackColor == Color.Yellow)
                    {
                        Chongoi cn = btn.Tag as Chongoi;
                        string mave = TaoMa.Instance.TaoMaVE();
                        string sql3 = "EXEC TaoVE @IdVe , @IdHoaDon , @IdChuyen , @SoXe , @IdKhachHang , @TenChoNgoi";
                        int kt2 = DataProvider.Instance.ExcuteNonQuery(sql3, new object[] { mave, mahd, cn.Idchuyen, cn.So_xe, idkh, cn.Tenchongoi });
                        demve++;
                        idchuyenxe = cn.Idchuyen;
                        hoadon += Environment.NewLine + cn.Tenchongoi + "\t" + lbe_giave.Text;
                    }
                }

                MessageBox.Show("Đã đặt thành công " + demve + " ve!", "Thông báo");

                if (idchuyenxe.Length > 0)
                {
                    loadGhengoi(idchuyenxe);
                }
                if (demve > 0)
                {
                    btn_inhoadon.Enabled = true;
                    hoadon += Environment.NewLine + "Tổng tiền: " + (gia * demve).ToString()
                        + Environment.NewLine + "Số lượng vé: " + demve.ToString();
                    this.hoadon = hoadon;
                }

                //MessageBox.Show(hoadon);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi:" + ex.Message);
            }
        }
        private void btn_inhoadon_Click(object sender, EventArgs e)
        {
            MessageBox.Show(hoadon);
        }

        private void btn_huyve_Click(object sender, EventArgs e)
        {
            if (!ktHuyVe())
            {
                MessageBox.Show("Vui lòng chọn vé!");
                return;
            }
            try
            {
                string idchuyenx = "";
                int demve = 0;
                foreach (Button btn in flpn_ghe.Controls)
                {
                    if (btn.BackColor == Color.Red)
                    {
                        Chongoi cn = btn.Tag as Chongoi;
                        string mave = TaoMa.Instance.GetMaveByGhengoi(cn.Idchuyen, cn.So_xe, cn.Tenchongoi);
                        string sql = "exec XoaVe @idve";
                        int kt = DataProvider.Instance.ExcuteNonQuery(sql, new object[] { mave });
                        demve++;
                        idchuyenx = cn.Idchuyen;

                    }
                }
                if (idchuyenx.Length > 0)
                {
                    loadGhengoi(idchuyenx);
                    MessageBox.Show("Xóa vé thành công" + demve.ToString() + " vé");
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Error");
            }




        }



        #endregion
        #endregion

        #region TABThongtincanhan

        private void btn_ttcanhan_Click(object sender, EventArgs e)
        {
            ThongTinTaiKhoan f = new ThongTinTaiKhoan(mand, lbe_tennd.Text);
            f.ShowDialog();
        }

        #endregion
        #region TABCHUYENXE
        #region TIMKIEMCHUYENXE
        void loadChuyenxe2(string idtuyen, DateTime ngaydi)
        {
            flpn_chuyenxe1.Controls.Clear();
            List<Chuyenxe> tablelist = ChuyenxeDAO.Instance.LoadCXeList(idtuyen, ngaydi);
            if (tablelist.Count <= 0)
            {
                MessageBox.Show("Không có chuyến xe nào!");
                txt_Giodi.Text = "...";
                txt_SoXe.Text = "...";
                txt_MASOCHUYEN.Text = "...";
                txt_GiaVe.Text = "...";
                txt_TinhTrang.Text = "...";
                return;
            }
            foreach (Chuyenxe item in tablelist)
            {
                Button btn = new Button() { Width = ChuyenxeDAO.XEWidth, Height = ChuyenxeDAO.XEHeight };
                btn.Text = "Xe" + Environment.NewLine + item.Soxe + Environment.NewLine + Environment.NewLine + item.Tinhtrang;
                btn.Click += Btn_Click11;
                btn.Tag = item;

                switch (item.Tinhtrang)
                {
                    case "Dự kiến":
                        btn.BackColor = Color.Aqua;
                        break;
                    case "Đã chạy":
                        btn.BackColor = Color.Yellow;
                        break;
                    case "Đã hủy":
                        btn.BackColor = Color.Red;
                        break;
                    default:
                        btn.BackColor = Color.Red;
                        break;
                }

                flpn_chuyenxe1.Controls.Add(btn);
            }
        }
        private void Btn_Click11(object sender, EventArgs e)
        {
            Chuyenxe xe = (sender as Button).Tag as Chuyenxe;
            txt_Giodi.Text = xe.Gio.ToString();
            txt_SoXe.Text = xe.Soxe.ToString();
            txt_MASOCHUYEN.Text = xe.Idchuyen.ToString();
            txt_GiaVe.Text = xe.Giave.ToString();
            txt_TinhTrang.Text = xe.Tinhtrang.ToString();

            txt_IDTAIXE.Text = xe.Idtaixe.ToString();

        }
        public void LoadTuyen()
        {
            try
            {
                string query = "SELECT * FROM dbo.TUYENXE";
                using (SqlConnection connection = new SqlConnection(DataProvider.Instance.connectionstring))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            comboBox1.DataSource = dt;
                            comboBox1.DisplayMember = "TenTuyen";
                            comboBox1.ValueMember = "IdTuyen";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message);
            }
            loadIdTaixe();
            loadSoXe();
        }
        void loadSoXe()
        {
            try
            {
                string query = "SELECT * FROM dbo.XE";
                using (SqlConnection connection = new SqlConnection(DataProvider.Instance.connectionstring))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            cbo_soxefcx.DataSource = dt;
                            cbo_soxefcx.DisplayMember = "So_xe";
                            cbo_soxefcx.ValueMember = "So_xe";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message);
            }
        }
        void loadIdTaixe()
        {
            try
            {
                string query = "SELECT * FROM dbo.TAIXE";
                using (SqlConnection connection = new SqlConnection(DataProvider.Instance.connectionstring))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            cbo_idtaixefcx.DataSource = dt;
                            cbo_idtaixefcx.DisplayMember = "idtaixe";
                            cbo_idtaixefcx.ValueMember = "idtaixe";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message);
            }
        }

        
        private void btn_tchuyencxe_Click(object sender, EventArgs e)
        {
            loadChuyenxe2(comboBox1.SelectedValue.ToString(), dtpk_ngdi.Value);
        }

        #endregion
        #region THEMCHUYENXE
        private void btn_THEMCHUYENXE_Click_1(object sender, EventArgs e)
        {

            //Chuyenxe newchuyen = new Chuyenxe(cbo_tuyenxe.Text, txt_MASOCHUYEN.Text, ngaydi, gio, txt_SoXe.Text, txt_IDTAIXE.Text, int.Parse(txt_GiaVe.Text), txt_TinhTrang.Text);

            try
            {

                //DateTime ngaydi = DateTime.ParseExact(dtpk_ngdi.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                string gio = txt_Giodi.Text.ToString();
                string query = "INSERT INTO CHUYENXE (IdChuyen,IdTuyen,NgayDi,Gio,So_Xe,IdTaiXe,GiaVe,TinhTrang) " +
                               "VALUES ( @IdChuyen , @IdTuyen , @NgayDi , @Gio , @So_Xe , @IdTaiXe , @GiaVe , @TinhTrang )";
                int kt = DataProvider.Instance.ExcuteNonQuery(query, new object[] { txt_MASOCHUYEN.Text, comboBox1.SelectedValue.ToString(), dtpk_ngdi.Value, gio, cbo_soxefcx.SelectedValue.ToString(), cbo_idtaixefcx.SelectedValue.ToString(), txt_GiaVe.Text, txt_TinhTrang.Text });
                if (kt > 0)
                {
                    MessageBox.Show("Thêm chuyến xe thành công ");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return;
            }
        }

        #endregion
        #region XOACHUYENXE
        private void btn_XOACHUYENXE_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "delete from ChuyenXe where IdChuyen='" + txt_MASOCHUYEN.Text + "'";
                int kt = DataProvider.Instance.ExcuteNonQuery(query);
                if (kt > 0)
                {
                    MessageBox.Show("Xóa chuyến xe thành công ");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding vehicle to database: " + ex.Message);
                return;
            }
        }


        #endregion
        #region HUYTHAOTACCHUYEN
        private void btn_HUYTHAOTACCHUYEN_Click(object sender, EventArgs e)
        {
            txt_Giodi.Clear();
            txt_SoXe.Clear();
            txt_MASOCHUYEN.Clear();
            txt_GiaVe.Clear();
            txt_TinhTrang.Clear();
            txt_IDTAIXE.Clear();
        }

        #endregion
        #region SUACHUYENXE
        private void btn_suach_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "update ChuyenXe set IdTaiXe='" + cbo_idtaixefcx.SelectedValue.ToString() + "', TinhTrang=N'" + txt_TinhTrang.Text + "',NgayDi = '"+dtpk_ngdi.Value+"' where IdChuyen='" + txt_MASOCHUYEN.Text + "'";
                int kt = DataProvider.Instance.ExcuteNonQuery(query);
                if (kt > 0)
                {
                    MessageBox.Show("Sửa chuyến xe thành công ");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return;
            }
        }
        
        #endregion
        #endregion
        #region TAbThongKe

        #endregion
        #region TabPhanQuyen
        void loadNguoiDung_fpq()
        {
            string sql = "select * from NguoiDung where IdND like 'NV%' or IdND like 'QL%' or IdND like 'TX%' ";
            DataTable dt= DataProvider.Instance.ExcuteQuery(sql);
            cbo_idnd.DataSource = dt;
            cbo_idnd.DisplayMember = "IDND";
            cbo_idnd.ValueMember = "IDND";
            string loaind = cbo_idnd.SelectedValue.ToString();
            if (loaind.Contains("NV"))
            {
                txt_loaindpq.Text = "Nhân Viên";
            }
            else

            if (loaind.Contains("QL"))
            {
                txt_loaindpq.Text = "Quản lý";
            }
            else txt_loaindpq.Text = "Tài xế";
            loadQuyen();
            loadDSquyen(cbo_idnd.SelectedValue.ToString());
        }
        void loadQuyen()
        {
            string sql = "select * from PhanQuyen";
            DataTable dt = DataProvider.Instance.ExcuteQuery(sql);
            cbo_quyen.DataSource = dt;
            cbo_quyen.DisplayMember = "tenquyen"; 
            cbo_quyen.ValueMember = "IDphanquyen";
        }
        void loadDSquyen(string idnd)
        {
            string sql = "select p.* from PhanQuyen p join NguoiDungPhanQuyen nd on p.IdPhanQuyen=nd.IdPhanQuyen where nd.IdND='"+idnd+"'";
            DataTable dt = DataProvider.Instance.ExcuteQuery(sql);
            dgv_dsquyen.DataSource = dt;
        }
        private void cbo_idnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            string loaind = cbo_idnd.SelectedValue.ToString();
            if(loaind.Contains("NV"))
            {
                txt_loaindpq.Text = "Nhân Viên";
            }    
            else

            if(loaind.Contains("QL"))
            {
                txt_loaindpq.Text = "Quản lý";
            }    
            else txt_loaindpq.Text = "Tài xế";
            loadDSquyen(cbo_idnd.SelectedValue.ToString());
        }
        private void btn_themquyen_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "EXEC UPS_themquyen @IdND , @IdQuyen";
                int kt = DataProvider.Instance.ExcuteNonQuery(sql,new object[] { cbo_idnd.SelectedValue.ToString(),cbo_quyen.SelectedValue.ToString()});
                if (kt > 0)
                {
                    MessageBox.Show("Thêm quyền thành công!");
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi:" + ex.Message); }
            loadDSquyen(cbo_idnd.SelectedValue.ToString());

        }
        private void btn_xoaquyen_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = "EXEC UPS_xoaquyen @IdND , @IdQuyen";
                int kt = DataProvider.Instance.ExcuteNonQuery(sql, new object[] { cbo_idnd.SelectedValue.ToString(), cbo_quyen.SelectedValue.ToString() });
                if(kt > 0)
                {
                    MessageBox.Show("Xóa quyền thành công!");
                } 
                    
            }
            catch (Exception ex) { MessageBox.Show("Lỗi:" + ex.Message); }
            loadDSquyen(cbo_idnd.SelectedValue.ToString());
        }





















        #endregion
        #region event
        private void btn_tttaikhoan_Click(object sender, EventArgs e)
        {
            TTTaiKhoan f = new TTTaiKhoan(idnd);
            
            f.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tab_ngdung.SelectedIndex = 6;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tab_ngdung.SelectedIndex = 5;
        }

        private void btn_thongke_Click(object sender, EventArgs e)
        {
            fThongKe f = new fThongKe();
            f.ShowDialog();
        }
        private void fMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Application.Exit();
        }

        #endregion


    }
}
