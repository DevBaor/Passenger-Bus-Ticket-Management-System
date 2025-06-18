using QuanLyBanVe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanVe.DAO
{
   public class XeDAO
    {
        private static XeDAO instance;
        public static int XEWidth = 160;
        public static int XEHeight = 160;
        public static XeDAO Instance {
            get { if (instance == null) instance = new XeDAO();return XeDAO.instance; }
            private set { XeDAO.instance = value; }
        }
        public XeDAO() { }
        public List<TUYENXE> LoadXeList()
        { 
            List<TUYENXE> xelist = new List<TUYENXE>();
            DataTable data = DataProvider.Instance.ExcuteQuery("Select * from TUYENXE");
            foreach (DataRow items in data.Rows)
            {
                TUYENXE xe = new TUYENXE(items);
                xelist.Add(xe);
            }

            return xelist;
        }
        public List<XE> LoadXeList2()
        {
            List<XE> xelist = new List<XE>();
            DataTable data = DataProvider.Instance.ExcuteQuery("Select * from XE");
            foreach (DataRow items in data.Rows)
            {
                XE xe = new XE(items);
                xelist.Add(xe);
            }
            return xelist;
        }
    }
}
