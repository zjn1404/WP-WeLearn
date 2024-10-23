using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp;

namespace TutorApp.Services.Interfaces
{
    public interface INavigationService
    {
        bool CanGoBack { get; }
        void RegisterPage(string pageKey, Type pageType);
        bool NavigateTo(string pageKey, object parameter = null);
        Window NavigateToNewWindow(string windowKey, string pageKey, object parameter = null);
       
        void SetWindowActive(MainWindow window);
        Frame GetCurrentFrame();

        MainWindow CreateNewWindow(string windowKey);
       



    }
}

