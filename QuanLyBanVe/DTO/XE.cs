using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanVe.DTO
{
    public class XE
    {
        private string soxe;
        private string hieuXe;
        private int sochongoi;
        private string tinhtrang;
        public string Soxe { get => soxe; set => soxe = value; }
        public string HieuXe { get => hieuXe; set => hieuXe = value; }
        public int Sochongoi { get => sochongoi; set => sochongoi = value; }
        public string Tinhtrang { get => tinhtrang; set => tinhtrang = value; }
        public XE(string soxe, string hieuxe, int sochongoi, string tinhtrang)
        {
            this.Soxe = soxe;
            this.HieuXe = hieuxe;
            this.Sochongoi = sochongoi;
            this.Tinhtrang = tinhtrang;
        }
        public XE(DataRow row)
        {
            this.Soxe = row["So_Xe"].ToString();
            this.HieuXe = row["Hieu_Xe"].ToString();
            this.Sochongoi = int.Parse(row["So_Cho_Ngoi"].ToString());
            this.Tinhtrang = row["TinhTrang"].ToString();
        }
    }
}
