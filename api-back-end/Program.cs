//using api_back_end.Context;
using api_back_end.Context;
using api_back_end.Services.Generator;
//using api_back_end.Services.Generator;
using api_back_end.Services.Simulator;
using back_end_api.ControlCenter;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FlightsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("policy", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddScoped<IControlCenter, ControlCenter>();

builder.Services.AddHostedService<ArrivingSimulator>();
builder.Services.AddHostedService<ArrivingGenerator>();
builder.Services.AddHostedService<DepartingSimulator>();
builder.Services.AddHostedService<DepartingGenerator>();


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

app.UseCors("policy");

app.Run();
