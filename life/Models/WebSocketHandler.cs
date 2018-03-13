using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using life.Controllers;
using Newtonsoft.Json;
using System.Text;

namespace life.Models
{
    public class WebSocketHandler
    {

        // public static WebSocket ws;
        // public static GameGrid grid;

        public static async Task SocketSend(HttpContext context, WebSocket webSocket)
        {

            GameGrid grid = HomeController.gameService;
            // ws = webSocket;

            while (true)
            {

                if (grid.updated)
                {

                    var jsonGrid = JsonConvert.SerializeObject(grid.grid);
                    var buffer = Encoding.UTF8.GetBytes(jsonGrid);
                    var segment = new ArraySegment<byte>(buffer);
                    await webSocket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
                    
                    grid.updated = false;

                }
                //System.Threading.Thread.Sleep(50);
                //}
            }

        }

        //public static async Task SocketBroadcast(GameGrid passedGrid)
        //{

        //    // grid.advance();
        //    //while (passedGrid.isRunning)
        //    // {
        //    var jsonGrid = JsonConvert.SerializeObject(passedGrid.grid);
        //    var buffer = Encoding.UTF8.GetBytes(jsonGrid);
        //    var segment = new ArraySegment<byte>(buffer);
        //    await webSocket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
        //    // }

        //}
    }
}
