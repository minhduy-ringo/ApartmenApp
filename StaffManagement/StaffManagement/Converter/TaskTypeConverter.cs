using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace StaffManagement.Converter
{
    class TaskTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (System.Convert.ToInt32(value))
            {
                case 1: return ("Sửa chữa");
                case 2: return ("Bảo trì");
                case 3: return ("Vận chuyển");
                case 4: return ("Khác");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}