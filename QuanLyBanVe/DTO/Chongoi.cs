using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanVe.DTO
{
    public class Chongoi
    {
        string idchuyen;
        string so_xe;
        string tenchongoi;
        string tinhtrang;

        public string Idchuyen { get => idchuyen; set => idchuyen = value; }
        public string So_xe { get => so_xe; set => so_xe = value; }
        public string Tenchongoi { get => tenchongoi; set => tenchongoi = value; }
        public string Tinhtrang { get => tinhtrang; set => tinhtrang = value; }

        public Chongoi(DataRow row)
        {
            this.idchuyen= row["idchuyen"].ToString();
            this.so_xe = row["so_xe"].ToString();
            this.tenchongoi = row["tenchongoi"].ToString();
            this.tinhtrang = row["tinhtrang"].ToString();
        }
    }
}
