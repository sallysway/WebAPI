using Application;
using CheckoutAPI.Exceptions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlDoc = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlDocPath = Path.Combine(AppContext.BaseDirectory, xmlDoc);
    options.IncludeXmlComments(xmlDocPath);
});
var configuration = builder.Configuration;
builder.Services.AddApplicationServices(configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
