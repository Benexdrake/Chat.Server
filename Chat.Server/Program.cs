using Chat.Server.Data;
using Chat.Server.Hubs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ChatDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Chat")));

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHub<ChatHub>("chat-hub");


app.MapGet("users", (ChatDbContext context) => context.Users.ToList());

app.MapGet("userByName", (ChatDbContext context, string username) => context.Users.Where(x => x.Name.Contains(username)).FirstOrDefault());

app.MapGet("userById", (ChatDbContext context, ulong userid) => context.Users.Where(x => x.Id == userid).FirstOrDefault());

app.MapGet("messages", (ChatDbContext context, string searchParams) => context.Messages.Where(x => x.Content.Contains(searchParams)));

app.MapGet("messagesByCount", (ChatDbContext context, int start, int end) => context.Messages.ToList().GetRange(start, end));

app.MapGet("message", (ChatDbContext context, ulong messageId) => context.Messages.Where(x => x.Id == messageId));

app.MapGet("messagesByDate", (ChatDbContext context, DateTime date) => context.Messages.Where(x => x.Date == date));

app.MapGet("messagesByUserId", (ChatDbContext context, ulong userId) => context.Messages.Where(x => x.UserId == userId));


app.Run();


//var summaries = new[]
//{
//	"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//var forecast = Enumerable.Range(1, 5).Select(index =>
//	new WeatherForecast
//	(
//		DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//		Random.Shared.Next(-20, 55),
//		summaries[Random.Shared.Next(summaries.Length)]
//	))
//	.ToArray();
//return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();


//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}
