using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class NhaCungCap
    {
        [Key]
        public int mancc { get; set; }
        //[ForeignKey("Sach")]
        //public int masach { get; set; }
        public string tenncc { get; set; }
        public bool isdeleted { get; set; }
        public ICollection<Sach> DSS { get; set; }
        public static List<NhaCungCap> ListNhaCungCap()
        {
            List<NhaCungCap> b = null;
            using (var db = new Context())
            {
                b = db.NhaCungCap.Where(p => p.isdeleted == false).ToList();
                db.Dispose();
            }
            return b;
        }
        public static void addNhaCungCap(string tenncc)
        {
            using (var db = new Context())
            {
                db.NhaCungCap.Add(new NhaCungCap { tenncc = tenncc });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }
        public static NhaCungCap UpdateNhaCungCap(int mancc, string tenncc)
        {
            NhaCungCap b = null;
            using (var db = new Context())
            {
                b = db.NhaCungCap.Find(mancc);
                b.tenncc = tenncc;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
        public static NhaCungCap UpdateNhaCungCap1(int mancc)
        {
            NhaCungCap b = null;
            using (var db = new Context())
            {
                b = db.NhaCungCap.Find(mancc);
                b.isdeleted = true;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
        public static NhaCungCap FindNhaCungCap(int id)
        {
            NhaCungCap b = null;
            using (var db = new Context())
            {
                b = db.NhaCungCap.Find(id);
                db.Dispose();
            }
            return b;
        }
    }  
}