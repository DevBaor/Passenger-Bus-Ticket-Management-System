using QuanLyBanVe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanVe.DAO
{
    public class ChuyenxeDAO
    {
        private static ChuyenxeDAO instance;
        public static int XEWidth = 100;
        public static int XEHeight = 120;
        public static ChuyenxeDAO Instance
        {
            get { if (instance == null) instance = new ChuyenxeDAO(); return ChuyenxeDAO.instance; }
            private set { ChuyenxeDAO.instance = value; }
        }
        public ChuyenxeDAO() { }
        public List<Chuyenxe> LoadCXeList(string idtuyen,DateTime ngaydi)
        {
            List<Chuyenxe> cxelist = new List<Chuyenxe>();
            string sql = "exec TimChuyenXe @IdTuyen , @NgayDi";
            DataTable data = DataProvider.Instance.ExcuteQuery(sql,new object[] {idtuyen,ngaydi});
            foreach (DataRow items in data.Rows)
            {
                Chuyenxe cxe = new Chuyenxe(items);
                cxelist.Add(cxe);
            }

            return cxelist;
        }
    }
}
