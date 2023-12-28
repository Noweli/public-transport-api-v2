using System.Text.Json.Serialization;
using Asp.Versioning;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PublicTransportApi.Data;
using PublicTransportApi.Services;
using PublicTransportApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("V1", new OpenApiInfo { Title = "Public Transport API", Version = "V1.0" });
    options.ResolveConflictingActions(desc => desc.First());
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("datasource=database.sqlite"));
builder.Services.AddScoped<ILineService, LineService>();
builder.Services.AddScoped<IScheduleEntryService, ScheduleService>();
builder.Services.AddScoped<IStopPointService, StopPointService>();
builder.Services.AddScoped<ISPLService, SPLService>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/V1/swagger.json", "V1.0"); });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();