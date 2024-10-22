using Microsoft.UI.Xaml.Data;
using System;

namespace TutorApp.Helpers
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime dateTime)
            {

                
                // Trả về chuỗi định dạng "dd/MM/yyyy"
                return dateTime.ToString("dd/MM/yyyy");
            }
            return "Invalid Date";  // Trả về chuỗi mặc định nếu không có giá trị
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (DateTime.TryParse(value?.ToString(), out DateTime result))
            {
                return result;
            }
            return DateTime.Now; // Trả về giá trị mặc định
        }
    }
}
