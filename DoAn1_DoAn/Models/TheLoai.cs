using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class TheLoai
    {
        [Key]
        public int matl { get; set; }
        //[ForeignKey("Sach")]
        //public int masach { get; set; }
        public string tentheloai { get; set; }
        public bool isdeleted { get; set; }
        public ICollection<Sach> DSS { get; set; }
        public static List<TheLoai> ListTheLoai()
        {
            List<TheLoai> b = null;
            using (var db = new Context())
            {
                b = db.TheLoai.Where(p => p.isdeleted == false).ToList();
                db.Dispose();
            }
            return b;
        }
        public static void addTheLoai(string tentheloai)
        {
            using (var db = new Context())
            {
                db.TheLoai.Add(new TheLoai { tentheloai=tentheloai });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }
        public static TheLoai UpdateTheLoai(int matl,string tentheloai)
        {
            TheLoai b = null;
            using (var db = new Context())
            {
                b = db.TheLoai.Find(matl);
                b.tentheloai = tentheloai;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
        public static TheLoai UpdateTheLoai1(int matl)
        {
            TheLoai b = null;
            using (var db = new Context())
            {
                b = db.TheLoai.Find(matl);
                b.isdeleted = true;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
        public static TheLoai FindTheLoai(int id)
        {
            TheLoai b = null;
            using (var db = new Context())
            {
                b = db.TheLoai.Find(id);
                db.Dispose();
            }
            return b;
        }
    }
}