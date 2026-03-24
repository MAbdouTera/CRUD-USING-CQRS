using Api.Exceptions;
using Application.Behaviors;
using Application.Common.Interfaces;
using FluentValidation;
using Infrasturcture.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=app.db"));
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(Application.IAssemblyMarker).Assembly);
});
builder.Services.AddValidatorsFromAssembly(typeof(Application.IAssemblyMarker).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddScoped<IAppDbContext, AppDbContext>();
var app = builder.Build();
app.UseExceptionHandler();
app.MapControllers();
app.Run();
