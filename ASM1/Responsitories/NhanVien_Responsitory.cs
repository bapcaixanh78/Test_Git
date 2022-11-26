using ASM1.Context;
using ASM1.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASM1.Responsitories
{
    public class NhanVien_Responsitory
    {
        FpolyDBContext _dbcontext;
        public NhanVien_Responsitory()
        {
            _dbcontext = new FpolyDBContext();
        }

        #region Get Data 
        public List<NhanVien> GetLstNhanVien() => _dbcontext.NhanViens.ToList();

        public List<ChucVu> GetLstChucVu() => _dbcontext.ChucVus.ToList();

        public List<CuaHang> GetLstCuaHang() => _dbcontext.CuaHangs.ToList();

        #endregion

        #region Methods cơ bản
        public bool Add(NhanVien nv)
        {
            if(nv == null)
            {
                return false;
            }
            nv.Id = Guid.NewGuid(); //Đã khởi tạo id của nhân viên
            _dbcontext.NhanViens.Add(nv);
            _dbcontext.SaveChanges(); return true;
        }

        public bool Update(NhanVien nv)
        {
            if (nv == null)
            {
                return false;
            }
            _dbcontext.NhanViens.Update(nv);
            _dbcontext.SaveChanges(); return true;
        }

        public bool Delete(NhanVien nv)
        {
            if (nv == null)
            {
                return false;
            }
            _dbcontext.NhanViens.Remove(nv);
            _dbcontext.SaveChanges(); return true;
        }
        #endregion  
    }
}
