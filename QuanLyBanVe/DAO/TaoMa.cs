using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanVe.DAO
{
    public class TaoMa
    {
        private static TaoMa instance;
        public static TaoMa Instance
        {
            get { if (instance == null) instance = new TaoMa(); return instance; }
            private set { instance = value; }
        }
        private TaoMa() { }
        public string GetMaveByGhengoi(string idch,string soxe,string tencn)
        {
            string sql = "select IdVe from Ve where IdChuyen='"+idch+"' and So_Xe='"+soxe+"' and TenChoNgoi='"+tencn+"'";
            string mave = (string)DataProvider.Instance.ExcuteScalar(sql);
            return mave;
        }
        public string TaoMaKH()
        {
            string sql = "DECLARE @IDKH VARCHAR(20) SET @IDKH=dbo.TAOMAKH() select @IDKH";
            string mahd = (string)DataProvider.Instance.ExcuteScalar(sql);
            return mahd;
        }
        public string TaoMaHD()
        {
            string sql = "DECLARE @IDHD VARCHAR(20) SET @IDHD=dbo.TAOMAHD() select @IDHD";
            string mahd= (string)DataProvider.Instance.ExcuteScalar(sql);
            return mahd;
        }
        public string TaoMaVE()
        {
            string sql = "DECLARE @IDVE VARCHAR(20) SET @IDVE=dbo.TAOMAVE() select @IDVE";
            string mave = (string)DataProvider.Instance.ExcuteScalar(sql);
            return mave;
        }
    }
}
