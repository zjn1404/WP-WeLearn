using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System.Collections.Generic;
using System;
using TutorApp.Services.Interfaces;
using TutorApp;
using System.Linq;

public class NavigationService : INavigationService
{
    private readonly Dictionary<string, Type> _pages = new();
    private readonly Dictionary<string, MainWindow> _windows = new();
    private Frame _frame;
    private MainWindow _activeWindow;
    public bool CanGoBack => GetCurrentFrame().CanGoBack;

    public NavigationService(Frame frame)
    {
        _frame = frame ?? throw new ArgumentNullException(nameof(frame));
    }

   
    public void CloseAllWindows(string exceptWindowKey = null)
    {
        var windowsToClose = _windows.ToList();

        foreach (var window in windowsToClose)
        {
            // Bỏ qua cửa sổ được chỉ định (nếu có)
            if (exceptWindowKey != null && window.Key == exceptWindowKey)
                continue;

            try
            {
                // Kiểm tra xem cửa sổ có tồn tại và có thể truy cập được không
                if (window.Value != null)
                {
                    // Thử lấy content để kiểm tra cửa sổ còn hoạt động không
                    var _ = window.Value.Content;

                    // Gửi message để đóng cửa sổ thay vì đóng trực tiếp
                    window.Value.DispatcherQueue.TryEnqueue(() =>
                    {
                        try
                        {
                            window.Value.Close();
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine($"Error closing window: {ex.Message}");
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error handling window: {ex.Message}");
            }
            finally
            {
                // Luôn xóa cửa sổ khỏi dictionary bất kể có lỗi hay không
                _windows.Remove(window.Key);
            }
        }

        // Reset active window nếu nó không còn trong dictionary
        if (_activeWindow != null && !_windows.ContainsValue(_activeWindow))
        {
            _activeWindow = null;
        }
    }

    public MainWindow CreateNewWindow(string windowKey)
    {
        try
        {
            // Đóng các cửa sổ cũ một cách an toàn
            CloseAllWindows();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error closing existing windows: {ex.Message}");
        }

        var window = new MainWindow();
        window.Closed += (sender, args) =>
        {
            if (sender is MainWindow closedWindow)
            {
                try
                {
                    var keyToRemove = _windows.FirstOrDefault(x => x.Value == closedWindow).Key;
                    if (keyToRemove != null)
                    {
                        _windows.Remove(keyToRemove);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error in window closed handler: {ex.Message}");
                }
            }
        };

        _windows.Add(windowKey, window);
        return window;
    }

    public void SetWindowActive(MainWindow window)
    {
        if (window == null)
            throw new ArgumentNullException(nameof(window));
        _activeWindow = window;
        if (!_windows.ContainsValue(window))
        {
            string key = $"Window_{_windows.Count}";
            _windows.Add(key, window);
        }
    }

    public Frame GetCurrentFrame()
    {
        if (_activeWindow != null && _activeWindow.ContentFrame != null)
        {
            return _activeWindow.ContentFrame;
        }
        return _frame;
    }

    public bool NavigateTo(string pageKey, object parameter = null)
    {
        if (!_pages.ContainsKey(pageKey))
            throw new ArgumentException($"Page not found: {pageKey}");
        Frame currentFrame = GetCurrentFrame();
        return currentFrame.Navigate(_pages[pageKey], parameter);
    }

    public Window NavigateToNewWindow(string windowKey, string pageKey, object parameter = null)
    {
        try
        {
            // Đóng các cửa sổ cũ một cách an toàn
            CloseAllWindows(windowKey);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error closing windows: {ex.Message}");
        }

        MainWindow window;
        if (_windows.ContainsKey(windowKey))
        {
            var existingWindow = _windows[windowKey];
            try
            {
                var _ = existingWindow.Content;
                window = existingWindow;
            }
            catch
            {
                _windows.Remove(windowKey);
                window = CreateNewWindow(windowKey);
            }
        }
        else
        {
            window = CreateNewWindow(windowKey);
        }

        try
        {
            window.Activate();
            SetWindowActive(window);
            if (_pages.ContainsKey(pageKey))
            {
                window.ContentFrame.Navigate(_pages[pageKey], parameter);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error navigating to page: {ex.Message}");
            throw;
        }

        return window;
    }

    public void RegisterPage(string pageKey, Type pageType)
    {
        if (!_pages.ContainsKey(pageKey))
        {
            _pages.Add(pageKey, pageType);
        }
    }

    public void GoBack()
    {
        Frame currentFrame = GetCurrentFrame();
        if (currentFrame.CanGoBack)
        {
            currentFrame.GoBack();
        }
    }
}