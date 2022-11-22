using ExpensesTracker.Shared.Models;
using System.Net.Http.Json;
using System.Text;
using static System.Net.WebRequestMethods;

namespace ExpensesTracker.Client.Services.LoggingService
{
    public class LoggerService : ILogger
    {
        private readonly HttpClient _httpClient;

        public LoggerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception ex, Func<TState, Exception, string> formatter)
        {           
            LogMessage logMessage = new();
            logMessage.Message = $"Error:{ex.Message},\n{ex.StackTrace}";

            await _httpClient.PostAsJsonAsync<LogMessage>("/logs", logMessage);

        }
    }
}