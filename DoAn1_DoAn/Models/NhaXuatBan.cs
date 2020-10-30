using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class NhaXuatBan
    {
        [Key]
        public int manxb { get; set; }
        //[ForeignKey("Sach")]
        //public int masach { get; set; }
        public string tennxb { get; set; }
        public bool isdeleted { get; set; }
        public ICollection<Sach> DSS { get; set; }
        public static List<NhaXuatBan> ListNXB()
        {
            List<NhaXuatBan> b = null;
            using (var db = new Context())
            {
                b = db.NhaXuatBan.Where(p => p.isdeleted == false).ToList();
                db.Dispose();
            }
            return b;
        }
        public static void addNhaXuatBan(string tennxb)
        {
            using (var db = new Context())
            {
                db.NhaXuatBan.Add(new NhaXuatBan { tennxb = tennxb });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }
        public static NhaXuatBan UpdateNhaXuatBan(int manxb, string tennxb)
        {
            NhaXuatBan b = null;
            using (var db = new Context())
            {
                b = db.NhaXuatBan.Find(manxb);
                b.tennxb = tennxb;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
        public static NhaXuatBan UpdateNhaXuatBan1(int matl)
        {
            NhaXuatBan b = null;
            using (var db = new Context())
            {
                b = db.NhaXuatBan.Find(matl);
                b.isdeleted = true;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
        public static NhaXuatBan FindNhaXuatBan(int id)
        {
            NhaXuatBan b = null;
            using (var db = new Context())
            {
                b = db.NhaXuatBan.Find(id);
                db.Dispose();
            }
            return b;
        }
    }
}