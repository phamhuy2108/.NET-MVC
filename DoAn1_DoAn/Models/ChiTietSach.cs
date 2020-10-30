using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class ChiTietSach
    {
        [Key]
        //public int machitietsach { get; set; }
        //[ForeignKey("Sach")]
        [ForeignKey("Sach")]
        public int masach { get;set;  }
        public string tenchitiet { get; set; }
        public string chitiet { get; set; }
        public virtual Sach Sach { get; set; }
    }
}