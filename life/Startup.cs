using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using life.Models;
using life.Controllers;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading;

namespace life
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton<GameGrid>();
            services.AddSingleton<Singleton>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseWebSockets();  // added



            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        await WebSocketHandler.SocketSend(context, webSocket);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                        //await next();
                    }
                }
                else
                {
                    await next();
                }

            });

            //app.Use(async (http, next) =>
            //{

            //    if (http.WebSockets.IsWebSocketRequest)
            //    {
            //        IHandler handler = WebsocketManager.getHandler(http.Request.Path);
            //        if (handler == null)
            //        {
            //            await next();
            //        }
            //        else
            //        {
            //            var webSocket = await http.WebSockets.AcceptWebSocketAsync();
            //            if (webSocket != null && webSocket.State == WebSocketState.Open)
            //            {
            //                // TODO: Handle the socket here.
            //                WebsocketManager.Handle(webSocket, handler);
            //            }
            //        }
            //    }
            //    else
            //    {
            //        // Nothing to do here, pass downstream.  
            //        await next();
            //    }
            //});



        }
    }
}
