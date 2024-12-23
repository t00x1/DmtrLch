using BusinessLogic.Service;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public JwtMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
{
    var path = context.Request.Path.Value.ToLower();
    Console.WriteLine(path);
    // Пропускаем middleware для определённых маршрутов
    if (path.StartsWith("/ws") || path.Contains("register") || path.Contains("login") || path.Contains("emailconfirmcode") || path.Contains("emailsendcode")) 
    {
        Console.WriteLine("token doesnt need");
        await _next(context);
        return;
    }

    try
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var userAuthorizationService = scope.ServiceProvider.GetRequiredService<UseridentificationService>();
                var userId = await userAuthorizationService.Authorize(token);

                if (!string.IsNullOrEmpty(userId))
                {
                    context.Items["UserId"] = userId;
                    await _next(context);
                    return;
                }
              
            }
        }
        Console.WriteLine($"НЕТ ТОКЕНА");
        context.Response.StatusCode = 401;
        await context.Response.WriteAsync("Invalid token");

        
    }
    catch (Exception ex)
    {
        // Логируем исключение
        Console.WriteLine($"Error: {ex.Message}");
        Console.WriteLine(ex.StackTrace);

        // Отправляем ошибку только если ответ ещё не был отправлен
        if (!context.Response.HasStarted)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync($"An error occurred while processing your request: {ex.Message}");
        }
    }
}

}
