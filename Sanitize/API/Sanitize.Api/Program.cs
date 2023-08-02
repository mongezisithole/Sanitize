using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Sanitize.Api.Extensions;
using Sanitize.Data.Context;
using Sanitize.Repository.Implementations;
using Sanitize.Repository.Interfaces;
using Sanitize.Services.Implementations;
using Sanitize.Services.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<ISanitizeServices, SanitizeServices>();
builder.Services.AddScoped<ISanitizer, Sanitizer>();

//Config db
builder.Services.AddDbContext<SensitiveContext>(options =>
{
    //Uncomment if you want to use InMemory DB
    //options.UseInMemoryDatabase(databaseName: "SensitiveWords");
    //Not a good thing to hard code a conn string here but since I don't have MsSql I thought it would be easy if I put here in case you want to change it.
    options.UseNpgsql("Server=localhost;Username=postgres;Password=postgres;Database=SensitiveWords;Port=5432;SSLMode=Prefer");
    options.EnableSensitiveDataLogging();
}, ServiceLifetime.Scoped);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    //Basic config for swagger
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Sensitive Words API",
        Description = "An ASP.NET Core Web API for sanitizing sensitive words sent by a user",
        TermsOfService = null,
        Contact = new OpenApiContact
        {
            Name = "Mongezi M Sithole",
            Url = null
        },
        License = new OpenApiLicense
        {
            Name = "Test License",
            Url = null
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

//Not advised but for a quick demo I can allow any
app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Data seeding done here
app.SeedData();

app.Run();
