using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace StaffManagement.Converter
{
    class TaskStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (System.Convert.ToInt32(value))
            {
                case 1: return ("Tiếp nhận");
                case 2: return ("Tiến hành");
                case 3: return ("Chờ xác nhận");
                case 4: return ("Hoàn thành");
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
