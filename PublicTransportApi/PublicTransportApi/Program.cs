using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PublicTransportApi.Data;
using PublicTransportApi.Services;
using PublicTransportApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite("datasource=database.sqlite"));
builder.Services.AddScoped<ILineService, LineService>();
builder.Services.AddScoped<IScheduleEntryService, ScheduleService>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

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