using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TutorApp.Common;
using TutorApp.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;


namespace TutorApp.ViewModels
{
    public abstract class ViewModelBase : ObservableRecipient
    {
        protected readonly INavigationService NavigationService;

        protected ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }
    }
}
