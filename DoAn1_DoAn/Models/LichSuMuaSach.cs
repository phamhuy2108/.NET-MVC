using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class LichSuMuaSach
    {
        public int id { get; set; }
        public DateTime ngay { get; set; }
        public string doituong { get; set; }
        public string thaotac { get; set; }
        public int soluong { get; set; }
        public int masach { get; set; }
    }
}