namespace ToDoIfy.Services
{
	public interface IQuoteService
	{
        Task<Tuple<string, string>> GetQuote();
    }
}