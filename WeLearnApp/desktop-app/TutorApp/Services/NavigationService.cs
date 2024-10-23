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

        window.Activate();
        SetWindowActive(window);

        if (_pages.ContainsKey(pageKey))
        {
            window.ContentFrame.Navigate(_pages[pageKey], parameter);
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

    public MainWindow CreateNewWindow(string windowKey)
    {
        var window = new MainWindow();

        window.Closed += (sender, args) =>
        {
            if (sender is MainWindow closedWindow)
            {
                var keyToRemove = _windows.FirstOrDefault(x => x.Value == closedWindow).Key;
                if (keyToRemove != null)
                {
                    _windows.Remove(keyToRemove);
                }
            }
        };

        _windows.Add(windowKey, window);
        return window;
    }
}