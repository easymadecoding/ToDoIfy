using System;
using System.Collections.ObjectModel;
using Plugin.LocalNotification;
using Plugin.LocalNotification.iOSOption;
using ToDoIfy.Services;

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
        private IQuoteService _quoteService;
        private const string deadlineTitle = "Task deadline at: ";
        private Tuple<string, string> _quote = new Tuple<string, string>(string.Empty, string.Empty);
        private string _quoteLabel = string.Empty;

        public string QuoteLabel
        {
            get => _quoteLabel;
            set
            {
                _quoteLabel = value;
                OnPropertyChanged();
            }
        }

        public ToDoViewModel(TodoItemDatabase database, IQuoteService quoteService)
		{
            _database = database;
            _quoteService = quoteService;

            var result = Task.Run(_database.GetItemsAsync).ConfigureAwait(true);
            TodoItems = new ObservableCollection<TodoItem>( result.GetAwaiter().GetResult() );

            InitializaQuote();

            if (TodoItems.Count != 0)
            {
                QuoteLabel = string.Empty;
            }
            else
            {
                QuoteLabel = _quote.Item1 + "\n\t\t -" + _quote.Item2;
            }
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

            QuoteLabel = string.Empty;
        }

        public async void UpdateTodoItem(TodoItem todoItem)
        {
            await _database.SaveItemAsync(todoItem);
        }

        public async void RemoveTodoItem(TodoItem todoItem)
        {
            TodoItems.Remove(todoItem);
            await _database.DeleteItemAsync(todoItem);

            if (TodoItems.Count == 0)
            {
                QuoteLabel = _quote.Item1 + "\n\t\t -" + _quote.Item2;
            }
        }

        public async void TapItem(TodoItem todoItem)
        {
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?Text={todoItem.Title}&Deadline={todoItem.Deadline}");
        }

        public Command<TodoItem> RemoveCommand => new Command<TodoItem>(RemoveTodoItem);
        public Command<TodoItem> TapCommand => new Command<TodoItem>(TapItem);

        private void InitializaQuote()
        {
            var quote = Task.Run(_quoteService.GetQuote).ConfigureAwait(true);
            _quote = quote.GetAwaiter().GetResult();
        }
    }
}

