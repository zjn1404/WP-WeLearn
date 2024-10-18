using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Services.Interfaces;

namespace TutorApp.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Type> _pages = new();
        private Frame _frame;

        public NavigationService(Frame frame)
        {
            _frame = frame;
        }

        public bool CanGoBack => _frame.CanGoBack;

        public void RegisterPage(string pageKey, Type pageType)
        {
            if (!_pages.ContainsKey(pageKey))
            {
                _pages.Add(pageKey, pageType);
            }
        }

        public bool NavigateTo(string pageKey, object parameter = null)
        {
            if (_pages.ContainsKey(pageKey))
            {
                return _frame.Navigate(_pages[pageKey], parameter);
            }
            throw new ArgumentException($"Page not found: {pageKey}");
        }

        public bool GoBack()
        {
            if (_frame.CanGoBack)
            {
                _frame.GoBack();
                return true; 
            }
            return false;
        }
    }

}
