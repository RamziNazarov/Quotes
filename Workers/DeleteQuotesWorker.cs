using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quotes.Interfaces.Services;

namespace Quotes.Workers
{
    public class DeleteQuotesWorker : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;

        public DeleteQuotesWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DeleteQuotesAsync, null, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5));
            return Task.CompletedTask;
        }

        private async void DeleteQuotesAsync(object obj)
        {
            using var scope = _serviceProvider.CreateScope();
            var quoteService = scope.ServiceProvider.GetRequiredService<IQuoteService>();
            await quoteService.DeleteOldQuotesAsync();
        }
        
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}