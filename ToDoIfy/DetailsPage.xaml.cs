using ToDoIfy.ViewModels;

namespace ToDoIfy;

public partial class DetailsPage : ContentPage
{
    public DetailsPage(DetailsViewModel detailsViewModel)
    {
        InitializeComponent();
        BindingContext = detailsViewModel;
    }
}
