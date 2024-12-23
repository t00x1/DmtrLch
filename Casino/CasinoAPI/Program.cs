using Domain.Models;
using Domain.DTO;
using Microsoft.AspNetCore.Builder;

using DataAccess.Repository;
using DataAccess.Wrapper;
using Domain.Interfaces.Repository;
using Domain.Common.Generic.Validation;
using Domain.Common.Generic;
using Domain.Interfaces.Common.Generic;
using Domain.Interfaces.Infrastructure;
using Domain.Interfaces.Infrastructure.File;
using CasinoApi.Router;
using  Router;
using Infrastructure.Code;
using Infrastructure.Password;
using BusinessLogic.Factory;
using Domain.Interfaces.BusinessLogic.Factory.Code;
using BusinessLogic.Service;
using Domain.Interfaces.Infrastructure.Code;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Infrastructure.Email;
using Microsoft.AspNetCore.Identity.UI.Services;
using Infrastructure.File;
using Domain.Interfaces.Infrastructure.JWT;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.AspNetCore.Builder;
using Domain.Interfaces.Infrastructure.JsonDeserializer;
using Infrastructure.Services;
using Domain.Containers;
using CasinoApi.Hosting;


var builder = WebApplication.CreateBuilder(args);

//СДЕЛАТЬ УНИВЕРСАЛЬНЫЙ JWT И брать кодключ из  настроек, сделать нормальные зависимости, сделать refresh token, сделать middle ware с токеном 
//Часть с подтверждением кода чет не так работает, нужно затестить 
//ПРОСТО ужас КАКОЙ КОСТЫЛЬ В ОТОБРАЖЕНИИ КАРТИНКИ 
//Написать адаптивную json парсилку 

// добавить проверку корректности файла подгрузки
// надо продумать то что когда токен будет меняться как-то нужно будет переподключаться к ws....
// Исправить то что выйгрыш ток в стоке 
// вернуть чтение файла  формы кода на почты 
// еще можно подумать над тем как можно сделать для каждого типа комнаты свою ставку(интерфейсы объекты и наследрование)

//решить проблему что при реюз купона втсавляется код купона и юзер айди, ни получаются не уникальными
// Сделать получение баланса после применения промокода сейчас костыль жесткий 
builder.Services.AddWebSockets(options =>
{
    options.KeepAliveInterval = TimeSpan.FromSeconds(120); // Интервал живого соединения
}); 


builder.Services.AddSingleton<GuidUtility>();
builder.Services.AddSingleton<UsCode>();
builder.Services.AddSingleton<string>("your-long-secret-key-with-at-least-32-characters");
builder.Services.AddSingleton<ITextFileOperation, TextFileOperations>();
builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddSingleton<IPropertyInfoExtractor, PropertyInfoExtractor>();
builder.Services.AddSingleton<IBaseValidation, BaseValidation>();
builder.Services.AddSingleton<IAutoMapper, AutoMapper>();
builder.Services.AddSingleton<IImageFileOperations, FileOperationsImage>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<IServiceFactoryCode, ServiceFactoryCode>();
builder.Services.AddSingleton<ISenderEmail, EmailSenderYandex>();
builder.Services.AddTransient<UserRegisterService>();
builder.Services.AddTransient<UserLoginService>();
builder.Services.AddTransient<UserEmailCodeSend>();
builder.Services.AddTransient<UserGetDataAboutProfile>();
builder.Services.AddTransient<UseridentificationService>();
builder.Services.AddTransient<UserEmailConfirm>();
builder.Services.AddTransient<UserUpdatePicProfile>();
builder.Services.AddTransient<BalanceGet>();
builder.Services.AddScoped<CuponActivate>();

builder.Services.AddScoped<UserGetPicProfile>();




builder.Services.AddSingleton<RoomEventsWS>();

builder.Services.AddSingleton<CasinoApi.Router.IRouter,Router.Router>();
builder.Services.AddSingleton<IJsonConverter,JsonConverter>();
builder.Services.AddSingleton<IMyWebSocketService, MyWebSocketService>();

// builder.Services.AddSingleton<IRepositoryWrapper,RepositoryWrapperServer>(); СТранно надо чекнуть потом че за бред
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddSingleton<RepositoryWrapperServer>();
// builder.Services.AddSingleton<IWrapperFactory, WrapperFactory>(); ДВЕ ПРОБЛЕМЫ, равзе раньше нельзя был зарагестрировать интерфейс и реализацию и потом использовтать это в фабрике
// вторая, почему нельзя использовать в фактори объекты с разным периодом жизни?


// ЧТО ЗА БРЕД С ОТСЛЕЖИВАНИЕМ В ОБНОВЛЕНИИ БАЛАНСА
 builder.Services.AddSingleton<GameWheel>();

builder.Services.AddSingleton<GameRooms>();

builder.Services.AddHostedService<Hosting>();




var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddDbContext<CasinoContext>(options =>
    options.UseSqlServer(connectionString)
);

// builder.Services.AddSingleton<Func<CasinoContext>>(provider => () => provider.GetRequiredService<CasinoContext>());

builder.Services.AddControllers();

// Настройка Swagger с поддержкой JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Добавляем поддержку токена в заголовке для Swagger
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter your JWT token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  // Разрешить запросы с любых источников
              .AllowAnyMethod()  // Разрешить любые HTTP-методы (GET, POST и т.д.)
              .AllowAnyHeader(); // Разрешить любые заголовки
    });
});

var app = builder.Build();
app.UseCors("AllowAll");
app.UseWebSockets();
// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//     var logger = services.GetRequiredService<ILogger<Program>>();

//     try
//     {
//         var context = services.GetRequiredService<CasinoContext>();
//         context.Database.Migrate();
//         logger.LogInformation("Successful migration");
//     }
//     catch (Exception ex)
//     {
//         logger.LogError(ex, "Error during migration");
//     }
// }

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<WebSocketMiddleware>();
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
