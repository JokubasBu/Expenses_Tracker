namespace ExpensesTracker.Client.Services.LoggingService
{
    public class LoggerProvider : ILoggerProvider
    {
        private readonly HttpClient _httpClient;

        public LoggerProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new LoggerService(_httpClient);
        }

        public void Dispose()
        {

        }
    }
}
