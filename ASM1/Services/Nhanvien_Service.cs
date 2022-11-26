using ASM1.Context;
using ASM1.DomainModels;
using ASM1.Responsitories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ASM1.Services
{
    public class Nhanvien_Service
    {
        FpolyDBContext _dbContext;
        NhanVien_Responsitory _nvRes;
        List<NhanVien> _lstNV;
        List<ChucVu> _lstCV;
        List<CuaHang> _lstCuaHang;
        public Nhanvien_Service()
        {
            _dbContext = new FpolyDBContext();
            _nvRes = new NhanVien_Responsitory();
            _lstNV = new List<NhanVien>();
            _lstCV = new List<ChucVu>();
            _lstCuaHang = new List<CuaHang>();
            GetDataFromDB();
        }

        #region GetData
        public void GetDataFromDB()
        {
            _lstNV = _nvRes.GetLstNhanVien();
            _lstCV = _nvRes.GetLstChucVu();
            _lstCuaHang = _nvRes.GetLstCuaHang();
        }
        
        public Guid GetIdChucVu()
        {
            Guid cv = Guid.Empty;
            cv = GetLstChucVu().Where(c => c.Ma.ToLower() == "tp").Select(c => c.Id).FirstOrDefault();
            return cv;
        }

        public string GetMaTruongPhong()
        {
            string ma;
            ma = GetLstChucVu().Where(c => c.Id == GetIdChucVu()).Select(c => c.Ma).FirstOrDefault();
            return ma;
        }

        public List<NhanVien> GetNguoiGuiBaoCao()
        {
            return GetAllLstNV_Service().Where(c => c.IdCv == GetIdChucVu()).ToList();
        }


        public string GetTenCHByMa(string ma)
        {
            string ten = _lstCuaHang.Where(c => c.Ma == ma).Select(c => c.Ten).ToString();
            return ten;
        }

        public List<NhanVien> GetLstTruongPhong() =>GetLstNhanVien().Where(c=>c.IdCv == GetIdChucVu()).ToList();

        public string GetTenNhanVienById(Guid id) => GetAllLstNV_Service().Where(c => c.Id == id).FirstOrDefault().Ten;
        public List<Guid> GetLstIdNV() => _lstNV.Select(c => c.Id).ToList();

        public List<string> GetMaCuaHang() => _lstCuaHang.Select(c => c.Ma).ToList();

        public List<string> GetLstSDTNhanvien() => _lstNV.Select(c => c.Sdt).ToList();

        public List<NhanVien> GetLstNV(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return _lstNV;
            }
            return _lstNV.Where(c => c.Ma.ToLower().StartsWith(input) || c.Ten.ToLower().StartsWith(input)).ToList();
        }

        public List<string> GetLstMaNhanVien() => _lstNV.Select(c => c.Ma).ToList();

        public List<NhanVien> GetAllLstNV_Service() => _lstNV = _nvRes.GetLstNhanVien();

        public List<Guid> GetAllidChucVu() => _lstCV.Select(c => c.Id).ToList();

        public List<string> GetTenChucVuByid(Guid? id) => _lstCV.Where(c => c.Id == id).Select(c => c.Ten).ToList();

        public List<string> GetTenChucVu() => _lstCV.Select(c => c.Ten).ToList();

        public string GetTenCVById(Guid? id)
        {
            var lst = GetAllLstNV_Service().Where(c => c.Id == id).Select(c => c).ToList();
            string kq = "";
            foreach (var c in lst)
            {
                kq += c.Ten;
            }
            return kq;
        }
        public List<string> GetTenCuaHangById(Guid id)
        {

            var lst = _lstCuaHang.Where(c => c.Id == id).Select(c => c.Ten).ToList();
            if (lst.Count == 0) return null;
            return lst;
        }

        public Guid GetIDByTenCh(string ten)
        {
            if (string.IsNullOrEmpty(ten)) return Guid.Empty;
            var t = _lstCuaHang.Where(c => c.Ten == ten).Select(c => c.Id).FirstOrDefault();
            return t;
        }

        public Guid GetIdByTenCV(string ten)
        {
            if (string.IsNullOrEmpty(ten)) return Guid.Empty;
            var t = _lstCV.Where(c => c.Ten == ten).Select(c => c.Id).FirstOrDefault();
            return t;
        }

        //public Guid GetGuidByMa(string ma) => _lstCV.Select(c=>c.Id).FirstOrDefault(c=>)
        public List<string> GetTenNVById(Guid id) => _lstNV.Where(c => c.Id == id).Select(c => c.Ten).ToList();

        public List<NhanVien> LocNhanVienByChucVu(string cv) => GetLstNhanVien().Where(c=>c.IdCv == GetIdByTenCV(cv)).ToList();

        public List<NhanVien> LocNhanVienByCH(string ch) => GetLstNhanVien().Where(c => c.IdCh == GetIDByTenCh(ch)).ToList();
        #endregion

        #region Service
        public string AddNV(NhanVien nv)
        {
            //string regexSDT = "^0[1-9]{1}\\d{8}$";
            foreach (var c in GetAllLstNV_Service())
            {
                if (c.Id == nv.Id) return "Thêm thất bại do trùng id";
            }
            if (_nvRes.Add(nv))
            {
                //if (nv.DiaChi == "") return "Địa chỉ bị rỗng";
                //else if (!Regex.IsMatch(nv.Sdt, regexSDT)) return "Sai định dạng số điện thoại";
                GetDataFromDB();
                return "Thêm thành công";
            }
            return "Thêm thất bại";
        }

        public string UpdateNV(Guid id, NhanVien nv)
        {
            string regexSDT = "^0[1-9]{1}\\d{8}$";
            if (nv == null) return "Sửa thất bại";
            if (GetLstNhanVien().Any(c => c.Id == id) == false)
            {
                return "Không tìm thấy nhân viên muốn sửa!";
            }
            if (!Regex.IsMatch(nv.Sdt, regexSDT)) return "Sai định dạng số điện thoại";
            DeleteNV(id);
            AddNV(nv);
            return "Sửa thành công";
            //foreach (var v in GetLstNhanVien().Where(c => c.Id == id))
            //{
            //    v.Ma = nv.Ma;
            //    v.Ho = nv.Ho;
            //    v.TenDem = nv.TenDem;
            //    v.GioiTinh = nv.GioiTinh;
            //    v.DiaChi = nv.DiaChi;
            //    v.Sdt = nv.Sdt;
            //    v.NgaySinh = nv.NgaySinh;
            //    v.IdCv = nv.IdCv;
            //    _nvRes.Update(nv);
            //}
            //return "Sửa thành công";

        }

        //public List<NhanVien> Search_Nhanvien(string input)
        //{
        //    if (string.IsNullOrEmpty(input))
        //    {
        //        return GetLstNV();
        //    }
        //    return GetLstNV().Where(c => c.Ma.ToLower().StartsWith(input) || c.Ten.ToLower().StartsWith(input)).ToList();
        //}

        public string DeleteNV(Guid id)
        {
            var nvtmp = _lstNV.FirstOrDefault(c => c.Id == id);
            if (_nvRes.Delete(nvtmp))
            {
                GetAllLstNV_Service();
                return "Xóa thành công";
            }
            return "Xóa thất bại";
        }
        #endregion


        public List<ChucVu> GetLstChucVu() => _dbContext.ChucVus.ToList();

        public List<CuaHang> GetLstCuaHang() => _dbContext.CuaHangs.ToList();

        public List<NhanVien> GetLstNhanVien() => _dbContext.NhanViens.ToList();


        //public NhanVien GetNVBC()
        //{
        //    var tencv = GetLstChucVu().Where(c=>c.Ten == "TP").Select(c=>c.Ten)
        //}
        //public string removeAscent(string str)
        //{
        //    if (str == null) return str;
        //    str = str.ToLower();
        //    str = str.Replace("/ à | á | ạ | ả | ã | â | ầ | ấ | ậ | ẩ | ẫ | ă | ằ | ắ | ặ | ẳ | ẵ / g", "a");
        //    str = str.Replace("/ è | é | ẹ | ẻ | ẽ | ê | ề | ế | ệ | ể | ễ / g", "e");
        //    str = str.Replace("/ ì | í | ị | ỉ | ĩ / g", "i");
        //    str = str.Replace("/ ò | ó | ọ | ỏ | õ | ô | ồ | ố | ộ | ổ | ỗ | ơ | ờ | ớ | ợ | ở | ỡ / g", "o");
        //    str = str.Replace(/ ù | ú | ụ | ủ | ũ | ư | ừ | ứ | ự | ử | ữ / g", "u");
        //    str = str.Replace(/ ỳ | ý | ỵ | ỷ | ỹ / g, "y");
        //    str = str.Replace(/ đ / g, "d");
        //    return str;
        //}
    }
}
