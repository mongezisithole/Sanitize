using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Sanitize.Data.Context;
using Sanitize.Repository.Implementations;
using Sanitize.Repository.Interfaces;
using Sanitize.Server.Extensions;
using Sanitize.Services.Implementations;
using Sanitize.Services.Interfaces;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


builder.Services.AddScoped<ISanitizeServices, SanitizeServices>();
builder.Services.AddScoped<ISanitizer, Sanitizer>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<SensitiveContext>(options =>
{
    //options.UseInMemoryDatabase(databaseName: "SensitiveWords");
    options.UseNpgsql("Server=localhost;Username=postgres;Password=postgres;Database=SensitiveWords;Port=5432;SSLMode=Prefer");
    options.EnableSensitiveDataLogging();
}, ServiceLifetime.Scoped);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
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

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.SeedData();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
