using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApiDemo.Models;
using WebApiDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ➤ Configure MongoDB Settings
builder.Services.Configure<StudentDatabaseSettings>(
    builder.Configuration.GetSection(nameof(StudentDatabaseSettings)));

builder.Services.Configure<CourseDatabaseSettings>(
    builder.Configuration.GetSection(nameof(CourseDatabaseSettings)));

builder.Services.AddSingleton<IStudentDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<StudentDatabaseSettings>>().Value);

builder.Services.AddSingleton<ICourseDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<CourseDatabaseSettings>>().Value);


// ➤ MongoDB Client
builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("StudentDatabaseSettings:ConnectionURI"))); 

//builder.Services.AddSingleton<IMongoClient>(s =>
//    new MongoClient(builder.Configuration.GetValue<string>("CourseDatabaseSetting:ConnectionURI")));


// ➤ Register Services
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IStudentService, StudentService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowReactApp");

// Swagger only in dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
 
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
