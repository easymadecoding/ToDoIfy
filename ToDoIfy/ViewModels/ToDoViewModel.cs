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
                new TodoItem { Title = "Task 1", IsDone = false },
                new TodoItem { Title = "Task 2", IsDone = true },
                new TodoItem { Title = "Task 3", IsDone = false }
            };
        }

        public void AddTodoItem(string title)
        {
            TodoItems.Add(new TodoItem { Title = title, IsDone = false });
        }

        public void RemoveTodoItem(TodoItem todoItem)
        {
            TodoItems.Remove(todoItem);
        }

        public async void TapItem(string itemName)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?Text={itemName}");
        }

        public Command<TodoItem> RemoveCommand => new Command<TodoItem>(RemoveTodoItem);
        public Command<string> TapCommand => new Command<string>(TapItem);
    }
}

