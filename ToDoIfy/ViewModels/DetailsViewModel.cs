using CommunityToolkit.Mvvm.ComponentModel;

namespace ToDoIfy.ViewModels
{
    [QueryProperty("Text", "Text")]
	public partial class DetailsViewModel : ObservableObject
	{
        [ObservableProperty]
        string text;

        async void GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        public Command GoBackCommand => new Command(GoBack);
    }
}

