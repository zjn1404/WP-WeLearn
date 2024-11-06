using Microsoft.UI.Xaml.Controls;

using Microsoft.UI.Xaml;

/// <summary>
/// Interface for objects that are aware of navigation events.
/// Implementing this interface allows an object to respond to being navigated to or from.
/// </summary>
public interface INavigationAware
{
    /// <summary>
    /// Called when the page is navigated to.
    /// </summary>
    /// <param name="parameter">An optional parameter passed during navigation.</param>
    void OnNavigatedTo(object parameter);

    /// <summary>
    /// Called when the page is navigated from.
    /// </summary>
    void OnNavigatedFrom();
}

/// <summary>
/// A base class for pages that need to be aware of navigation events.
/// This class handles the <see cref="OnNavigatedTo"/> and <see cref="OnNavigatedFrom"/> methods for navigation-aware pages.
/// </summary>
public partial class NavigationAwarePage : Page
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NavigationAwarePage"/> class.
    /// Subscribes to the Loaded and Unloaded events for handling navigation.
    /// </summary>
    public NavigationAwarePage()
    {
        this.Loaded += NavigationAwarePage_Loaded;
        this.Unloaded += NavigationAwarePage_Unloaded;
    }

    /// <summary>
    /// Handles the Loaded event of the page. This method is called when the page is fully loaded.
    /// It checks if the DataContext implements <see cref="INavigationAware"/> and calls <see cref="OnNavigatedTo"/>.
    /// </summary>
    private void NavigationAwarePage_Loaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is INavigationAware navigationAware)
        {
            navigationAware.OnNavigatedTo(this.NavigationParameter());
        }
    }

    /// <summary>
    /// Handles the Unloaded event of the page. This method is called when the page is unloaded.
    /// It checks if the DataContext implements <see cref="INavigationAware"/> and calls <see cref="OnNavigatedFrom"/>.
    /// </summary>
    private void NavigationAwarePage_Unloaded(object sender, RoutedEventArgs e)
    {
        if (DataContext is INavigationAware navigationAware)
        {
            navigationAware.OnNavigatedFrom();
        }
    }

    /// <summary>
    /// Retrieves the navigation parameter passed to the page.
    /// </summary>
    /// <returns>The navigation parameter, or null if no parameter is available.</returns>
    private object NavigationParameter()
    {
        if (this.Frame?.GetNavigationState() != null)
        {
            return Frame.GetValue(Frame.TagProperty);
        }
        return null;
    }
}
