namespace DoAn1_DoAn.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChiTietGioHangs",
                c => new
                    {
                        mactgiohang = c.Int(nullable: false, identity: true),
                        makh = c.Int(nullable: false),
                        tenctgiohang = c.String(),
                        hinhctgiohang = c.String(),
                        dongia = c.Double(nullable: false),
                        soluong = c.Int(nullable: false),
                        isdeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.mactgiohang)
                .ForeignKey("dbo.GioHangs", t => t.makh, cascadeDelete: true)
                .Index(t => t.makh);
            
            CreateTable(
                "dbo.Saches",
                c => new
                    {
                        masach = c.Int(nullable: false, identity: true),
                        mancc = c.Int(nullable: false),
                        mahinhthuc = c.Int(nullable: false),
                        matacgia = c.Int(nullable: false),
                        manxb = c.Int(nullable: false),
                        mantl = c.Int(nullable: false),
                        tensach = c.String(),
                        gia = c.Double(nullable: false),
                        hinh = c.String(),
                        isdeleted = c.Boolean(nullable: false),
                        TheLoai_matl = c.Int(),
                    })
                .PrimaryKey(t => t.masach)
                .ForeignKey("dbo.HinhThucs", t => t.mahinhthuc, cascadeDelete: true)
                .ForeignKey("dbo.NhaCungCaps", t => t.mancc, cascadeDelete: true)
                .ForeignKey("dbo.NhaXuatBans", t => t.manxb, cascadeDelete: true)
                .ForeignKey("dbo.TacGias", t => t.matacgia, cascadeDelete: true)
                .ForeignKey("dbo.TheLoais", t => t.TheLoai_matl)
                .Index(t => t.mancc)
                .Index(t => t.mahinhthuc)
                .Index(t => t.matacgia)
                .Index(t => t.manxb)
                .Index(t => t.TheLoai_matl);
            
            CreateTable(
                "dbo.ChiTietSaches",
                c => new
                    {
                        masach = c.Int(nullable: false),
                        tenchitiet = c.String(),
                        chitiet = c.String(),
                    })
                .PrimaryKey(t => t.masach)
                .ForeignKey("dbo.Saches", t => t.masach)
                .Index(t => t.masach);
            
            CreateTable(
                "dbo.HinhThucs",
                c => new
                    {
                        mahinhthuc = c.Int(nullable: false, identity: true),
                        tenhinhthuc = c.String(),
                    })
                .PrimaryKey(t => t.mahinhthuc);
            
            CreateTable(
                "dbo.NhaCungCaps",
                c => new
                    {
                        mancc = c.Int(nullable: false, identity: true),
                        tenncc = c.String(),
                        isdeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.mancc);
            
            CreateTable(
                "dbo.NhaXuatBans",
                c => new
                    {
                        manxb = c.Int(nullable: false, identity: true),
                        tennxb = c.String(),
                        isdeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.manxb);
            
            CreateTable(
                "dbo.TacGias",
                c => new
                    {
                        matacgia = c.Int(nullable: false, identity: true),
                        tentacgia = c.String(),
                        isdeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.matacgia);
            
            CreateTable(
                "dbo.GioHangs",
                c => new
                    {
                        makh = c.Int(nullable: false),
                        tengiohang = c.String(),
                        tongtien = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.makh)
                .ForeignKey("dbo.KhachHangs", t => t.makh)
                .Index(t => t.makh);
            
            CreateTable(
                "dbo.KhachHangs",
                c => new
                    {
                        makh = c.Int(nullable: false, identity: true),
                        ho = c.String(),
                        ten = c.String(),
                        diachi = c.String(),
                        sdt = c.String(),
                        email = c.String(),
                        matkhau = c.String(),
                        nhaplaimatkhau = c.String(),
                        quyen = c.String(),
                        isdeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.makh);
            
            CreateTable(
                "dbo.LichSuHeThongs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ngay = c.DateTime(nullable: false),
                        doituong = c.String(),
                        thaotac = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.LichSuMuaSaches",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ngay = c.DateTime(nullable: false),
                        doituong = c.String(),
                        thaotac = c.String(),
                        soluong = c.Int(nullable: false),
                        masach = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.SoLuongNguoiDangNhaps",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        thang = c.Int(nullable: false),
                        soluongnguoi = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.SoLuongSachDuocBanRas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        thang = c.Int(nullable: false),
                        soluongban = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.TheLoais",
                c => new
                    {
                        matl = c.Int(nullable: false, identity: true),
                        tentheloai = c.String(),
                        isdeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.matl);
            
            CreateTable(
                "dbo.SachChiTietGioHangs",
                c => new
                    {
                        Sach_masach = c.Int(nullable: false),
                        ChiTietGioHang_mactgiohang = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Sach_masach, t.ChiTietGioHang_mactgiohang })
                .ForeignKey("dbo.Saches", t => t.Sach_masach, cascadeDelete: true)
                .ForeignKey("dbo.ChiTietGioHangs", t => t.ChiTietGioHang_mactgiohang, cascadeDelete: true)
                .Index(t => t.Sach_masach)
                .Index(t => t.ChiTietGioHang_mactgiohang);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Saches", "TheLoai_matl", "dbo.TheLoais");
            DropForeignKey("dbo.GioHangs", "makh", "dbo.KhachHangs");
            DropForeignKey("dbo.ChiTietGioHangs", "makh", "dbo.GioHangs");
            DropForeignKey("dbo.Saches", "matacgia", "dbo.TacGias");
            DropForeignKey("dbo.Saches", "manxb", "dbo.NhaXuatBans");
            DropForeignKey("dbo.Saches", "mancc", "dbo.NhaCungCaps");
            DropForeignKey("dbo.Saches", "mahinhthuc", "dbo.HinhThucs");
            DropForeignKey("dbo.SachChiTietGioHangs", "ChiTietGioHang_mactgiohang", "dbo.ChiTietGioHangs");
            DropForeignKey("dbo.SachChiTietGioHangs", "Sach_masach", "dbo.Saches");
            DropForeignKey("dbo.ChiTietSaches", "masach", "dbo.Saches");
            DropIndex("dbo.SachChiTietGioHangs", new[] { "ChiTietGioHang_mactgiohang" });
            DropIndex("dbo.SachChiTietGioHangs", new[] { "Sach_masach" });
            DropIndex("dbo.GioHangs", new[] { "makh" });
            DropIndex("dbo.ChiTietSaches", new[] { "masach" });
            DropIndex("dbo.Saches", new[] { "TheLoai_matl" });
            DropIndex("dbo.Saches", new[] { "manxb" });
            DropIndex("dbo.Saches", new[] { "matacgia" });
            DropIndex("dbo.Saches", new[] { "mahinhthuc" });
            DropIndex("dbo.Saches", new[] { "mancc" });
            DropIndex("dbo.ChiTietGioHangs", new[] { "makh" });
            DropTable("dbo.SachChiTietGioHangs");
            DropTable("dbo.TheLoais");
            DropTable("dbo.SoLuongSachDuocBanRas");
            DropTable("dbo.SoLuongNguoiDangNhaps");
            DropTable("dbo.LichSuMuaSaches");
            DropTable("dbo.LichSuHeThongs");
            DropTable("dbo.KhachHangs");
            DropTable("dbo.GioHangs");
            DropTable("dbo.TacGias");
            DropTable("dbo.NhaXuatBans");
            DropTable("dbo.NhaCungCaps");
            DropTable("dbo.HinhThucs");
            DropTable("dbo.ChiTietSaches");
            DropTable("dbo.Saches");
            DropTable("dbo.ChiTietGioHangs");
        }
    }
}
