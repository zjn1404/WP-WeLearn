using Microsoft.UI.Xaml.Data;
using System;
using System.Diagnostics;

namespace TutorApp.Helpers
{
    public class DateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                if (value is DateTime dateTime)
                {
                    return dateTime.ToString("MM/dd/yyyy hh:mm tt");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DateTime conversion error: {ex.Message}");
            }

            return "Invalid Date";
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (DateTime.TryParse(value?.ToString(), out DateTime result))
            {
                return result;
            }
            return DateTime.Now; 
        }
    }
}
