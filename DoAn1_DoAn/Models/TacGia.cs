using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class TacGia
    {
        [Key]
        public int matacgia { get; set; }
        //[ForeignKey("Sach")]
        //public int masach { get; set; }
        public string tentacgia { get; set; }
        public bool isdeleted { get; set; }
        public ICollection<Sach> DSS { get; set; }
        public static List<TacGia> ListTacGia()
        {
            List<TacGia> b = null;
            using (var db = new Context())
            {
                b = db.TacGia.Where(p => p.isdeleted == false).ToList();
                db.Dispose();
            }
            return b;
        }
        public static void addTacGia(string tentacgia)
        {
            using (var db = new Context())
            {
                db.TacGia.Add(new TacGia { tentacgia = tentacgia });
                db.SaveChanges();
                db.Dispose();
            }
            Console.WriteLine("Added Sucessfully");
        }
        public static TacGia UpdateTacGia(int matacgia, string tentacgia)
        {
            TacGia b = null;
            using (var db = new Context())
            {
                b = db.TacGia.Find(matacgia);
                b.tentacgia = tentacgia;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
        public static TacGia UpdateTacGia1(int matl)
        {
            TacGia b = null;
            using (var db = new Context())
            {
                b = db.TacGia.Find(matl);
                b.isdeleted = true;
                db.Entry(b).State = EntityState.Modified;
                db.SaveChanges();
                db.Dispose();
            }
            return b;
        }
        public static TacGia FindTacGia(int id)
        {
            TacGia b = null;
            using (var db = new Context())
            {
                b = db.TacGia.Find(id);
                db.Dispose();
            }
            return b;
        }
    }
}