using AutoMapper;
using WorkForceHub.API.Configurations;
using WorkForceHub.API.Middlewares;
using WorkForceHub.Application.Configurations;
using WorkForceHub.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


//Adding Servvices to Container
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.AddOpenApi();

//Add Swagger 
builder.Services.AddSwaggerGen();

//Add CORS
// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();




if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AngularPolicy");
app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
