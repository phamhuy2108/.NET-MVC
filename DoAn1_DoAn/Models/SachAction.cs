using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class SachAction
    {
        public static void addSach(int mancc, int mahinhthuc, int matacgia, int manxb, int matl, string tensach,double gia,string hinh)
        {
            using (var db = new Context())
            {
                db.Sach.Add(new Sach { mancc = mancc, mahinhthuc = mahinhthuc, matacgia = matacgia, manxb = manxb, mantl = matl, tensach = tensach,gia=gia,hinh=hinh, isdeleted = false });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }
        public static List<Sach> ListSach()
        {
            List<Sach> b = null;
            using (var db = new Context())
            {
                var sach = (from c in db.Sach
                            where c.isdeleted == false
                            select c);
                b = sach.ToList();
                db.Dispose();
            }
            return b;
        }
        public static Sach FindSach(int id)
        {
            Sach b = null;
            using (var db = new Context())
            {
                b = db.Sach.Find(id);
                db.Dispose();
            }
            return b;
        }
        public static Sach UpdateSach(int masach,int mancc, int mahinhthuc,int matacgia,int manxb,int matl,string tensach,double gia,string hinh)
        {
            Sach b = null;
            using (var db = new Context())
            {
                b = db.Sach.Find(masach);
                b.mancc = mancc;
                b.mahinhthuc = mahinhthuc;
                b.matacgia = matacgia;
                b.manxb = manxb;
                b.mantl = matl; //saiten
                b.tensach = tensach;
                b.gia = gia;
                b.hinh = hinh;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
        public static Sach XoaSach(int masach)
        {
            Sach b = null;
            using (var db = new Context())
            {
                b = db.Sach.Find(masach);
                b.isdeleted=true;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
    }
}