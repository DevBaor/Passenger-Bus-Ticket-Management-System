using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanVe.DAO
{
    internal class AccountDAO
    {
        private static AccountDAO instance;
        public static AccountDAO Instance
        {
            get { if (instance == null)  instance=new AccountDAO();return instance ; }
            private set { instance = value ; }
        }
        private AccountDAO() { }
        public bool Login(string Username, string Password)
        {
            string query = "sp_Login @idnguoidung , @pass";
            DataTable result = DataProvider.Instance.ExcuteQuery(query, new object[] { Username,Password});
            return result.Rows.Count>0;
        }
    }
    
}
