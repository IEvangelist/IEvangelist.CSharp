using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IEvangelist.CSharp.Six.Features
{
    class AwaitErrorHandling
    {
        internal event Action<string> RequestStatusChanged;

        // In C# 5, you were not permitted to use an await statement within a catch or finally block.

        internal async Task<string> GetJokeAsync()
        {
            var client = new HttpClient();
            try
            {
                const string url = "https://api.chucknorris.io/jokes/random";
                OnRequestStatusChanged($"HTTP Request > {url}");
                var response = await client.GetStringAsync(url);
                var joke = JsonConvert.DeserializeObject<Joke>(response);

                return joke.Value;
            }
            catch (HttpRequestException ex) when (ex.Message.Contains("404"))
            {
                await LogAsync(ex);
            }
            finally
            {
                await CleanupAsync();
                client.Dispose();
            }

            return "That's not very funny!";
        }

        private Task CleanupAsync()
        {
            OnRequestStatusChanged("HTTP Done > hope you like the joke.");

            return Task.CompletedTask;
        }

        private Task<bool> LogAsync(Exception ex)
        {
            OnRequestStatusChanged($"HTTP Error > {ex.Message}.");

            return Task.FromResult(true);
        }

        private void OnRequestStatusChanged(string message) 
            => RequestStatusChanged?.Invoke(message);
    }

    public class Joke
    {
        [JsonProperty("value")] public string Value { get; set; }
    }
}