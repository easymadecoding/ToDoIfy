using System;
using System.Collections.ObjectModel;
using Plugin.LocalNotification;
using Plugin.LocalNotification.iOSOption;

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

        private TodoItemDatabase _database;
        private const string deadlineTitle = "Task deadline at: ";

        public ToDoViewModel(TodoItemDatabase database)
		{
            _database = database;

            var result = Task.Run(_database.GetItemsAsync).ConfigureAwait(true);
            TodoItems = new ObservableCollection<TodoItem>( result.GetAwaiter().GetResult() );
        }

        public async void AddTodoItem(string title, DateTime deadline)
        {
            var deadlineNotification = new NotificationRequest
            {
                Title = title,
                Description = deadlineTitle + deadline.ToString(),
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(1) //deadline.AddHours(-1)
                }
            };

            var itemToBeAdded = new TodoItem { Title = title, IsDone = false, Deadline = deadline };

            TodoItems.Add(itemToBeAdded);
            await _database.SaveItemAsync(itemToBeAdded);

            await LocalNotificationCenter.Current.Show(deadlineNotification);
        }

        public async void UpdateTodoItem(TodoItem todoItem)
        {
            await _database.SaveItemAsync(todoItem);
        }

        public async void RemoveTodoItem(TodoItem todoItem)
        {
            TodoItems.Remove(todoItem);
            await _database.DeleteItemAsync(todoItem);
        }

        public async void TapItem(TodoItem todoItem)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?Text={todoItem.Title}&Deadline={todoItem.Deadline}");
        }

        public Command<TodoItem> RemoveCommand => new Command<TodoItem>(RemoveTodoItem);
        public Command<TodoItem> TapCommand => new Command<TodoItem>(TapItem);
    }
}

