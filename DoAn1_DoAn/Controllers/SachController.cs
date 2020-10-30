using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn1_DoAn.Models;
using System.Text.RegularExpressions;
using System.Text;
using ClosedXML.Excel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DoAn1_DoAn.Controllers
{
    public class SachController : Controller
    {
        // GET: Sach
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            if (checkLogin() == false)
            {
                return View();
            }
            else
            {
                return Redirect("~/Sach/TrangChu");
            }
        }
        [HttpPost]
        public ActionResult DangKy(string ho, string ten, string diachi, string sdt, string email, string matkhau, string nhaplaimatkhau)
        {
            using (var db = new Context())
            {
                if (checkLogin() == false)
                {
                    var a = db.KhachHang.Where(p => p.email == email).ToList();
                    if (a.Count() == 0)
                    {
                        //GioHangAction.addGioHang(ten);
                        KhachHangAction.addKhachHang(ho, ten, diachi, sdt, email, matkhau, nhaplaimatkhau);
                        return Redirect("~/Sach/DangNhap");
                    }
                    else
                    {
                        TempData["LoiDangKy"] = "Tai khoan da ton tai";
                        return Redirect("~/Sach/DangKy");
                    }
                }
                else
                { return Redirect("~/Sach/TrangChu"); }
            }
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            if (checkLogin() == false)
            {
                //using (var db = new Context())
                //{
                //    var login = (from a in db.User
                //                 select a).ToList();

                //    if (checkLogin() == true)
                //    {
                //        return RedirectToAction("List", "Home");
                //    }
                //    else
                //    {
                return View();
            }
            else { return Redirect("~/Sach/TrangChu"); }
            //    }
            //}
        }
        [HttpPost]
        public ActionResult DangNhap(string email, string matkhau)
        {
            using (var db = new Context())
            {
                var login = (from a in db.KhachHang
                             where a.email == email && a.matkhau == matkhau && a.isdeleted == false
                             select a).ToList();
                //var loginlock = (from a in db.KhachHang
                //                 where a.email == email && a.matkhau == matkhau && a.isdeleted == "locked"
                //                 select a).ToList();
                var quyen = (from b in db.KhachHang
                             where b.email == email && b.matkhau == matkhau
                             select b.quyen).FirstOrDefault();
                var id = (from b in db.KhachHang
                          where b.email == email && b.matkhau == matkhau && b.isdeleted == false
                          select b.makh).FirstOrDefault();
                List<KhachHang> listlogin = login;
                if (login.Count() != 0)
                {
                    Session.Add("id", id.ToString());
                    Session.Add("taikhoan", email);
                    Session.Add("matkhau", matkhau);
                    Session.Add("quyen", quyen.ToString());
                    //LogAction.addLog2((Session["taikhoan"].ToString()), id);
                    TempData["ThongBao"] = "Đăng nhập thànnh cônng"; //ViewBag không return được view khác
                    LichSuHeThongAction.addLSHeThong2(email);
                    if(quyen.ToString()=="user")
                    {
                    return RedirectToAction("TrangChu", "Sach");
                    }
                    else
                    {
                        return RedirectToAction("QuanLy", "Sach");
                    }
                    //return View();
                }
                else
                {
                    TempData["LoiDangNhap"] = "Sai ten dang nhap hoac mat khau";
                    return Redirect("~/Sach/DangNhap");
                }
            }
        }
        public ActionResult TrangChu()
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var hot = (from a in db.Sach
                           where a.mantl == 1 /*&& a.isdeleted == "active"*/
                           select a).ToList();
                var tamly = (from a in db.Sach
                             where a.mantl == 2 /*&& a.isdeleted == "active"*/
                             select a).ToList();
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                ViewBag.ListHot = hot;
                ViewBag.ListTamLy = tamly;
                return View();
            }
        }
        [HttpGet]
        public ActionResult ThemSach()
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                ViewBag.ListNCC = NhaCungCap.ListNhaCungCap();
                ViewBag.ListHinhThuc = HinhThuc.ListHinhThuc();
                ViewBag.ListTacGia = TacGia.ListTacGia();
                ViewBag.ListNXB = NhaXuatBan.ListNXB();
                ViewBag.ListTheLoai = TheLoai.ListTheLoai();
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                return View();
            }
        }
        [HttpPost]
        public ActionResult ThemSach(int mancc, int mahinhthuc, int matacgia, int manxb, int matl, string tensach, double gia, HttpPostedFileBase myfile)
        {
            try
            {
                string _path = "";
                if (myfile.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(myfile.FileName);
                    _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    myfile.SaveAs(_path);
                    SachAction.addSach(mancc, mahinhthuc, matacgia, manxb, matl, tensach, gia, _FileName);
                }
                return RedirectToAction("QuanLySanPham", "Sach");
            }
            catch
            {
                return RedirectToAction("ThemSach", "Sach");
            }
        }
        public ActionResult QuanLySanPham()
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                ViewBag.ListSanPham = SachAction.ListSach();
                var d = (from s in db.Sach
                         join e in db.NhaXuatBan on s.manxb equals e.manxb
                         join f in db.NhaCungCap on s.mancc equals f.mancc
                         join v in db.HinhThuc on s.mahinhthuc equals v.mahinhthuc
                         join r in db.TacGia on s.matacgia equals r.matacgia
                         join g in db.TheLoai on s.mantl equals g.matl
                         select new
                         {
                             masach = s.masach,
                             tenhinhthuc = v.tenhinhthuc,
                             tentacgia = r.tentacgia,
                             tensach = s.tensach,
                             tenncc = f.tenncc,
                             tennxb = e.tennxb,
                             tentheloai = g.tentheloai,
                             isdeleted = s.isdeleted,
                             hinh = s.hinh,
                             gia = s.gia
                         }).ToList();
                ViewBag.SanPham1 = d;
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                return View();
            }
        }
        [HttpGet]
        public ActionResult ChiTiet(int masach)
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var tim = SachAction.FindSach(masach);
                int mancc = tim.mancc;
                int mahinhthuc = tim.mahinhthuc;
                int matacgia = tim.matacgia;
                int mas = tim.masach;
                ViewBag.ChiTiet = SachAction.FindSach(masach);
                var nhacungcap = (from a in db.NhaCungCap
                                  where mancc == a.mancc /*&& a.isdeleted == "active"*/
                                  select a.tenncc).FirstOrDefault();
                var hinhthuc = (from a in db.HinhThuc
                                where mahinhthuc == a.mahinhthuc /*&& a.isdeleted == "active"*/
                                select a.tenhinhthuc).FirstOrDefault();
                var tacgia = (from a in db.TacGia
                              where matacgia == a.matacgia /*&& a.isdeleted == "active"*/
                              select a.tentacgia).FirstOrDefault();
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                var chitietsach = (from b in db.ChiTietSach
                                   where mas == b.masach
                                   select b).FirstOrDefault();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                ViewBag.NhaCungCap = nhacungcap;
                ViewBag.HinhThuc = hinhthuc;
                ViewBag.TacGia = tacgia;
                if (chitietsach != null)
                {
                    ViewBag.TenChiTietSach = chitietsach.tenchitiet;
                    ViewBag.ChiTietSach = chitietsach.chitiet;
                }
                else
                {
                    ViewBag.TenChiTietSach = "";
                    ViewBag.ChiTietSach = "";
                }

                return View();
            }
        }
        [HttpPost]
        public ActionResult addCart(int masach, int soluong)
        {
            using (var db = new Context())
            {
                if (checkLogin() == true)
                {
                    var cart = SachAction.FindSach(masach);
                    var appear = (from a in db.ChiTietGioHang
                                  where a.tenctgiohang == cart.tensach && a.isdeleted == false
                                  select a).ToList();
                    List<ChiTietGioHang> c = appear;
                    int makh = Convert.ToInt32(Session["id"]);
                    var b = (from a in db.GioHang
                             where a.makh == makh
                             select a).ToList();
                    //if (appear.Count() == 0)
                    //{
                    if (b.Count() == 0)
                    {
                        if (appear.Count() == 0)
                        {
                            GioHangAction.addGioHang(Convert.ToInt32(Session["id"]), Session["TaiKhoan"].ToString());
                            ChiTietGioHangAction.addCTGioHang(Convert.ToInt32(Session["id"]), cart.tensach, cart.hinh, cart.gia, soluong);
                            return Redirect("~/Sach/GioHang");
                        }
                        else
                        {
                            GioHangAction.addGioHang(Convert.ToInt32(Session["id"]), Session["TaiKhoan"].ToString());
                            ChiTietGioHangAction.DaTonTai(c[0].mactgiohang, soluong);
                            return Redirect("~/Sach/GioHang");
                        }
                    }
                    else
                    {
                        if (appear.Count() == 0)
                        {
                            ChiTietGioHangAction.addCTGioHang(Convert.ToInt32(Session["id"]), cart.tensach, cart.hinh, cart.gia, soluong);
                            return Redirect("~/Sach/GioHang");
                        }
                        else
                        {
                            ChiTietGioHangAction.DaTonTai(c[0].mactgiohang, soluong);
                            return Redirect("~/Sach/GioHang");
                        }
                    }
                }
                else
                {
                    return Redirect("~/Sach/DangNhap");
                }
                //}
                //else
                //{
                //    CartAction.UpdateCartAppear(b.id, b.soluong);
                //    return Redirect("~/Home/Cart");
                //}
            }
        }
        [HttpGet]
        public ActionResult GioHang()
        {
            if (checkLogin() == true)
            {
                using (var db = new Context())
                {
                    int a = Convert.ToInt32(Session["id"]);
                    var giohang = (from b in db.ChiTietGioHang
                                   where b.makh == a && b.isdeleted == false
                                   select b).ToList();
                    int c = Convert.ToInt32(Session["id"]);
                    var cart = (from b in db.ChiTietGioHang
                                where b.makh == c && b.isdeleted == false
                                select b).ToList();
                    List<ChiTietGioHang> giohanga = cart;
                    int sum = 0;
                    foreach (var b in giohanga)
                    {
                        sum = sum + b.soluong;
                    }
                    double tonggia = 0;
                    foreach (ChiTietGioHang d in cart)
                    {
                        tonggia = tonggia + d.tongtien;
                    }
                    GioHangAction.UpdateGioHang1(c, tonggia);
                    var tonggiatien = (from b in db.GioHang
                                       where b.makh == c
                                       select b).FirstOrDefault();
                    ViewBag.TongGiaTien = tonggiatien.tongtien;
                    ViewBag.SoLuong = sum;
                    ViewBag.GioHang = giohang;
                    return View();
                }
            }
            else
            {
                return Redirect("~/Sach/DangNhap");
            }
        }
        [HttpPost]
        public ActionResult GioHang(int mactgh, int soluong)
        {
            ChiTietGioHangAction.UpdateCTGioHang(mactgh, soluong);
            return Redirect("~/Sach/GioHang");
        }
        public Boolean checkLogin()
        {
            if (Session["taikhoan"] != null && !Session["taikhoan"].Equals(""))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public ActionResult DangXuat()
        {
            Session.Clear();
            return Redirect("~/Sach/TrangChu");
        }
        [HttpGet]
        public ActionResult ChiTietSach(int id)
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var chitietsach = ChiTietSachAction.FindCTSach(id);
                if (chitietsach != null)
                {
                    ViewBag.CTSach = chitietsach;
                    var cart = (from b in db.ChiTietGioHang
                                where b.makh == c && b.isdeleted == false
                                select b).ToList();
                    List<ChiTietGioHang> giohang = cart;
                    int sum = 0;
                    foreach (var a in giohang)
                    {
                        sum = sum + a.soluong;
                    }
                    ViewBag.SoLuong = sum;
                    return View();
                }
                else
                {
                    ChiTietSachAction.addCTSach1(id);
                    ViewBag.CTSach = ChiTietSachAction.FindCTSach(id);
                    return Redirect("~/Sach/SuaChiTietSach?masach=" + id);
                }
            }
        }
        [HttpPost]
        public ActionResult ChiTietSach(int masach, string tenchitiet, string chitiet)
        {
            ChiTietSachAction.addCTSach(masach, tenchitiet, chitiet);
            return Redirect("~/Sach/ChiTietSach");
        }
        public ActionResult XoaGioHang(int id)
        {
            ChiTietGioHangAction.XoaChiTietGioHang(id);
            return Redirect("~/Sach/GioHang");
        }
        [HttpGet]
        public ActionResult SuaChiTietSach(int masach)
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                ViewBag.CTSach = ChiTietSachAction.FindCTSach(masach);
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                return View();
            }
        }
        [HttpPost]
        public ActionResult SuaChiTietSach(int masach, string tenchitiet, string chitiet)
        {
            ChiTietSachAction.UpdateCTSach(masach, tenchitiet, chitiet);
            return Redirect("~/Sach/ChiTietSach?id=" + masach);
        }
        public ActionResult QuanLyKhachHang()
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                ViewBag.KhachHang = KhachHangAction.ListKhachHang();
                return View();
            }
        }
        [HttpGet]
        public ActionResult SuaSach(int masach)
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var tim = SachAction.FindSach(masach);
                var ncc1 = (from b in db.NhaCungCap
                            where b.mancc != tim.mancc
                            select b).ToList();
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                ViewBag.ListNCC = ncc1;
                ViewBag.ListHinhThuc = db.HinhThuc.Where(s => s.mahinhthuc != tim.mahinhthuc).ToList();
                ViewBag.ListTacGia = db.TacGia.Where(s => s.matacgia != tim.matacgia).ToList();
                ViewBag.ListNXB = db.NhaXuatBan.Where(s => s.manxb != tim.manxb).ToList();
                ViewBag.ListTheLoai = db.TheLoai.Where(s => s.matl != tim.mantl).ToList();

                ViewBag.SuaSach = tim;
                var ncc = (from b in db.NhaCungCap
                           where b.mancc == tim.mancc
                           select b).First();
                var hinhthuc = (from b in db.HinhThuc
                                where b.mahinhthuc == tim.mahinhthuc
                                select b).First();
                var nhaxuatban = (from b in db.NhaXuatBan
                                  where b.manxb == tim.manxb
                                  select b).First();
                var tacgia = (from b in db.TacGia
                              where b.matacgia == tim.matacgia
                              select b).First();
                var theloai = (from b in db.TheLoai
                               where b.matl == tim.mantl
                               select b).First();
                ViewBag.SSNCC = ncc;
                ViewBag.SSHinhThuc = hinhthuc;
                ViewBag.SSNXB = nhaxuatban;
                ViewBag.SSTacGia = tacgia;
                ViewBag.SSTheLoai = theloai;
                return View();
            }
        }
        [HttpPost]
        public ActionResult SuaSach(int masach, int mancc, int mahinhthuc, int matacgia, int manxb, int matl, string tensach, double gia, HttpPostedFileBase myfile, string hinh)
        {
            try
            {
                string _path = "";
                if (myfile.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(myfile.FileName);
                    _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    myfile.SaveAs(_path);
                    SachAction.UpdateSach(masach, mancc, mahinhthuc, matacgia, manxb, matl, tensach, gia, _FileName);
                }
                return RedirectToAction("QuanLySanPham", "Sach");
            }
            catch
            {
                SachAction.UpdateSach(masach, mancc, mahinhthuc, matacgia, manxb, matl, tensach, gia, hinh);
                return RedirectToAction("QuanLySanPham", "Sach");
            }
        }
        public ActionResult XoaSach(int masach)
        {
            SachAction.XoaSach(masach);
            return RedirectToAction("QuanLySanPham", "Sach");
        }
        [HttpGet]
        public ActionResult SuaKhach(int makh)
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                ViewBag.SuaKhach = KhachHangAction.FindKhach(makh);
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                return View();
            }
        }
        [HttpPost]
        public ActionResult SuaKhach(int makh, string ho, string ten, string diachi, string sdt, string email, string matkhau, string nhaplaimatkhau, string quyen)
        {
            using (var db = new Context())
            {
                var tontai = (from b in db.GioHang
                              where b.makh == makh
                              select b).ToList();
                if (tontai.Count() == 0)
                {
                    KhachHangAction.UpdateKhachHang(makh, ho, ten, diachi, sdt, email, matkhau, nhaplaimatkhau, quyen);
                    GioHangAction.addGioHang(makh, email);
                    GioHangAction.UpdateGioHang(makh, email);
                    LichSuHeThongAction.addLSHeThong(Session["taikhoan"].ToString(), makh);
                    return Redirect("~/Sach/QuanLyKhachHang");
                }
                else
                {
                    GioHangAction.addGioHang(makh, email);
                    GioHangAction.UpdateGioHang(makh, email);
                    return Redirect("~/Sach/QuanLyKhachHang");
                }
            }
        }
        public ActionResult XoaKhach(int makh)
        {
            KhachHangAction.XoaKhachHang(makh);
            LichSuHeThongAction.addLSHeThong1(Session["taikhoan"].ToString(), makh);
            return Redirect("~/Sach/QuanLyKhachHang");
        }
        //public ActionResult ThanhToan(int id)
        //{
        //    GioHangAction.XoaGioHang(id);
        //    return Redirect("~/Sach/GioHang");
        //}
        public ActionResult SanPham()
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                ViewBag.ListSach = SachAction.ListSach().Take(4);
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                return View();
            }
        }
        public ActionResult ThanhToan()
        {
            using (var db = new Context())
            {
                var c = ChiTietGioHangAction.ListCTGioHang();
                foreach (var a in c)
                {
                    var ctsach = ChiTietGioHangAction.FindCTGioHang(a.mactgiohang);
                    var masach = db.Sach.Where(p => p.tensach == ctsach.tenctgiohang && p.isdeleted == false).FirstOrDefault();
                    ChiTietGioHangAction.Xoa(a.mactgiohang);
                    LichSuMuaSachAction.addLSMuaSach(Session["taikhoan"].ToString(), ctsach.tenctgiohang, masach.masach, ctsach.soluong);
                }
                return Redirect("~/Sach/GioHang");
            }
        }
        public ActionResult ThanhToanCTGioHang(int id)
        {
            using (var db = new Context())
            {
                var ctsach = ChiTietGioHangAction.FindCTGioHang(id);
                var masach = db.Sach.Where(p => p.tensach == ctsach.tenctgiohang && p.isdeleted == false).FirstOrDefault();
                ChiTietGioHangAction.Xoa(id);
                LichSuMuaSachAction.addLSMuaSach(Session["taikhoan"].ToString(), ctsach.tenctgiohang, masach.masach, ctsach.soluong);
                return Redirect("~/Sach/GioHang");
            }
        }
        public ActionResult TimKiem(string tensach)
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var timkiem = (from b in db.Sach
                               where b.tensach.Contains(tensach) || b.tensach.StartsWith(tensach) || tensach.EndsWith(b.tensach) || tensach.StartsWith(b.tensach) && b.isdeleted == false
                               select b).ToList();
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                //var timkiem = db.Sach.Where(s => ConvertToUnSign(s.tensach).StartsWith(tensach)).ToList();
                ViewBag.TimKiem = timkiem.Take(4);
                return View();
            }
        }
        public ActionResult LichSuHeThong()
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                ViewBag.LSHT = LichSuHeThongAction.ListLichSuHeThong();
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                return View();
            }
        }
        [HttpGet]
        public ActionResult ThemKhach()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemKhach(string ho, string ten, string diachi, string sdt, string email, string matkhau, string nhaplaimatkhau)
        {
            KhachHangAction.addKhachHang(ho, ten, diachi, sdt, email, matkhau, nhaplaimatkhau);
            return Redirect("~/Sach/QuanLyKhachHang");
        }
        public ActionResult LichSuMuaSach()
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                ViewBag.LSMS = LichSuMuaSachAction.ListLichSuMuaSach();
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                return View();
            }
        }
        public ActionResult TimKiemKhachHang(string email)
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var timkiem = (from b in db.KhachHang
                               where b.email.Contains(email) || b.email.StartsWith(email) || email.EndsWith(b.email) || email.StartsWith(b.email) && b.isdeleted == false
                               select b).ToList();
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                //var timkiem = db.Sach.Where(s => ConvertToUnSign(s.tensach).StartsWith(tensach)).ToList();
                ViewBag.TimKiemKhach1 = timkiem;
                TempData["abc"] = timkiem;
                return View();
            }
        }
        public ActionResult TimKiemSanPham(string tensach)
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var timkiem = (from b in db.Sach
                               where b.tensach.Contains(tensach) || b.tensach.StartsWith(tensach) || tensach.EndsWith(b.tensach) || tensach.StartsWith(b.tensach) && b.isdeleted == false
                               select b).ToList();
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                //var timkiem = db.Sach.Where(s => ConvertToUnSign(s.tensach).StartsWith(tensach)).ToList();
                ViewBag.TimKiemSach = timkiem;
                TempData["bcd"] = timkiem;
                return View();
            }
        }
        public ActionResult XuatKhach()
        {
            var gv = new GridView();
            gv.DataSource = KhachHangAction.ListKhachHang();
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";

            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);

            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();

            return Redirect("~/Sach/QuanLyKhachHang");

        }
        public ActionResult XuatKhach1()
        {
            var gv = new GridView();
            gv.DataSource = TempData["abc"];
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);

            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();

            return Redirect("~/Sach/QuanLyKhachHang");

        }
        public ActionResult XuatSach()
        {
            var gv = new GridView();
            gv.DataSource = SachAction.ListSach();
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            //Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);

            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();

            return Redirect("~/Sach/QuanLySanPham");

        }
        public ActionResult XuatSach1()
        {
            var gv = new GridView();
            gv.DataSource = TempData["bcd"];
            gv.DataBind();

            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";

            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);

            gv.RenderControl(objHtmlTextWriter);

            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();

            return Redirect("~/Sach/QuanLySanPham");

        }
        [HttpGet]
        public ActionResult DSTheLoai()
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                ViewBag.DSTheLoai = TheLoai.ListTheLoai();
                return View();
            }
        }
        [HttpGet]
        public ActionResult ThemTheLoai()
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                return View();
            }
        }
        [HttpPost]
        public ActionResult ThemTheLoai(string tentheloai)
        {
            TheLoai.addTheLoai(tentheloai);
            return Redirect("~/Sach/DSTheLoai");
        }
        [HttpGet]
        public ActionResult SuaTheLoai(int matl)
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                ViewBag.TimTheLoai = TheLoai.FindTheLoai(matl);
                return View();
            }
        }
        [HttpPost]
        public ActionResult SuaTheLoai(int matl,string tentheloai)
        {
            TheLoai.UpdateTheLoai(matl,tentheloai);
            return Redirect("~/Sach/DSTheLoai");
        }
        public ActionResult XoaTheLoai(int matl)
        {
            using (var db = new Context())
            {
                var c = db.Sach.Where(p => p.mantl == matl).ToList();
                if(c.Count()==0)
                { 
                ViewBag.TimTheLoai = TheLoai.FindTheLoai(matl);
                TheLoai.UpdateTheLoai1(matl);
                return Redirect("~/Sach/DSTheLoai");
                }
                else
                {
                    return Redirect("~/Sach/DSTheLoai");
                }
            }
        }
        [HttpGet]
        public ActionResult DSTacGia()
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                ViewBag.DSTacGia = TacGia.ListTacGia();
                return View();
            }
        }
        [HttpGet]
        public ActionResult ThemTacGia()
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                return View();
            }
        }
        [HttpPost]
        public ActionResult ThemTacGia(string tentacgia)
        {
            TacGia.addTacGia(tentacgia);
            return Redirect("~/Sach/DSTacGia");
        }
        [HttpGet]
        public ActionResult SuaTacGia(int matacgia)
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                ViewBag.TimTacGia = TacGia.FindTacGia(matacgia);
                return View();
            }
        }
        [HttpPost]
        public ActionResult SuaTacGia(int matacgia, string tentacgia)
        {
            TacGia.UpdateTacGia(matacgia, tentacgia);
            return Redirect("~/Sach/DSTacGia");
        }
        public ActionResult XoaTacGia(int matacgia)
        {
            using (var db = new Context())
            {
                var c = db.Sach.Where(p => p.matacgia == matacgia).ToList();
                if (c.Count() == 0)
                {
                    ViewBag.TimTacGia = TacGia.FindTacGia(matacgia);
                    TacGia.UpdateTacGia1(matacgia);
                    return Redirect("~/Sach/DSTacGia");
                }
                else
                {
                    return Redirect("~/Sach/DSTacGia");
                }
            }
        }
        public ActionResult QuanLy()
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                var abc = (from b in db.SoLuongNguoiDangNhap
                           select new { letter = b.thang, frequency = b.soluongnguoi }).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                ViewBag.ABCD = SoLuongNguoiDangNhap.ListDangNhap();
                ViewBag.BCD = abc;
                return View();
            }
        }
        [HttpGet]
        public ActionResult DSNXB()
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                ViewBag.DSNXB = NhaXuatBan.ListNXB();
                return View();
            }
        }
        [HttpGet]
        public ActionResult ThemNXB()
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                return View();
            }
        }
        [HttpPost]
        public ActionResult ThemNXB(string tennxb)
        {
            NhaXuatBan.addNhaXuatBan(tennxb);
            return Redirect("~/Sach/DSNXB");
        }
        [HttpGet]
        public ActionResult SuaNXB(int manxb)
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                ViewBag.TimNXB = NhaXuatBan.FindNhaXuatBan(manxb);
                return View();
            }
        }
        [HttpPost]
        public ActionResult SuaNXB(int manxb, string tennxb)
        {
            NhaXuatBan.UpdateNhaXuatBan(manxb, tennxb);
            return Redirect("~/Sach/DSNXB");
        }
        public ActionResult XoaNXB(int manxb)
        {
            using (var db = new Context())
            {
                var c = db.Sach.Where(p => p.manxb == manxb).ToList();
                if (c.Count() == 0)
                {
                    ViewBag.TimNXB = NhaXuatBan.FindNhaXuatBan(manxb);
                    NhaXuatBan.UpdateNhaXuatBan1(manxb);
                    return Redirect("~/Sach/DSNXB");
                }
                else
                {
                    return Redirect("~/Sach/DSNXB");
                }
            }
        }
        [HttpGet]
        public ActionResult DSNCC()
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                ViewBag.DSNCC = NhaCungCap.ListNhaCungCap();
                return View();
            }
        }
        [HttpGet]
        public ActionResult ThemNCC()
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                return View();
            }
        }
        [HttpPost]
        public ActionResult ThemNCC(string tenncc)
        {
            NhaCungCap.addNhaCungCap(tenncc);
            return Redirect("~/Sach/DSNCC");
        }
        [HttpGet]
        public ActionResult SuaNCC(int mancc)
        {
            using (var db = new Context())
            {
                int c = Convert.ToInt32(Session["id"]);
                var cart = (from b in db.ChiTietGioHang
                            where b.makh == c && b.isdeleted == false
                            select b).ToList();
                List<ChiTietGioHang> giohang = cart;
                int sum = 0;
                foreach (var a in giohang)
                {
                    sum = sum + a.soluong;
                }
                ViewBag.SoLuong = sum;
                ViewBag.TimNCC = NhaCungCap.FindNhaCungCap(mancc);
                return View();
            }
        }
        [HttpPost]
        public ActionResult SuaNCC(int mancc, string tenncc)
        {
            NhaCungCap.UpdateNhaCungCap(mancc, tenncc);
            return Redirect("~/Sach/DSNCC");
        }
        public ActionResult XoaNCC(int mancc)
        {
            using (var db = new Context())
            {
                var c = db.Sach.Where(p => p.mancc == mancc).ToList();
                if (c.Count() == 0)
                {
                    ViewBag.TimNCC = NhaCungCap.FindNhaCungCap(mancc);
                    NhaCungCap.UpdateNhaCungCap1(mancc);
                    return Redirect("~/Sach/DSNCC");
                }
                else
                {
                    return Redirect("~/Sach/DSNCC");
                }
            }
        }

    }
}
