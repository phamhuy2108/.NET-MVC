using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class HinhThuc
    {
        [Key]
        public int mahinhthuc { get; set; }
        //[ForeignKey("Sach")]
        public string tenhinhthuc { get; set; }
        public ICollection<Sach> DSS { get; set; }
        public static List<HinhThuc> ListHinhThuc()
        {
            List<HinhThuc> b = null;
            using (var db = new Context())
            {
                b = db.HinhThuc.ToList();
                db.Dispose();
            }
            return b;
        }
    }
}