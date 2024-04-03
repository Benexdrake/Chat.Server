using Chat.Server.Data;
using Chat.Server.Hubs;
using Chat.Server.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ChatDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Chat")));

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy", policy =>
	{
		policy.WithOrigins("http://localhost:3000")
			  .AllowAnyHeader()
			  .AllowAnyMethod()
			  .AllowCredentials();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

// ChatHub
app.MapHub<ChatHub>("/chathub");

// Minimal API
app.MapGet("users", (ChatDbContext context) => context.Users.ToList());

app.MapGet("userByName", (ChatDbContext context, string username) => context.Users.Where(x => x.Name.Contains(username)).FirstOrDefault());

app.MapGet("userById", (ChatDbContext context, string userid) => context.Users.Where(x => x.Id.Equals(userid)).FirstOrDefault());

app.MapGet("messages", (ChatDbContext context, string searchParams) => context.Messages.Where(x => x.Content.Contains(searchParams)));

app.MapGet("messagesByCount", (ChatDbContext context, int start, int end) => context.Messages.ToList().GetRange(start, end));

app.MapGet("message", (ChatDbContext context, ulong messageId) => context.Messages.Where(x => x.Id == messageId));

app.MapGet("messagesByDate", (ChatDbContext context, string date) => context.Messages.Where(x => x.Date.Equals(date)));

app.MapGet("messagesByUserId", (ChatDbContext context, string userId) => context.Messages.Where(x => x.UserId.Equals(userId)));

app.MapPost("insertUser", (ChatDbContext context, User user) =>
{
	var userDb = context.Users.Where(x => x.Id == user.Id).FirstOrDefault();
	if (userDb != null)
		context.Users.Remove(userDb);
	try
	{
		context.Users.Add(user);
		context.SaveChanges();
	}
	catch (Exception e)
	{
		Console.WriteLine(e.Message);
	}
});


app.Run();