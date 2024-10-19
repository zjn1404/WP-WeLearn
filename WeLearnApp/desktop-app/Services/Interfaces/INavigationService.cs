using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Services.Interfaces
{
    public interface INavigationService
    {
        bool NavigateTo(string pageKey, object parameter = null);
        bool GoBack();
        void RegisterPage(string pageKey, Type pageType);
        bool CanGoBack { get; }
    }
}
