using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class ChiTietSachAction
    {
        public static void addCTSach(int masach, string tenchitiet, string chitiet)
        {
            using (var db = new Context())
            {
                db.ChiTietSach.Add(new ChiTietSach { masach=masach,tenchitiet=tenchitiet,chitiet=chitiet });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }
        public static void addCTSach1 (int masach)
        {
            using (var db = new Context())
            {
                db.ChiTietSach.Add(new ChiTietSach { masach = masach });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }
        public static ChiTietSach FindCTSach(int id)
        {
            ChiTietSach b = null;
            using (var db = new Context())
            {
                b = db.ChiTietSach.Find(id);
                db.Dispose();
            }
            return b;
        }
        public static ChiTietSach UpdateCTSach(int masach, string tenchitiet,string chitiet)
        {
            ChiTietSach b = null;
            using (var db = new Context())
            {
                b = db.ChiTietSach.Find(masach);
                b.tenchitiet = tenchitiet;
                b.chitiet = chitiet;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
    }
}