using Api.Extensions;
using Api.Interfaces;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using mongo_db_demo.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddDbContext<MongoContext>(options =>
{
    options.UseMongoDB("mongodb://localhost:27017", "productsdb");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
