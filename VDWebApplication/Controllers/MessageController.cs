using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;

namespace VDWebApplication.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessageController : BaseController
    {
        // GET: Chat

        private static Dictionary<string,WebSocket> _webSocketCollection = new Dictionary<string, WebSocket>();
         

        [HttpGet]
        public async Task<IActionResult> Connect(string? id)
        {
            try
            {
                if (HttpContext.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                    WebSocket? checkExist = _webSocketCollection.GetValueOrDefault(id);
                    if(checkExist != null) throw new Exception("Id đã tồn tại");
                    else
                    {
                        _webSocketCollection.Add(id, webSocket);
                        foreach (var item in _webSocketCollection)
                        {
                            item.Value.SendAsync(System.Text.Encoding.UTF8.GetBytes($"{id} đã kết nối vào cuộc trò chuyện"), WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                    }
                    while (webSocket.State == WebSocketState.Open)
                    {
                        var buffer = new byte[1024];
                        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                        if (result.MessageType == WebSocketMessageType.Text)
                        {
                            foreach (var item in _webSocketCollection)
                            {
                                item.Value.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
                            }
                        }
                        else if (result.MessageType == WebSocketMessageType.Binary)
                        {

                        }
                    }
                 
                }
                else
                {
                    throw new Exception("WebSocket is not supported.");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



     


    }

}
