using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class LichSuMuaSachAction
    {
        public static void addLSMuaSach(string doituong,string tensach, int masach,int soluong)
        {
            using (var db = new Context())
            {
                db.LichSuMuaSach.Add(new LichSuMuaSach { ngay = DateTime.Now, doituong = doituong, thaotac = "Đã mua cuốn sách có tên là" + tensach, masach = masach,soluong=soluong });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }
        public static List<LichSuMuaSach> ListLichSuMuaSach()
        {
            List<LichSuMuaSach> b = null;
            using (var db = new Context())
            {
                b = db.LichSuMuaSach.ToList();
                db.Dispose();
            }
            return b;
        }
    }
}