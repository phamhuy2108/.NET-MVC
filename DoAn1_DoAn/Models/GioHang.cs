using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class GioHang
    {
        [Key]
        //public int magiohang { get; set; }
        [ForeignKey("KhachHang")]
        public int makh { get; set; }
        //[ForeignKey("ChiTietGioHang")]
        //public int mactgiohang { get; set; }
        //public int makh { get; set; }
        public string tengiohang { get; set; }
        public double tongtien { get; set;}
        public virtual KhachHang KhachHang { get; set; }
        public ICollection<ChiTietGioHang> DSCTGH { get; set; }
    }
}