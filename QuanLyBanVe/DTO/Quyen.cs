using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanVe.DTO
{
    public class Quyen
    {
        public string idphanquyen;

        public Quyen(DataRow row)
        {
            this.idphanquyen = row["idphanquyen"].ToString();
        }
    }
}
