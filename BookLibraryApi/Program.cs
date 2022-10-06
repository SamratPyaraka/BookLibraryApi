using Microsoft.EntityFrameworkCore;
using BookLibraryApi.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BookLibraryApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookLibraryApiContext") ?? throw new InvalidOperationException("Connection string 'BookLibraryApiContext' not found.")));

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();
app.Run();
