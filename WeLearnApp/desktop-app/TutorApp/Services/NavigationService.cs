using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using TutorApp.Services.Interfaces;

namespace TutorApp.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Dictionary<string, Type> _pages = new();
        private readonly Dictionary<string, Window> _windows = new();
        private Frame _frame;

        public NavigationService(Frame frame)
        {
            _frame = frame ?? throw new ArgumentNullException(nameof(frame));
        }

        public bool CanGoBack => _frame.CanGoBack;

        public void RegisterPage(string pageKey, Type pageType)
        {
            if (!_pages.ContainsKey(pageKey))
            {
                _pages.Add(pageKey, pageType);
            }
        }

        // Thêm method để đăng ký window
        public void RegisterWindow(string windowKey, Window window)
        {
            if (!_windows.ContainsKey(windowKey))
            {
                _windows.Add(windowKey, window);
            }
        }

        // Navigate trong cùng window
        public bool NavigateTo(string pageKey, object parameter = null)
        {
            if (_pages.ContainsKey(pageKey))
            {
                return _frame.Navigate(_pages[pageKey], parameter);
            }
            throw new ArgumentException($"Page not found: {pageKey}");
        }

        // Navigate đến window mới
        public Window NavigateToNewWindow(string windowKey, string pageKey, object parameter = null)
        {
            if (!_windows.ContainsKey(windowKey))
            {
                // Tạo window mới nếu chưa tồn tại
                var newWindow = new MainWindow();
                //var newFrame = new Frame();
                //newWindow.Content = newFrame;
                _windows.Add(windowKey, newWindow);

                if (_pages.ContainsKey(pageKey))
                {
                    newWindow.ContentFrame.Navigate(_pages[pageKey], parameter);
                }

                newWindow.Activate();
                return newWindow;
            }
            else
            {
                // Nếu window đã tồn tại, active nó lên
                var existingWindow = _windows[windowKey];
                if (existingWindow.Content is Frame frame && _pages.ContainsKey(pageKey))
                {
                    frame.Navigate(_pages[pageKey], parameter);
                }
                existingWindow.Activate();
                return existingWindow;
            }
        }

        // Đóng window
        public void CloseWindow(string windowKey)
        {
            if (_windows.ContainsKey(windowKey))
            {
                _windows[windowKey].Close();
                _windows.Remove(windowKey);
            }
        }

        // Kiểm tra window có tồn tại
        public bool WindowExists(string windowKey)
        {
            return _windows.ContainsKey(windowKey);
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

        // Lấy window theo key
        public Window GetWindow(string windowKey)
        {
            return _windows.ContainsKey(windowKey) ? _windows[windowKey] : null;
        }
    }
}