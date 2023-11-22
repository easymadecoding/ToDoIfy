using ToDoIfy.ViewModels;

namespace ToDoIfy;

public partial class DetailsPage : ContentPage
{
    private DetailsViewModel _viewModel;

    public DetailsPage()
    {
        InitializeComponent();
        _viewModel = new DetailsViewModel();
        BindingContext = _viewModel;
    }
}
