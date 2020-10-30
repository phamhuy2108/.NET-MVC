using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class KhachHang
    {
        [Key]
        public int makh { get; set; }
        //[ForeignKey("GioHang")]
        //public int magiohang { get { return makh; } }
        public string ho { get; set; }
        public string ten { get; set; }
        public string diachi { get; set; }
        public string sdt { get; set; }
        public string email { get; set; }
        public string matkhau { get; set; }
        public string nhaplaimatkhau { get; set; }
        public string quyen { get; set; }
        public bool isdeleted { get; set; }
        public virtual GioHang GioHang { get; set; }
    }
}