using Chat.Server.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ChatDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Chat")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("users", (ChatDbContext context) =>
{
	return context.Users.ToList();
}).WithDescription("Get all Users from DB");

app.MapGet("userByName", (ChatDbContext context, string username) =>
{
	return context.Users.Where(x => x.Name.Contains(username)).FirstOrDefault();
}).WithDescription("Get a User by Name from DB");

app.MapGet("userById", (ChatDbContext context, ulong userid) =>
{
	return context.Users.Where(x => x.Id == userid).FirstOrDefault();
}).WithDescription("Get a User by Id from DB");

app.MapGet("messages", (ChatDbContext context, string searchParams) =>
{
	return context.Messages.Where(x => x.Content.Contains(searchParams));
}).WithDescription("Get all Messages from DB");

app.MapGet("message", (ChatDbContext context, ulong messageId) =>
{
	return context.Messages.Where(x => x.Id == messageId);
}).WithDescription("Get a Messages from DB");

app.MapGet("messagesByDate", (ChatDbContext context, DateTime date) =>
{
	return context.Messages.Where(x => x.Date == date);
}).WithDescription("Get all Messages with specific Date from DB");

app.MapGet("messagesByUserId", (ChatDbContext context, ulong userId) =>
{
	return context.Messages.Where(x => x.UserId == userId);
}).WithDescription("Get all Messages from User X by Id from DB");


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
