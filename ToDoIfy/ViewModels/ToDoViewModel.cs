using System;
using System.Collections.ObjectModel;

namespace ToDoIfy.ViewModels
{
	public class ToDoViewModel : BindableObject
	{

        private ObservableCollection<TodoItem> _todoItems;

        public ObservableCollection<TodoItem> TodoItems
        {
            get => _todoItems;
            set
            {
                _todoItems = value;
                OnPropertyChanged();
            }
        }

        public ToDoViewModel()
		{
            TodoItems = new ObservableCollection<TodoItem>
            {
                new TodoItem { Title = "Task 1", IsDone = false, Deadline = DateTime.Now },
                new TodoItem { Title = "Task 2", IsDone = true, Deadline = DateTime.Now },
                new TodoItem { Title = "Task 3", IsDone = false, Deadline = DateTime.Now }
            };
        }

        public void AddTodoItem(string title, DateTime deadline)
        {
            TodoItems.Add(new TodoItem { Title = title, IsDone = false, Deadline = deadline });
        }

        public void RemoveTodoItem(TodoItem todoItem)
        {
            TodoItems.Remove(todoItem);
        }

        public async void TapItem(TodoItem todoItem)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?Text={todoItem.Title}&Deadline={todoItem.Deadline}");
        }

        public Command<TodoItem> RemoveCommand => new Command<TodoItem>(RemoveTodoItem);
        public Command<TodoItem> TapCommand => new Command<TodoItem>(TapItem);
    }
}

