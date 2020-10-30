using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class Sach
    {
        [Key]

        public int masach { get; set; }
        //[ForeignKey("NhaCungCap")]
        public int mancc { get; set; }
        //[ForeignKey("HinhThuc")]
        public int mahinhthuc { get; set; }
        //[ForeignKey("TacGia")]
        public int matacgia { get; set; }
        //[ForeignKey("NhaXuatBan")]
        public int manxb { get; set; }
        public int mantl { get; set; }
        public string tensach { get; set; }
        public double gia { get; set; }
        public string hinh { get; set; }
        public bool isdeleted { get; set; }
        //public int mactsach { get; set; }
        public NhaCungCap NhaCungCap { get; set; }
        public NhaXuatBan NhaXuatBan { get; set; }
        public HinhThuc HinhThuc { get; set; }
        public TacGia TacGia { get; set; }
        public ICollection<ChiTietGioHang> DSCTGH { get; set; }
        public virtual ChiTietSach ChiTietSach { get; set; }
    }
}