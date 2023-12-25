using System.Text.Json;

namespace ToDoIfy.Services
{
	public class QuoteService : IQuoteService
	{
        private HttpClient client = new HttpClient();

        public async Task<Tuple<string, string>> GetQuote()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://quotes-inspirational-quotes-motivational-quotes.p.rapidapi.com/quote?token=ipworld.info"),
                Headers =
                {
                    { "X-RapidAPI-Key", "You need to enter your own API Key here" },
                    { "X-RapidAPI-Host", "quotes-inspirational-quotes-motivational-quotes.p.rapidapi.com" },
                },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                Dictionary<string, string> result = new Dictionary<string, string>();
                result = JsonSerializer.Deserialize<Dictionary<string, string>>(body);

                return new Tuple<string, string>(result["text"], result["author"]);
            }
        }
	}
}

