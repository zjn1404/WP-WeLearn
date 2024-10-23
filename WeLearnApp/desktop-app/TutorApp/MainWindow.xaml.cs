using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TutorApp.Services.Interfaces;
using TutorApp.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using TutorApp.Helpers;
using Microsoft.Extensions.DependencyInjection;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace TutorApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public event EventHandler Closed;

        public Frame ContentFrame { get; private set; }
        public bool IsWindowActive { get; set; }
        private readonly INavigationService _navigationService;

        public MainWindow()
        {
            this.InitializeComponent();
            ContentFrame = new Frame();
            this.Content = ContentFrame;

            // Đăng ký sự kiện khi window bị đóng
            this.Closed += (s, e) => Closed?.Invoke(this, e);
        }
    }
}
