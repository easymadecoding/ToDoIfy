using CommunityToolkit.Mvvm.ComponentModel;

namespace ToDoIfy.ViewModels
{
    [QueryProperty("Text", "Text")]
    [QueryProperty("Deadline", "Deadline")]
    public partial class DetailsViewModel : ObservableObject
	{
        [ObservableProperty]
        string text;

        [ObservableProperty]
        string deadline;

        async void GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        public Command GoBackCommand => new Command(GoBack);
    }
}

