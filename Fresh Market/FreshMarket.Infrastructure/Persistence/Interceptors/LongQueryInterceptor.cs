using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Data.Common;
namespace FreshMarket.Infrastructure.Persistence.Interceptors
{
    public class LongQueryInterceptor : DbCommandInterceptor
    {
        private readonly ILogger<LongQueryInterceptor> _logger;

        public LongQueryInterceptor(ILogger<LongQueryInterceptor> logger)
        {
            _logger = logger;
        }

        public override ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
        {
            if (eventData.Duration.Seconds > 2)
            {
                LogLongQuery(command, eventData);
            }

            return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
        }

        private void LogLongQuery(DbCommand command, CommandExecutedEventData eventData)
        {
            _logger.LogWarning($"Long query: {command.CommandText}. TotalMilliseconds:{eventData.Duration.TotalMilliseconds}");
        }

        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            if (eventData.Duration.Seconds > 2)
            {
                LogLongQuery(command, eventData);
            }
            return base.ReaderExecuted(command, eventData, result);
        }
    }
}
