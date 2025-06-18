using QuanLyBanVe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanVe.DAO
{
    public class QuyenDAO
    {
        private static QuyenDAO instance;
        public static QuyenDAO Instance
        {
            get { if (instance == null) instance = new QuyenDAO(); return instance; }
            private set { instance = value; }
        }
        private QuyenDAO() { }
        public List<Quyen> LoadListQuyen(string idnd)
        {
            List<Quyen> xelist = new List<Quyen>();
            DataTable data = DataProvider.Instance.ExcuteQuery("select IdPhanQuyen from NguoiDungPhanQuyen where IdND='"+idnd+"'");
            foreach (DataRow items in data.Rows)
            {
                Quyen xe = new Quyen(items);
                xelist.Add(xe);
            }

            return xelist;
        }
    }
}
