using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class ChiTietGioHangAction
    {
        public static void addCTGioHang(int makh,string tenctgiohang,string hinhctgiohang,double dongia,int soluong)
        {
            using (var db = new Context())
            {
                db.ChiTietGioHang.Add(new ChiTietGioHang { makh=makh, tenctgiohang=tenctgiohang,hinhctgiohang=hinhctgiohang, dongia=dongia,soluong=soluong });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }
        public static List<ChiTietGioHang> ListCTGioHang()
        {
            List<ChiTietGioHang> b = null;
            using (var db = new Context())
            {
                var c = db.ChiTietGioHang.Where(s => s.isdeleted == false).ToList();
                b = c;
                db.Dispose();
            }
            return b;
        }
        public static List<ChiTietGioHang> ListCTGioHang1()
        {
            List<ChiTietGioHang> b = null;
            using (var db = new Context())
            {
                b = db.ChiTietGioHang.ToList();
                db.Dispose();
            }
            return b;
        }
        public static ChiTietGioHang UpdateCTGioHang(int id, int soluong)
        {
            ChiTietGioHang b = null;
            using (var db = new Context())
            {
                b = db.ChiTietGioHang.Find(id);
                b.soluong = soluong;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
        public static void XoaChiTietGioHang(int id)
        {
            using (var db = new Context())
            {
                var ctgiohang = db.ChiTietGioHang.Find(id);
                db.Entry(ctgiohang).State = EntityState.Deleted;
                db.SaveChanges();
                db.Dispose();
            }
        }
        public static ChiTietGioHang DaTonTai(int id, int soluong)
        {
            ChiTietGioHang b = null;
            using (var db = new Context())
            {
                b = db.ChiTietGioHang.Find(id);
                b.soluong = b.soluong+soluong;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
        public static ChiTietGioHang Xoa(int id)
        {
            ChiTietGioHang b = null;
            using (var db = new Context())
            {
                b = db.ChiTietGioHang.Find(id);
                b.isdeleted = true;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
        public static ChiTietGioHang FindCTGioHang(int id)
        {
            ChiTietGioHang b = null;
            using (var db = new Context())
            {
                b = db.ChiTietGioHang.Find(id);
                db.Dispose();
            }
            return b;
        }
    }
}