using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;

namespace TutorApp.Controls
{
    public sealed partial class TutorFilterControl : UserControl
    {
        public event EventHandler<FilterChangedEventArgs> FilterChanged;

        public TutorFilterControl()
        {
            this.InitializeComponent();
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RaiseFilterChangedEvent();
        }

        private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                RaiseFilterChangedEvent();
            }
        }

        private void ClearFilters_Click(object sender, RoutedEventArgs e)
        {
            LocationFilter.SelectedIndex = 0;
            SubjectFilter.SelectedIndex = 0;
            GradeFilter.SelectedIndex = 0;
            TuitionFilter.SelectedIndex = 0;
            RatingFilter.SelectedIndex = 0;

            RaiseFilterChangedEvent();
        }

        private void RaiseFilterChangedEvent()
        {
            var filters = new FilterCriteria
            {
                Location = ((ComboBoxItem)LocationFilter.SelectedItem)?.Content.ToString(),
                Subject = ((ComboBoxItem)SubjectFilter.SelectedItem)?.Content.ToString(),
                Grade = ((ComboBoxItem)GradeFilter.SelectedItem)?.Content.ToString(),
                TuitionRange = ((ComboBoxItem)TuitionFilter.SelectedItem)?.Content.ToString(),
                Rating = ((ComboBoxItem)RatingFilter.SelectedItem)?.Content.ToString(),
                SearchText = SearchBox.Text
            };

            FilterChanged?.Invoke(this, new FilterChangedEventArgs(filters));
        }
    }

    public class FilterChangedEventArgs : EventArgs
    {
        public FilterCriteria Filters { get; }

        public FilterChangedEventArgs(FilterCriteria filters)
        {
            Filters = filters;
        }
    }

    public class FilterCriteria
    {
        public string Location { get; set; }
        public string Subject { get; set; }
        public string Grade { get; set; }
        public string TuitionRange { get; set; }
        public string Rating { get; set; }
        public string SearchText { get; set; }
    }
}