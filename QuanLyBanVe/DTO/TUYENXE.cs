using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace QuanLyBanVe.DTO
{
    public class TUYENXE
    {
        public TUYENXE(string idtuyen, string tentuyen,string DiaDiemdi,string DiaDiemden)
        { 
            this.IdTuyen=idtuyen;
            this.Tentuyen=tentuyen;
            this.Diadiemdi = DiaDiemdi;
            this.Diadiemden = DiaDiemden;
        }
        public TUYENXE(DataRow row)
        {
            this.IdTuyen = row["idTuyen"].ToString();
            this.Tentuyen = row["TenTuyen"].ToString();
            this.Diadiemdi = row["IdDiaDiemDi"].ToString();
            this.Diadiemden = row["IdDiaDiemDen"].ToString();

        }
       
        private string idTuyen;
        private string tentuyen;
        private string diadiemdi;
        private string diadiemden;

        public string IdTuyen { get => idTuyen; set => idTuyen = value; }
        public string Tentuyen { get => tentuyen; set => tentuyen = value; }
        public string Diadiemdi { get => diadiemdi; set => diadiemdi = value; }
        public string Diadiemden { get => diadiemden; set => diadiemden = value; }
    }
}
