using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM_CS3.QLNV_Services
{
    public class QLNV_Service
    {
        private List<NhanVien> lstnv;

        public QLNV_Service()
        {
            lstnv = new List<NhanVien>();
        }

        public string Add(NhanVien nv)
        {
            nv.Id = Guid.NewGuid();
            nv.IdCH = Guid.NewGuid();
            nv.IdCV = Guid.NewGuid();
            lstnv.Add(nv);
            return "Thêm thành công";
        }
    }
}
