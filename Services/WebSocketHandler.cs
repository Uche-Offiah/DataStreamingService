using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace DataStreamingService.Services
{
    public class WebSocketHandler
    {
        private readonly DataService _dataService;

        public WebSocketHandler(DataService dataService)
        {
            _dataService = dataService;
        }

        public async Task HandleWebSocketAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                using (WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync())
                {
                    await SendDataAsync(webSocket);
                }
            }
            else
            {
                context.Response.StatusCode = 400;
            }
        }

        private async Task SendDataAsync(WebSocket webSocket)
        {
            var data = await _dataService.ProcessDataAsync();
            var jsonData = JsonConvert.SerializeObject(data);
            var buffer = Encoding.UTF8.GetBytes(jsonData);

            var segment = new ArraySegment<byte>(buffer);
            await webSocket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
        }
    }
}
