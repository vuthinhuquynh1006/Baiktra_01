using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoSpeedLogistics
{
    // ==========================================
    // 1. ABSTRACT CLASS PHUONGTIEN (LỚP CHA TRỪU TƯỢNG)
    // ==========================================
    public abstract class PhuongTien
    {
        // Private Fields (Đóng gói)
        private string _maPT = string.Empty;
        private string _tenHang = string.Empty;
        private int _namSanXuat;
        private decimal _giaGoc;

        // Encapsulation Properties & Validation
        public string MaPT
        {
            get => _maPT;
            set => _maPT = string.IsNullOrWhiteSpace(value) ? "PT000" : value.Trim();
        }

        public string TenHang
        {
            get => _tenHang;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Tên hãng không được để trống!");
                _tenHang = value.Trim();
            }
        }

        public int NamSanXuat
        {
            get => _namSanXuat;
            set
            {
                int currentYear = DateTime.Now.Year;
                if (value < 1900 || value > currentYear)
                    throw new ArgumentException("Năm sản xuất không hợp lệ!");
                _namSanXuat = value;
            }
        }

        public decimal GiaGoc
        {
            get => _giaGoc;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Giá gốc phải lớn hơn 0!");
                _giaGoc = value;
            }
        }

        // Constructor
        protected PhuongTien(string maPT, string tenHang, int namSanXuat, decimal giaGoc)
        {
            MaPT = maPT;
            TenHang = tenHang;
            NamSanXuat = namSanXuat;
            GiaGoc = giaGoc;
        }

        // Abstract Method
        public abstract decimal TinhGiaLanBanh();

        // Virtual Method
        public virtual string GetInfo()
        {
            return $"Mã PT: {MaPT} | Hãng: {TenHang} | Năm SX: {NamSanXuat} | Giá gốc: {GiaGoc:N0} VNĐ";
        }
    }

    // ==========================================
    // 2. CLASS OTO (KẾ THỪA TỪ PHUONGTIEN)
    // ==========================================
    public class OTo : PhuongTien
    {
        private int _soChoNgoi;
        private double _dungTichDongCo;

        public int SoChoNgoi
        {
            get => _soChoNgoi;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Số chỗ ngồi phải lớn hơn 0!");
                _soChoNgoi = value;
            }
        }

        public double DungTichDongCo
        {
            get => _dungTichDongCo;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Dung tích động cơ phải lớn hơn 0!");
                _dungTichDongCo = value;
            }
        }

        public OTo(string maPT, string tenHang, int namSanXuat, decimal giaGoc, int soChoNgoi, double dungTichDongCo)
            : base(maPT, tenHang, namSanXuat, giaGoc)
        {
            SoChoNgoi = soChoNgoi;
            DungTichDongCo = dungTichDongCo;
        }

        // Override TinhGiaLanBanh()
        public override decimal TinhGiaLanBanh()
        {
            if (SoChoNgoi <= 9)
            {
                // Thuế trước bạ 12% + Thuế TTĐB 30%
                return GiaGoc + (GiaGoc * 0.12m) + (GiaGoc * 0.30m);
            }
            // Số chỗ > 9: Thuế trước bạ 10%
            return GiaGoc + (GiaGoc * 0.10m);
        }

        // Override GetInfo()
        public override string GetInfo()
        {
            return $"{base.GetInfo()} | Loại: Ô tô | Chỗ ngồi: {SoChoNgoi} | Động cơ: {DungTichDongCo}L ==> GIÁ LĂN BÁNH: {TinhGiaLanBanh():N0} VNĐ";
        }
    }

    // ==========================================
    // 3. CLASS XEMAY (KẾ THỪA TỪ PHUONGTIEN)
    // ==========================================
    public class XeMay : PhuongTien
    {
        private int _dungTichXylanh;

        public int DungTichXylanh
        {
            get => _dungTichXylanh;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Dung tích xilanh phải lớn hơn 0!");
                _dungTichXylanh = value;
            }
        }

        public XeMay(string maPT, string tenHang, int namSanXuat, decimal giaGoc, int dungTichXylanh)
            : base(maPT, tenHang, namSanXuat, giaGoc)
        {
            DungTichXylanh = dungTichXylanh;
        }

        // Override TinhGiaLanBanh()
        public override decimal TinhGiaLanBanh()
        {
            decimal thueTruocBa = DungTichXylanh < 175 ? 0.02m : 0.05m;
            return GiaGoc + (GiaGoc * thueTruocBa);
        }

        // Override GetInfo()
        public override string GetInfo()
        {
            return $"{base.GetInfo()} | Loại: Xe máy | Xilanh: {DungTichXylanh}cc ==> GIÁ LĂN BÁNH: {TinhGiaLanBanh():N0} VNĐ";
        }
    }

    // ==========================================
    // 4. CLASS QUANLYPHUONGTIEN (QUẢN LÝ TẬP HỢP)
    // ==========================================
    public class QuanLyPhuongTien
    {
        private readonly List<PhuongTien> _danhSach = new();

        public void AddPhuongTien(PhuongTien pt)
        {
            if (pt != null)
                _danhSach.Add(pt);
        }

        public void DisplayAll()
        {
            Console.WriteLine("\n=== DANH SÁCH TOÀN BỘ PHƯƠNG TIỆN ===");
            if (_danhSach.Count == 0)
            {
                Console.WriteLine("Danh sách hiện đang trống.");
                return;
            }

            foreach (var pt in _danhSach)
            {
                Console.WriteLine(pt.GetInfo());
            }
        }

        public PhuongTien? FindMaxGiaLanBanh()
        {
            if (_danhSach.Count == 0) return null;
            return _danhSach.MaxBy(pt => pt.TinhGiaLanBanh());
        }

        public List<PhuongTien> SearchByName(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<PhuongTien>();

            return _danhSach
                .Where(pt => pt.TenHang.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }

    // ==========================================
    // 5. CHƯƠNG TRÌNH CHÍNH
    // ==========================================
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            QuanLyPhuongTien ql = new();

            while (true)
            {
                Console.WriteLine("\n================ AUTOSPEED LOGISTICS ================");
                Console.WriteLine("1. Thêm mới Ô tô");
                Console.WriteLine("2. Thêm mới Xe máy");
                Console.WriteLine("3. Hiển thị toàn bộ danh sách phương tiện");
                Console.WriteLine("4. Tìm phương tiện có giá lăn bánh cao nhất");
                Console.WriteLine("5. Tìm kiếm phương tiện theo tên hãng");
                Console.WriteLine("0. Thoát chương trình");
                Console.Write("Lựa chọn thao tác (0-5): ");

                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        NhapOTo(ql);
                        break;
                    case "2":
                        NhapXeMay(ql);
                        break;
                    case "3":
                        ql.DisplayAll();
                        break;
                    case "4":
                        var maxPt = ql.FindMaxGiaLanBanh();
                        if (maxPt != null)
                        {
                            Console.WriteLine("\n--- PHƯƠNG TIỆN CÓ GIÁ LĂN BÁNH CAO NHẤT ---");
                            Console.WriteLine(maxPt.GetInfo());
                        }
                        else
                        {
                            Console.WriteLine("\nDanh sách trống!");
                        }
                        break;
                    case "5":
                        Console.Write("\nNhập từ khóa tên hãng cần tìm: ");
                        string? keyword = Console.ReadLine() ?? "";
                        var ketQua = ql.SearchByName(keyword);
                        Console.WriteLine($"\n--- KẾT QUẢ TÌM KIẾM ({ketQua.Count}) ---");
                        foreach (var pt in ketQua)
                        {
                            Console.WriteLine(pt.GetInfo());
                        }
                        break;
                    case "0":
                        Console.WriteLine("Đã thoát chương trình.");
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ, vui lòng chọn lại!");
                        break;
                }
            }
        }

        private static void NhapOTo(QuanLyPhuongTien ql)
        {
            Console.WriteLine("\n--- NHẬP THÔNG TIN Ô TÔ ---");
            try
            {
                Console.Write("Mã phương tiện: ");
                string maPT = Console.ReadLine() ?? "";

                Console.Write("Tên hãng: ");
                string tenHang = Console.ReadLine() ?? "";

                Console.Write("Năm sản xuất: ");
                int namSX = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Giá gốc (VNĐ): ");
                decimal giaGoc = decimal.Parse(Console.ReadLine() ?? "0");

                Console.Write("Số chỗ ngồi: ");
                int soCho = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Dung tích động cơ (Lít): ");
                double dungTich = double.Parse(Console.ReadLine() ?? "0");

                OTo oto = new(maPT, tenHang, namSX, giaGoc, soCho, dungTich);
                ql.AddPhuongTien(oto);

                Console.WriteLine("\n-> THÊM Ô TÔ THÀNH CÔNG!");
                Console.WriteLine($"-> Thông tin chi tiết: {oto.GetInfo()}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Lỗi: Dữ liệu số nhập vào không đúng định dạng!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Lỗi Validation: {ex.Message}");
            }
        }

        private static void NhapXeMay(QuanLyPhuongTien ql)
        {
            Console.WriteLine("\n--- NHẬP THÔNG TIN XE MÁY ---");
            try
            {
                Console.Write("Mã phương tiện: ");
                string maPT = Console.ReadLine() ?? "";

                Console.Write("Tên hãng: ");
                string tenHang = Console.ReadLine() ?? "";

                Console.Write("Năm sản xuất: ");
                int namSX = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Giá gốc (VNĐ): ");
                decimal giaGoc = decimal.Parse(Console.ReadLine() ?? "0");

                Console.Write("Dung tích xi-lanh (cc): ");
                int dungTichCc = int.Parse(Console.ReadLine() ?? "0");

                XeMay xeMay = new(maPT, tenHang, namSX, giaGoc, dungTichCc);
                ql.AddPhuongTien(xeMay);

                Console.WriteLine("\n-> THÊM XE MÁY THÀNH CÔNG!");
                Console.WriteLine($"-> Thông tin chi tiết: {xeMay.GetInfo()}");
            }
            catch (FormatException)
            {
                Console.WriteLine("Lỗi: Dữ liệu số nhập vào không đúng định dạng!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Lỗi Validation: {ex.Message}");
            }
        }
    }
}