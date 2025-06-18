using QuanLyBanVe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanVe.DAO
{
    public class ChongoiDAO
    {
        private static ChongoiDAO instance;
        public static int XEWidth = 110;
        public static int XEHeight = 150;
        public static ChongoiDAO Instance
        {
            get { if (instance == null) instance = new ChongoiDAO(); return ChongoiDAO.instance; }
            private set { ChongoiDAO.instance = value; }
        }
        public ChongoiDAO() { }
        public List<Chongoi> LoadChongoiList(string idchuyen)
        {
            List<Chongoi> cxelist = new List<Chongoi>();
            string sql = "select * from ChoNgoi where IdChuyen='"+idchuyen+"'";
            DataTable data = DataProvider.Instance.ExcuteQuery(sql);
            foreach (DataRow items in data.Rows)
            {
                Chongoi cxe = new Chongoi(items);
                cxelist.Add(cxe);
            }

            return cxelist;
        }
    }
}
