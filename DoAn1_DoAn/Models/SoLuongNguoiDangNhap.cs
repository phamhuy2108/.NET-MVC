using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoAn1_DoAn.Models
{
    public class SoLuongNguoiDangNhap
    {
        public int id { get; set; }
        public int thang { get; set; }
        public int soluongnguoi { get; set; }
        public static List<SoLuongNguoiDangNhap> ListDangNhap()
        {
            List<SoLuongNguoiDangNhap> b = null;
            using (var db = new Context())
            {
                b = db.SoLuongNguoiDangNhap.ToList();
                db.Dispose();
            }
            return b;
        }
    }
}