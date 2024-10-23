using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Services.Interfaces
{
    public interface INavigationService
    {
        bool CanGoBack { get; }
        void RegisterPage(string pageKey, Type pageType);
        void RegisterWindow(string windowKey, Window window);
        bool NavigateTo(string pageKey, object parameter = null);
        Window NavigateToNewWindow(string windowKey, string pageKey, object parameter = null);
        void CloseWindow(string windowKey);
        bool WindowExists(string windowKey);
        bool GoBack();
        Window GetWindow(string windowKey);
    }
}
