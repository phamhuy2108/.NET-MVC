using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class ChiTietGioHang
    {
        [Key]
        public int mactgiohang { get; set; }
        //[ForeignKey("GioHang")]
        public int makh { get; set; }
        //[ForeignKey("Sach")]
        //public int masach { get; set; }
        public string tenctgiohang { get; set; }
        public string hinhctgiohang { get; set; }
        public double dongia { get; set; }
        public int soluong { get; set; }
        public double tongtien { get { return soluong* dongia; } }
        public ICollection<Sach> DSS { get; set; }
        public GioHang GioHang { get; set; }
        public bool isdeleted { get; set; }
    }
}