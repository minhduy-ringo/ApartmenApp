using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace StaffManagement.Converter
{
    public class DepartmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (System.Convert.ToInt32(value))
            { 
                case 1: return ("Bảo vệ");
                case 2: return ("Dịch vụ");
                case 3: return ("Nhân viên bảo trì");
                case 4: return ("Nhân viên cộng đồng");
                case 5: return ("Vận chuyển");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
