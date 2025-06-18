using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanVe.DTO
{
    public class Nhanvien
    {
        string idnhanvien;
        string hoten;
        DateTime ngaysinh;
        string gioitinh;
        string diachi;
        string sodt;
        string luongcoban;

        public Nhanvien(DataRow row)
        {
            Idnhanvien = row["Idnhanvien"].ToString();
            Hoten = row["Hoten"].ToString();
            Ngaysinh = (DateTime)row["Ngaysinh"];
            Gioitinh = row["Gioitinh"].ToString();
            Diachi = row["Diachi"].ToString();
            Sodt = row["Sodt"].ToString();
            Luongcoban = row["Luongcoban"].ToString();
        }

        public string Idnhanvien { get => idnhanvien; set => idnhanvien = value; }
        public string Hoten { get => hoten; set => hoten = value; }
        public DateTime Ngaysinh { get => ngaysinh; set => ngaysinh = value; }
        public string Gioitinh { get => gioitinh; set => gioitinh = value; }
        public string Diachi { get => diachi; set => diachi = value; }
        public string Sodt { get => sodt; set => sodt = value; }
        public string Luongcoban { get => luongcoban; set => luongcoban = value; }
    }
}
