namespace ExpensesTracker.Server.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;
        public LoggerService(ILogger<LoggerService> logger)
        {
            _logger = logger;
        }

        public void LogMessage(Exception ex)
        {
            _logger.LogError($"Error:{ex.Message},\n{ex.StackTrace}");
        }

        public void LogMessage(string Message)
        {
            _logger.LogError(Message);
        }
    }
}
