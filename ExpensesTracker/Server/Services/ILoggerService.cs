﻿namespace ExpensesTracker.Server.Services
{
    public interface ILoggerService
    {
        void LogMessage(Exception ex);
        void LogMessage(string Message);
    }
}