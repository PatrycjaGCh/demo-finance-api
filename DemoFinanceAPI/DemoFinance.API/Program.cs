using System.Reflection;
using System.Text.Json.Serialization;
using DemoFinance.Application;
using DemoFinance.Domain;
using DemoFinance.Infrastructure;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IExchangeRatesProvider, ExchangeRatesProvider>();
builder.Services.AddTransient<IRequestHandler<GetExchangedMoneyQuery, MoneyDto>, GetExchangedMoneyQueryHandler>(); 
// todo: generic registration

builder.Services.AddControllers()
                .AddJsonOptions(opt=> { opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddRouting(options => options.LowercaseUrls = true);

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