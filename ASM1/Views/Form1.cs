using ASM1.Context;
using ASM1.DomainModels;
using ASM1.Responsitories;
using ASM1.Services;
using ASM1.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ASM1
{
    public partial class Form1 : Form
    {
        Guid _idWhenClick;
        //Scaffold-DbContext 'Data Source=.\sqlexpress;Initial Catalog=FINALASS_FPOLYSHOP_SM22_BL2_DUNGNA29;Persist Security Info=True;User ID=khaiphamdinh78;Password=123' Microsoft.EntityFrameworkCore.SqlServer -OutputDir DomainModels -context FpolyDBContext -Contextdir Context -DataAnnotations -Force

        Nhanvien_Service _NvService;
        FpolyDBContext _DbContext;
        NhanVien_Responsitory _NvRespon;
        List<NhanVien> _lstNV;
        List<CuaHang> _lstCH;
        List<ChucVu> _lstCV;
        Utility _ul;
        public Form1()
        {
            InitializeComponent();
            _NvService = new Nhanvien_Service();
            _DbContext = new FpolyDBContext();
            _NvRespon = new NhanVien_Responsitory();
            txt_Ma.Enabled = false;
            rdo_Nam.Checked = true;
            _NvService.GetDataFromDB();
            LoadChucVu();
            LoadCH();
            LoadDTG(null);
            LoadNguoiGuiBC();
            LocCH();
            LocChucVu();
            _lstCH = new List<CuaHang>();
            _lstNV = new List<NhanVien>();
            _lstCV = new List<ChucVu>();
            _ul = new Utility();
        }
        void LoadChucVu()
        {
            foreach (var cv in _NvService.GetLstChucVu())
            {
                cb_Chucvu.Items.Add(cv.Ma + " - " + cv.Ten);
            }
            cb_Chucvu.SelectedIndex = 0;
        }

        void LocChucVu()
        {
            foreach (var cv in _NvService.GetLstChucVu())
            {
                cb_LocChucVu.Items.Add(cv.Ten);
            }
        }

        void LocCH()
        {
            foreach (var ch in _NvService.GetLstCuaHang())
            {
                cb_LocMaCuaHang.Items.Add(ch.Ten);
            }
        }
        void LoadCH()
        {
            foreach (var ch in _NvService.GetLstCuaHang())
            {
                cb_CH.Items.Add(ch.Ma + " - " + ch.Ten);
            }
            cb_CH.SelectedIndex = 0;
        }

        void LoadNguoiGuiBC()
        {
            foreach (var bc in _NvService.GetNguoiGuiBaoCao())
            {
                cb_Baocao.Items.Add(_NvService.GetMaTruongPhong() + " - " + bc.Ho + " " + bc.TenDem + " " + bc.Ten);
            }
            cb_Baocao.SelectedIndex = 0;
        }

        public NhanVien Get1NV(NhanVien nv)
        {
            NhanVien tmp = _NvService.GetLstNhanVien().FirstOrDefault(c => c.Id == nv.IdGuiBc);
            return tmp;
        }

        void LoadDTG(string input)
        {
            int stt = 1;
            Type type = typeof(NhanVien);
            int slThuoctinh = type.GetProperties().Length;
            dtg_Data.ColumnCount = slThuoctinh - 6;
            dtg_Data.Columns[0].Name = "STT";
            dtg_Data.Columns[1].Name = "ID";
            dtg_Data.Columns[2].Name = "Mã";
            dtg_Data.Columns[3].Name = "Họ và tên";
            dtg_Data.Columns[4].Name = "Giới tính";
            dtg_Data.Columns[5].Name = "Ngày sinh";
            dtg_Data.Columns[6].Name = "Địa chỉ";
            dtg_Data.Columns[7].Name = "SDT";
            dtg_Data.Columns[8].Name = "Mật khẩu";
            dtg_Data.Columns[9].Name = "Tên cửa hàng";
            dtg_Data.Columns[10].Name = "Tên chức vụ";
            dtg_Data.Columns[11].Name = "Tên người gửi";
            dtg_Data.Columns[12].Name = "Trạng thái";
            dtg_Data.Rows.Clear();


            foreach (var nv in _NvService.GetLstNV(input))
            {
                var tencv = "";
                var tenngbaocao = "";
                var CH = "";
                if (!string.IsNullOrEmpty(nv.IdCv.ToString()))
                {
                    tencv = _NvService.GetLstChucVu().FirstOrDefault(c => c.Id == nv.IdCv).Ten;
                }
                if (!string.IsNullOrEmpty(nv.IdGuiBc.ToString()))
                {
                    tenngbaocao = Get1NV(nv).Ma + "-" + Get1NV(nv).Ho + " " + Get1NV(nv).TenDem + " " + Get1NV(nv).Ten;
                }
                if (!string.IsNullOrEmpty(nv.IdCh.ToString()))
                {
                    CH = _NvService.GetLstCuaHang().FirstOrDefault(c => c.Id == nv.IdCh).Ten;
                }
                dtg_Data.Rows.Add(stt++, nv.Id, nv.Ma, nv.Ho + " " + nv.TenDem + " " + nv.Ten, nv.GioiTinh, nv.NgaySinh, nv.DiaChi, nv.Sdt, nv.MatKhau, CH, tencv,
                   tenngbaocao, nv.TrangThai);
            }
        }

        void LoadDTG(string input, List<NhanVien> lst)
        {
            int stt = 1;
            Type type = typeof(NhanVien);
            int slThuoctinh = type.GetProperties().Length;
            dtg_Data.ColumnCount = slThuoctinh - 6;
            dtg_Data.Columns[0].Name = "STT";
            dtg_Data.Columns[1].Name = "ID";
            dtg_Data.Columns[2].Name = "Mã";
            dtg_Data.Columns[3].Name = "Họ và tên";
            dtg_Data.Columns[4].Name = "Giới tính";
            dtg_Data.Columns[5].Name = "Ngày sinh";
            dtg_Data.Columns[6].Name = "Địa chỉ";
            dtg_Data.Columns[7].Name = "SDT";
            dtg_Data.Columns[8].Name = "Mật khẩu";
            dtg_Data.Columns[9].Name = "Tên cửa hàng";
            dtg_Data.Columns[10].Name = "Tên chức vụ";
            dtg_Data.Columns[11].Name = "Tên người gửi";
            dtg_Data.Columns[12].Name = "Trạng thái";
            dtg_Data.Rows.Clear();


            foreach (var nv in lst)
            {
                var tencv = "";
                var tenngbaocao = "";
                var CH = "";
                if (!string.IsNullOrEmpty(nv.IdCv.ToString()))
                {
                    tencv = _NvService.GetLstChucVu().FirstOrDefault(c => c.Id == nv.IdCv).Ten;
                }
                if (!string.IsNullOrEmpty(nv.IdGuiBc.ToString()))
                {
                    tenngbaocao = Get1NV(nv).Ma + "-" + Get1NV(nv).Ho + " " + Get1NV(nv).TenDem + " " + Get1NV(nv).Ten;
                }
                if (!string.IsNullOrEmpty(nv.IdCh.ToString()))
                {
                    CH = _NvService.GetLstCuaHang().FirstOrDefault(c => c.Id == nv.IdCh).Ten;
                }
                dtg_Data.Rows.Add(stt++, nv.Id, nv.Ma, nv.Ho + " " + nv.TenDem + " " + nv.Ten, nv.GioiTinh, nv.NgaySinh, nv.DiaChi, nv.Sdt, nv.MatKhau, CH, tencv,
                   tenngbaocao, nv.TrangThai);
            }
        }

        public NhanVien GetNhanVienFromGUI() => new NhanVien
        {
            Id = Guid.Empty,
            Ma = txt_Ma.Text,
            Ho = txt_Ho.Text,
            TenDem = txt_Tendem.Text,
            Ten = txt_Ten.Text,
            GioiTinh = (rdo_Nam.Checked ? "Nam" : "Nữ"),
            DiaChi = txt_Diachi.Text,
            Sdt = txt_Sdt.Text,
            NgaySinh = dtpk_Ngaysinh.Value,
            IdCv = _NvService.GetLstChucVu()[cb_Chucvu.SelectedIndex].Id,
            IdCh = _NvService.GetLstCuaHang()[cb_CH.SelectedIndex].Id,
            IdGuiBc = _NvService.GetNguoiGuiBaoCao()[cb_Baocao.SelectedIndex].Id,
            MatKhau = txt_Pass.Text
        };

        private void btn_Them_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Bạn có chắc chắn muốn thêm nhân viên này?", "Thông báo",MessageBoxButtons.OKCancel);
            if (confirm == DialogResult.OK)
            {
                if (!RegexHoten(_ul.LoaiBoDauTiengViet(txt_Ho.Text)))
                {
                    MessageBox.Show("Họ, tên đệm, tên không được chứa số hay kí tự đặc biệt", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                if (!RegexHoten(_ul.LoaiBoDauTiengViet(txt_Tendem.Text)))
                {
                    MessageBox.Show("Họ, tên đệm, tên không được chứa số hay kí tự đặc biệt", "Thông báo", MessageBoxButtons.OK);
                    return;

                }
                if (!RegexHoten(_ul.LoaiBoDauTiengViet(txt_Ten.Text)))
                {
                    MessageBox.Show("Họ, tên đệm, tên không được chứa số hay kí tự đặc biệt", "Thông báo", MessageBoxButtons.OK);
                    return;

                }
                string regexSDT = "^0[1-9]{1}\\d{8}$";
                if (txt_Diachi.Text == "")
                {
                    MessageBox.Show("Không thể thêm do địa chỉ bị rỗng", "Thông báo", MessageBoxButtons.OK); return;
                }
                else if (!Regex.IsMatch(txt_Sdt.Text, regexSDT))
                {
                    MessageBox.Show("Không thể thêm do số điện thoại sai định dạng", "Thông báo", MessageBoxButtons.OK); return;
                }
                else if (_NvService.GetLstSDTNhanvien().Contains(txt_Sdt.Text))
                {
                    MessageBox.Show("Không thể thêm do số điện thoại bị trùng","Thông báo", MessageBoxButtons.OK); return;
                }
                else if (txt_Pass.Text.Length <= 3)
                {
                    MessageBox.Show("Mật khẩu phải có nhiều hơn 3 kí tự", "Thông báo", MessageBoxButtons.OK); return;
                }
                MessageBox.Show(_NvService.AddNV(GetNhanVienFromGUI()));
                LoadDTG(null);
            }
            else return;
        }
        
        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Thông báo", MessageBoxButtons.OKCancel);
            if (confirm == DialogResult.OK)
            {
                MessageBox.Show(_NvService.DeleteNV(_idWhenClick), "Thông báo",MessageBoxButtons.OK);
                LoadDTG(null);
            }
            else return;
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Bạn có chắc chắn muốn sửa nhân viên này?", "Thông báo", MessageBoxButtons.OKCancel);
            if (confirm == DialogResult.OK)
            {
                if (RegexHoten(txt_Ho.Text))
                {
                    MessageBox.Show("Họ, tên đệm, tên không được chứa số hay kí tự đặc biệt", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                else if (RegexHoten(txt_Tendem.Text))
                {
                    MessageBox.Show("Họ, tên đệm, tên không được chứa số hay kí tự đặc biệt", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                else if (RegexHoten(txt_Ten.Text))
                {
                    MessageBox.Show("Họ, tên đệm, tên không được chứa số hay kí tự đặc biệt", "Thông báo", MessageBoxButtons.OK);
                    return;
                }
                string regexSDT = "^0[1-9]{1}\\d{8}$";
                if (!Regex.IsMatch(txt_Sdt.Text, regexSDT))
                {
                    MessageBox.Show("Không thể sửa do sai định dạng số điện thoại", "Thông báo", MessageBoxButtons.OK); return;
                }
                else if (_NvService.GetLstSDTNhanvien().Contains(txt_Sdt.Text))
                {
                    MessageBox.Show("Không thể sửa do số điện thoại bị trùng", "Thông báo", MessageBoxButtons.OK); return;
                }
                var nvtmp = GetNhanVienFromGUI();
                nvtmp.Id = _idWhenClick;
                MessageBox.Show(_NvService.UpdateNV(_idWhenClick, nvtmp), "Thông báo",MessageBoxButtons.OK);
                LoadDTG(null);
            }
        }

        public bool RegexHoten(string cancheck)
        {
            _ul.LoaiBoDauTiengViet(cancheck);
            string reg = "^[a-zA-Z]*$";
            if (Regex.IsMatch(cancheck, reg))
            {
                return true;
            }return false;
        }
        private void dtg_Data_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = e.RowIndex;
            if (rowindex == _NvService.GetLstNV(null).Count || rowindex == -1) return;
            _idWhenClick = Guid.Parse(dtg_Data.Rows[rowindex].Cells[1].Value.ToString());

            var cv = _NvService.GetLstNV(null).FirstOrDefault(c => c.Id == _idWhenClick);
            txt_Ma.Text = dtg_Data.Rows[rowindex].Cells[2].Value.ToString();
            txt_Ho.Text = _ul.GetHo(dtg_Data.Rows[rowindex].Cells[3].Value.ToString());
            txt_Ten.Text = _ul.GetTen(dtg_Data.Rows[rowindex].Cells[3].Value.ToString());
            txt_Tendem.Text = _ul.GetTenDem(dtg_Data.Rows[rowindex].Cells[3].Value.ToString());
            if (dtg_Data.Rows[rowindex].Cells[4].Value.ToString().ToLower() == "nữ")
            {
                rdo_Nu.Checked = true;
            }
            dtpk_Ngaysinh.Value = Convert.ToDateTime(dtg_Data.Rows[rowindex].Cells[5].Value);
            txt_Sdt.Text = dtg_Data.Rows[rowindex].Cells[7].Value.ToString();
            txt_Diachi.Text = dtg_Data.Rows[rowindex].Cells[6].Value.ToString();
            txt_Pass.Text = dtg_Data.Rows[rowindex].Cells[8].Value.ToString();
            cb_Chucvu.SelectedIndex = _NvService.GetLstChucVu().FindIndex(c => c.Id == cv.IdCv);
            cb_Baocao.SelectedIndex = _NvService.GetLstTruongPhong().FindIndex(c => c.Id == cv.IdGuiBc);
            cb_CH.SelectedIndex = _NvService.GetLstCuaHang().FindIndex(c => c.Id == cv.IdCh);
            //txt_Tendem.Text = dtg_Data.Rows[rowindex].Cells[4].Value.ToString();
            //txt_Tendem.Text = dtg_Data.Rows[rowindex].Cells[4].Value.ToString();
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_Ma.Text = null;
            txt_Ho.Text = null;
            txt_Tendem.Text = null;
            txt_Ten.Text = null;
            txt_Diachi.Text = null;
            txt_Sdt.Text = null;
            dtpk_Ngaysinh.Value = new DateTime(1900, 01, 01);
            txt_Pass.Text = null;
            LoadDTG(null);
            MessageBox.Show("Đã clear!", "Thông báo");
        }

        private void txt_Ten_TextChanged(object sender, EventArgs e)
        {
            txt_Ma.Text = _ul.ZenID_Auto(GetFullName());
        }

        private void cb_LocChucVu_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void cb_LocChucVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDTG(cb_LocChucVu.Text, _NvService.LocNhanVienByChucVu(cb_LocChucVu.Text));
        }

        private void cb_LocMaCuaHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDTG(cb_LocMaCuaHang.Text, _NvService.LocNhanVienByCH(cb_LocMaCuaHang.Text));
        }

        public string GetFullName() => txt_Ho.Text + " " + txt_Tendem.Text + " " + txt_Ten.Text;

        private void txt_Search_Click(object sender, EventArgs e)
        {
            txt_Search.Text = null;
        }

        private void txt_Search_Leave(object sender, EventArgs e)
        {
            txt_Search.Text = "Tìm kiếm ở đây...";
        }

        private void txt_Search_MouseLeave(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadDTG(txt_Search.Text);
        }
    }
}
