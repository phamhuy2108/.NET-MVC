using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class LichSuHeThong
    {
        public int id { get; set; }
        public DateTime ngay { get; set; }
        public string doituong { get; set; }
        public string thaotac { get; set; }
    }
}