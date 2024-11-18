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
        public XE(string id, string name,int sochongoi)
        { 
            this.ID = id;
            this.Name = name;
            this.Sochongoi = sochongoi;
           
        }
        public XE(DataRow row)
        {
            this.ID = row["id"].ToString();
            this.Name=row["name"].ToString();
            this.Sochongoi = (int)row["sochongoi"];
        }
        private string iD;
        private string name;
        private int sochongoi;

        
        public string Name { get => name; set => name = value; }
        public int Sochongoi { get => sochongoi; set => sochongoi = value; }
        public string ID { get => iD; set => iD = value; }
    }
}
