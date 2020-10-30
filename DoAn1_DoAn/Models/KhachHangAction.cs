using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class KhachHangAction
    {
        public static void addKhachHang(string ten, string ho, string diachi, string sdt, string email, string matkhau, string nhaplaimatkhau)
        {
            using (var db = new Context())
            {
                db.KhachHang.Add(new KhachHang { ho = ten, ten = ho, diachi= diachi,sdt = sdt, email =email, matkhau = matkhau, nhaplaimatkhau=nhaplaimatkhau,quyen="user" });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }
        public static List<KhachHang> ListKhachHang()
        {
            List<KhachHang> b = null;
            using (var db = new Context())
            {
                var a = db.KhachHang.Where(s => s.isdeleted == false).ToList();
                b = a;
                db.Dispose();
            }
            return b;
        }
        public static KhachHang UpdateKhachHang(int makh,string ho,string ten,string diachi,string sdt,string email, string matkhau,string nhaplaimatkhau,string quyen)
        {
            KhachHang b = null;
            using (var db = new Context())
            {
                b = db.KhachHang.Find(makh);
                b.ho = ho;
                b.ten = ten;
                b.diachi = diachi;
                b.sdt = sdt;
                b.email = email;
                b.matkhau = matkhau;
                b.nhaplaimatkhau = nhaplaimatkhau;
                b.quyen = quyen;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
        public static KhachHang FindKhach(int makh)
        {
            KhachHang b = null;
            using (var db = new Context())
            {
                b = db.KhachHang.Find(makh);
                db.Dispose();
            }
            return b;
        }
        public static KhachHang XoaKhachHang(int makh)
        {
            KhachHang b = null;
            using (var db = new Context())
            {
                b = db.KhachHang.Find(makh);
                b.isdeleted = true;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
    }
}