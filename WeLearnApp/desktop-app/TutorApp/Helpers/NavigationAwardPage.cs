using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorApp.Helpers
{

    public interface INavigationAware
    {
        void OnNavigatedTo(object parameter);
        void OnNavigatedFrom();
    }


    public partial class NavigationAwarePage : Page
    {
        public NavigationAwarePage()
        {
            this.Loaded += NavigationAwarePage_Loaded;
            this.Unloaded += NavigationAwarePage_Unloaded;
        }

        private void NavigationAwarePage_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedTo(this.NavigationParameter());
            }
        }

        private void NavigationAwarePage_Unloaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is INavigationAware navigationAware)
            {
                navigationAware.OnNavigatedFrom();
            }
        }

        private object NavigationParameter()
        {
            if (this.Frame?.GetNavigationState() != null)
            {
                return Frame.GetValue(Frame.TagProperty);
            }
            return null;
        }
    }
}
