using DataStreamingService.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.WebSockets;

namespace DataStreamingService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly WebSocketHandler _webSocketHandler;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, WebSocketHandler webSocketHandler, IConfiguration configuration)
        {
            _logger = logger;
            _webSocketHandler = webSocketHandler;
            _configuration = configuration;
        }

        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //        if (_logger.IsEnabled(LogLevel.Information))
        //        {
        //            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        //        }
        //        await Task.Delay(1000, stoppingToken);
        //    }
        //}

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            string url = _configuration.GetValue<string>("WebSocketServer:Url") ?? "http://localhost:5000/ws/";

            var listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();

            while (!stoppingToken.IsCancellationRequested)
            {
                var context = await listener.GetContextAsync();
                if (context.Request.IsWebSocketRequest)
                {
                    await _webSocketHandler.HandleWebSocketAsync(context);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    context.Response.Close();
                }
            }

            listener.Stop();
        }
    }

}
}
