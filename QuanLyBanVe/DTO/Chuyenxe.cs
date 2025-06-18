using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanVe.DTO
{
    public class Chuyenxe
    {
        string idchuyen;
        string idtuyen;
        DateTime ngaydi;
        string gio;
        string soxe;
        string idtaixe;
        int giave;
        string tinhtrang;

        public string Idchuyen { get => idchuyen; set => idchuyen = value; }
        public string Idtuyen { get => idtuyen; set => idtuyen = value; }
        public DateTime Ngaydi { get => ngaydi; set => ngaydi = value; }
        public string Gio { get => gio; set => gio = value; }
        public string Soxe { get => soxe; set => soxe = value; }
        public string Idtaixe { get => idtaixe; set => idtaixe = value; }
        public int Giave { get => giave; set => giave = value; }
        public string Tinhtrang { get => tinhtrang; set => tinhtrang = value; }

        public Chuyenxe(DataRow row)
        {
            this.idtuyen= row["idtuyen"].ToString();
            this.idchuyen = row["idchuyen"].ToString();
            this.ngaydi = (DateTime)row["ngaydi"];
            this.gio = row["gio"].ToString();
            this.soxe = row["so_xe"].ToString();
            this.idtaixe = row["idtaixe"].ToString();
            this.giave = (int)row["giave"];
            this.tinhtrang = row["tinhtrang"].ToString();
        }

    }
}
