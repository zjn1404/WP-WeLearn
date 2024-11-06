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

    /// <summary>
    /// This interface defines methods related to navigate to different page
    /// </summary>
    public interface INavigationService
    {

        /// <summary>
        ///     Gets a value indicating whether it is possible to go back.
        /// </summary>
        /// 
        /// <value>
        ///      Returns true if it is possible to go back; otherwise, false.
        /// </value>
        /// 
        bool CanGoBack { get; }


        /// <summary>
        /// Registers a page with the specified key and type.
        /// </summary>
        /// <param name="pageKey">A unique key for the page to be registered.</param>
        /// <param name="pageType">The type of the page to be registered.</param>
        void RegisterPage(string pageKey, Type pageType);


        /// <summary>
        /// Navigates to a page with the specified key and parameter for delivering data between pages.
        /// </summary>
        /// <param name="pageKey">The key of the page to navigate to, which was registered using the RegisterPage method.</param>
        /// <param name="parameter">An object containing data to be delivered to the page associated with the given pageKey.</param>
        /// <returns>
        /// Returns true if the navigation was successful; otherwise, false.
        /// </returns>
        bool NavigateTo(string pageKey, object parameter = null);



        /// <summary>
        /// Navigates to a new window, closes any existing windows with the same key, and navigates to the specified page with the provided parameter.
        /// </summary>
        /// <param name="windowKey">The key for the window being navigated to. This is used to identify the window to close and reopen if necessary.</param>
        /// <param name="pageKey">The key for the page to navigate to within the window. The page must be registered with the system.</param>
        /// <param name="parameter">Optional parameter to pass data to the page being navigated to. Default is null.</param>
        /// <returns>
        /// Returns the window that was navigated to.
        /// </returns>
        Window NavigateToNewWindow(string windowKey, string pageKey, object parameter = null);


        /// <summary>
        /// Sets the specified window as the active window and adds it to the collection of windows if it isn't already in the collection.
        /// </summary>
        /// <param name="window">The main window to be set as active. This window will also be added to the collection of windows if not already present.</param>
        void SetWindowActive(MainWindow window);

        /// <summary>
        ///    Gets current frame
        /// </summary>
        /// <returns>return current frame</returns>
        Frame GetCurrentFrame();


        /// <summary>
        /// Closes all windows except the one identified by the specified window key.
        /// </summary>
        /// <param name="exceptWindowKey">The key of the window that should not be closed. If no window should be excluded, pass null (default).</param>
        void CloseAllWindows(string exceptWindowKey = null);


        /// <summary>
        /// Creates a new window with the specified key and adds it to the collection of windows.
        /// Closes all existing windows before creating the new one.
        /// </summary>
        /// <param name="windowKey">The key associated with the new window to be created and added to the window collection.</param>
        /// <returns>
        /// Returns the newly created window.
        /// </returns>
        MainWindow CreateNewWindow(string windowKey);


        /// <summary>
        /// Navigates back to the nearest previous page in the navigation stack.
        /// </summary>
        public void GoBack();

    }
}

