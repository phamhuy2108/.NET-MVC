using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class LichSuHeThongAction
    {
        public static void addLSHeThong(string doituong, int makhach)
        {
            using (var db = new Context())
            {
                db.LichSuHeThong.Add(new LichSuHeThong { ngay = DateTime.Now, doituong = doituong, thaotac = "Sửa thông tin của người dùng có mã là " + makhach });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }
        public static void addLSHeThong1(string doituong, int makhach)
        {
            using (var db = new Context())
            {
                db.LichSuHeThong.Add(new LichSuHeThong { ngay = DateTime.Now, doituong = doituong, thaotac = "Xóa thông tin của người dùng có mã là " + makhach });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }
        public static void addLSHeThong2(string doituong)
        {
            using (var db = new Context())
            {
                db.LichSuHeThong.Add(new LichSuHeThong { ngay = DateTime.Now, doituong = doituong, thaotac = "Đã đăng nhập" });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }

        public static List<LichSuHeThong> ListLichSuHeThong()
        {
            List<LichSuHeThong> b = null;
            using (var db = new Context())
            {
                b=db.LichSuHeThong.ToList();
                db.Dispose();   
            }
            return b;
        }
    }
}