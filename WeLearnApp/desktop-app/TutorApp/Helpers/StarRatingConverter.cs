using Microsoft.UI.Xaml.Data;
using System;

namespace TutorApp.Helpers
{
    public class StarRatingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {

            int starCount = (int)value;
            return new string('★', starCount);
        }


        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value; // Not used in this scenario
        }

    }
}
