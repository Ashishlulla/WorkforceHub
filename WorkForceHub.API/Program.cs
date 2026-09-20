using WorkForceHub.API.Configurations;
using WorkForceHub.Application.Configurations;
using WorkForceHub.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


//Adding Servvices to Container
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.AddOpenApi();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
