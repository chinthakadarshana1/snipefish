using System.Reflection;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Snipefish.WebApi.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigAutofacContainer();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables("snipefish_");

// Add services to the container.
builder.Services.AddApplicationConfigurations(builder.Configuration);

builder.Services.AddNlogLogging(builder.Configuration);

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddCorsConfigurations(builder.Configuration);


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

//app.UseHttpsRedirection();

app.UseCors(CorsProvider.SnipefishCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
