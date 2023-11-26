using ToDoIfy.ViewModels;

namespace ToDoIfy;

public partial class MainPage : ContentPage
{
    private ToDoViewModel _viewModel;

    public MainPage(ToDoViewModel toDoViewModel)
	{
		InitializeComponent();
        _viewModel = toDoViewModel;
        BindingContext = _viewModel;
    }

    private void OnAddClicked(object sender, EventArgs e)
    {
        var newTaskTitle = NewTaskEntry.Text;
        var newTaskTimeDeadline = TimeTaskEntry.Time;
        var newTaskDayDeadline = DayTaskEntry.Date;

        var taskDeadline = newTaskDayDeadline.Add(newTaskTimeDeadline);

        if (!string.IsNullOrWhiteSpace(newTaskTitle))
        {
            _viewModel.AddTodoItem(newTaskTitle, taskDeadline);
            NewTaskEntry.Text = string.Empty;
        }
    }
}

public class TodoItem
{
	public string Title { get; set; }
    public DateTime Deadline { get; set; }
    public bool IsDone { get; set; }
}
