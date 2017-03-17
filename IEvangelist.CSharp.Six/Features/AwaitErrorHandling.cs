using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IEvangelist.CSharp.Six.Features
{
    class AwaitErrorHandling
    {
        private const string Url = "http://api.icndb.com/jokes/random?limitTo=[nerdy]";
        internal event Action<string> RequestStatusChanged;

        // In C# 5, you were not permitted to use an await statement within 
        // a catch or finally block.

        internal async Task<string> GetJokeAsync()
        {
            var client = new HttpClient();
            try
            {
                const string url = "http://api.icndb.com/jokes/random?limitTo=[nerdy]";
                OnRequestStatusChanged($"HTTP Request > {url}");
                var response = await client.GetStringAsync(url);
                var result = JsonConvert.DeserializeObject<Result>(response);

                return result?.Value?.Joke;
            }
            catch (HttpRequestException ex)
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

    public class Result
    {
        [JsonProperty("type")] public string Type { get; set; }
        [JsonProperty("value")] public Value Value { get; set; }
    }

    public class Value
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("joke")] public string Joke { get; set; }
    }
}