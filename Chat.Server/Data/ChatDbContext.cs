﻿using Chat.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Server.Data;
public class ChatDbContext : DbContext
{
	public DbSet<User> Users { get; set; }
	public DbSet<Message> Messages { get; set; }

	public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
	{}
}
