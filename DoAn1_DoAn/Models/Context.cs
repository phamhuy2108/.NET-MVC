using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DoAn1_DoAn.Models
{
    public class Context : DbContext
    {
        public Context() : base("NhaSach")
        {
            //string databasename = "NhaSach";
            //this.Database.Connection.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=" + databasename + ";Trusted_Connection=Yes";
        }
        public DbSet<ChiTietGioHang> ChiTietGioHang { get; set; }
        public DbSet<ChiTietSach> ChiTietSach { get; set; }
        public DbSet<HinhThuc> HinhThuc { get; set; }
        public DbSet<GioHang> GioHang { get; set; }
        public DbSet<KhachHang> KhachHang { get; set; }
        public DbSet<NhaCungCap> NhaCungCap { get; set; }
        public DbSet<NhaXuatBan> NhaXuatBan { get; set; }
        public DbSet<Sach> Sach { get; set; }
        public DbSet<TheLoai> TheLoai { get; set; }
        public DbSet<TacGia> TacGia  { get; set; }
        public DbSet<LichSuHeThong> LichSuHeThong { get; set; }
        public DbSet<LichSuMuaSach> LichSuMuaSach { get; set; }
        public DbSet<SoLuongNguoiDangNhap> SoLuongNguoiDangNhap { get; set; }
        public DbSet<SoLuongSachDuocBanRa> SoLuongSachDuocBanRa { get; set; }
    }
}