using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApiDemo.Models;
using WebApiDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<StudentDatabaseSettings>(
      builder.Configuration.GetSection(nameof(StudentDatabaseSettings)));

builder.Services.AddSingleton<IStudentDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<StudentDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
        new MongoClient(builder.Configuration.GetValue<string>("StudentDatabaseSettings:ConnectionURI")));

builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
