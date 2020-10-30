using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class SoLuongSachDuocBanRa
    { 
        public int id { get; set; }
        public int thang { get; set; }
        public int soluongban { get; set; } 
    }
}