using ChatWebApiSignalR.DataAccess;
using ChatWebApiSignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container, including SignalR services.
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=ChatMessages.db"));

// Http request and Response there is 
// XMLHTTPREquest is secondary call
var app = builder.Build();


// Enable serving static files (such as index.html)
app.UseStaticFiles();
// Configure HTTP request pipeline (middleware).
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.MapControllers();

// Map SignalR hubs to a specific route.
app.MapHub<ChatHub>("/chathub");

app.Run();
