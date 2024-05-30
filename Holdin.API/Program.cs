using Holdin.API.Data;
using Holdin.API.Services;
using Microsoft.EntityFrameworkCore;

Environment.SetEnvironmentVariable("STORAGE", "C:\\Users\\alvar\\Escritorio\\test\\");

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton<StorageService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite("DataSource=:memory");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
