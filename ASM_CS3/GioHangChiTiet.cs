//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASM_CS3
{
    using System;
    using System.Collections.Generic;
    
    public partial class GioHangChiTiet
    {
        public System.Guid IdGioHang { get; set; }
        public System.Guid IdChiTietSP { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<decimal> DonGia { get; set; }
        public Nullable<decimal> DonGiaKhiGiam { get; set; }
    
        public virtual ChiTietSP ChiTietSP { get; set; }
        public virtual GioHang GioHang { get; set; }
    }
}
