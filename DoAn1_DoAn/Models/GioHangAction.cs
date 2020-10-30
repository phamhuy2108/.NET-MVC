using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class GioHangAction
    {
        public static void addGioHang(int makh,string tengiohang)
        {
            using (var db = new Context())
            {
                db.GioHang.Add(new GioHang { makh =makh , tengiohang = tengiohang });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }
        public static void XoaGioHang(int id)
        {
            using (var db = new Context())
            {
                var giohang = db.GioHang.Find(id);
                db.Entry(giohang).State = EntityState.Deleted;
                db.SaveChanges();
                db.Dispose();
            }
        }
        public static GioHang UpdateGioHang(int makh, string tengiohang)
        {
            GioHang b = null;
            using (var db = new Context())
            {
                b = db.GioHang.Find(makh);
                b.tengiohang = tengiohang;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
        public static GioHang UpdateGioHang1(int makh, double tonggia)
        {
            GioHang b = null;
            using (var db = new Context())
            {
                b = db.GioHang.Find(makh);
                b.tongtien = tonggia;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
    }
}