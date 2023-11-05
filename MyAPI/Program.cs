using Microsoft.EntityFrameworkCore;
using MyAPI.Data;
using MyAPI.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddMvc();

// Используем временную БД, которая сохраняет данные в RAM
services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("db_API"), ServiceLifetime.Scoped);
services.AddTransient<IRepository, Repository>();

var context = new AppDbContext();
var repo = new Repository(context);
var api = new ServiceEventHandler(repo);

// Начинаем работу сервисов
api.StartServices();

services.AddTransient<IServiceEventHandler, ServiceEventHandler>();
var app = builder.Build();

// Для отправки запросов к API через GUI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
