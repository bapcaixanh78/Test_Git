using System;
using System.Collections.Generic;
using System.Text;

namespace ASM1.Utilities
{
    public class Utility
    {
        public string GetHo(string fullname)
        {
            string[] arr = fullname.Split(" ");
            return arr[0];
        }

        public string GetTen(string fullname)
        {
            string[] arr = fullname.Split(" ");
            return arr[arr.Length - 1];
        }

        public string GetTenDem(string fullname)
        {
            string[] arr = fullname.Split(" ");
            string td = "";
            foreach(string item in arr)
            {
                if(item == GetHo(fullname) || item == GetTen(fullname))
                {
                    continue;
                }
                td+=item;
            }
            return td;
        }

        private string GetChuCaiDau(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            return Convert.ToString(value[0]).ToUpper(); 
        }

        private string VietHoaChuCaiDau(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            return Convert.ToString(value[0]).ToUpper() + value.Substring(1);
        }

        public string ZenID_Auto(string name)
        {
            int ma;
            Random r = new Random();
            ma = Convert.ToInt32(r.Next(1, 50000));
            string temp = name.Trim().ToLower();
            string[] arrvalue = temp.Split(' ');//Cắt chuỗi thành mảng
            string final = VietHoaChuCaiDau(arrvalue[arrvalue.Length - 1].Trim()); //Khải
            for (int i = 0; i < arrvalue.Length - 1; i++)
            {
                final += GetChuCaiDau(arrvalue[i]);
            }
            return LoaiBoDauTiengViet(final) + ma;
        }

        public string LoaiBoDauTiengViet(string str)
        {
            string strFormD = str.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < strFormD.Length; i++)
            {
                System.Globalization.UnicodeCategory uc =
                System.Globalization.CharUnicodeInfo.GetUnicodeCategory(strFormD[i]);
                if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(strFormD[i]);
                }
            }
            sb = sb.Replace('Đ', 'D');
            sb = sb.Replace('đ', 'd');
            return (sb.ToString().Normalize(NormalizationForm.FormD));
        }
    }
}
