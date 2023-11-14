using ToDoIfy.ViewModels;

namespace ToDoIfy;

public partial class MainPage : ContentPage
{
    private ToDoViewModel _viewModel;

    public MainPage()
	{
		InitializeComponent();
        _viewModel = new ToDoViewModel();
        BindingContext = _viewModel;
    }

    private void OnAddClicked(object sender, EventArgs e)
    {
        var newTaskTitle = NewTaskEntry.Text;
        if (!string.IsNullOrWhiteSpace(newTaskTitle))
        {
            _viewModel.AddTodoItem(newTaskTitle);
            NewTaskEntry.Text = string.Empty;
        }
    }

    private void OnRemoveClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        if (button?.CommandParameter is TodoItem todoItem)
        {
            _viewModel.RemoveTodoItem(todoItem);
        }
    }
}

public class TodoItem
{
	public string Title { get; set; }
    public bool IsDone { get; set; }
}
