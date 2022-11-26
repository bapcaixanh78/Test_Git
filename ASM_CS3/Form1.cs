using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ASM_CS3.QLNV_Services;

namespace ASM_CS3
{
    public partial class Form1 : Form
    {
        FINALASS_FPOLYSHOP_SM22_BL2_DUNGNA29_Entities _ETT = new FINALASS_FPOLYSHOP_SM22_BL2_DUNGNA29_Entities();
        QLNV_Service _sv = new QLNV_Service();
        public Form1()
        {
            InitializeComponent();
            LoadData();
            rdo_Nam.Checked = true;
            LoadCH();
            LoadDataChucVu();
        }
        #region Methods

        List<string> GetTenChucVu()
        {
            List<string> lst = new List<string>();
            foreach(var x in _ETT.ChucVu.Select(c => c.Ten).ToList())
            {
                lst.Add(x);
            }
            return lst;
        }

        List<Guid> GetIDBaoCao()
        {
            List<Guid> lst = new List<Guid>();
            foreach (var x in _ETT.NhanVien.Select(c => c.IdGuiBC).ToList())
            {
                lst.Add((Guid)x);
            }
            return lst;
        }

        void LoadIDBaoCao()
        {
            foreach(var x in GetIDBaoCao())
            {
                cb_Baocao.Items.Add(x);
            }
            cb_Baocao.SelectedIndex = 0;
        }


        void LoadDataChucVu()
        {
            foreach(var x in GetTenChucVu())
            {
                cb_Chucvu.Items.Add(x);
            }
            cb_CH.SelectedIndex = 0;
        }

        void LoadData()
        {
            dtg_Data.DataSource = _ETT.NhanVien.ToList();
        }

        List<string> GetMaCH()
        {
            List<string> list = new List<string>();
            foreach(var x in _ETT.CuaHang.Select(c => c.Ma).ToList())
            {
                list.Add(x);
            }
            return list;
        }
        void LoadCH()
        {
            foreach(string x in GetMaCH())
            {
                cb_CH.Items.Add(x);
            }
            //cb_CH.SelectedIndex = 0;
        }

        NhanVien GetDataFromGUI()
        {
            return new NhanVien { Id = Guid.Empty, Ma = txt_Ma.Text, Ten = txt_Ten.Text, TenDem = txt_TenDem.Text, Ho = txt_Ho.Text, GioiTinh = rdo_Nam.Checked ? "Nam" : "Nữ", NgaySinh = dateTimePicker1.Value, DiaChi = txt_Diachi.Text, Sdt = txt_SDT.Text, MatKhau = txt_Pass.Text, IdCH = Guid.Empty, IdCV = Guid.Empty, IdGuiBC = Guid.Empty, TrangThai = 1};
        }

        void Del()
        {

        }

        void Update()
        {

        }

        void Clear()
        {

        }
        #endregion

        #region Events
        private void btn_Them_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_sv.Add(GetDataFromGUI()));
            LoadData();
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {

        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {

        }

        private void btn_clear_Click(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
