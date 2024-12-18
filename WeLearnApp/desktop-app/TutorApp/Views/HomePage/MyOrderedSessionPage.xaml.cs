using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using TutorApp.Services;
using TutorApp.Services.Interfaces;
using TutorApp.Services.Interfaces.ForAPI;
using TutorApp.ViewModels;

namespace TutorApp.Views.HomePage
{
    public sealed partial class MyOrderedSessionPage : Page
    {
        public MyOrderedSessionViewModel ViewModel { get; }

        public MyOrderedSessionPage()
        {
            this.InitializeComponent();
            var learningSessionService = App.Current.Services.GetService<ILearningSessionService>();
            var navigationService = App.Current.Services.GetService<INavigationService>();
            ViewModel = new MyOrderedSessionViewModel(learningSessionService);
            this.DataContext = ViewModel;
        }
    }
}