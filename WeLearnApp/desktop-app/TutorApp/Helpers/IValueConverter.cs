using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Helpers
{
    public interface IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language);
        public object ConvertBack(object value, Type targetType, object parameter, string language);
    }
}
