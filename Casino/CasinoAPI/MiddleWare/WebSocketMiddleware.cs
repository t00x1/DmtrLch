using Microsoft.AspNetCore.WebSockets;
using BusinessLogic.Service;
public class WebSocketMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;


    private readonly IServiceScopeFactory _serviceScopeFactory;
    public WebSocketMiddleware(RequestDelegate next, IServiceProvider serviceProvider, IServiceScopeFactory serviceScopeFactory)
{
    _next = next;
    _serviceProvider = serviceProvider;
    _serviceScopeFactory = serviceScopeFactory;
}

    public async Task InvokeAsync(HttpContext context)
    {
        string IdOfUser;
        if (context.Request.Path.Value.ToLower().StartsWith("/ws")  && context.WebSockets.IsWebSocketRequest)
        {
            var queryParameters = context.Request.Query;

            if (queryParameters.TryGetValue("token", out var token))
            {
                Console.WriteLine(token);
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var userAuthorizationService = scope.ServiceProvider.GetRequiredService<UseridentificationService>();
                    IdOfUser = await userAuthorizationService.Authorize(token);

                    if (!string.IsNullOrEmpty(IdOfUser))
                    {
                        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        var myWebSocketService = _serviceProvider.GetRequiredService<IMyWebSocketService>();
                        
                   
                        await myWebSocketService.HandleWebSocketAsync(webSocket,IdOfUser);
                        
                    }

                
                }

            }Console.WriteLine("Токен не валиден");
            
 


           
          
        }
        else
        {
          
            await _next(context);
        }
    }
}
