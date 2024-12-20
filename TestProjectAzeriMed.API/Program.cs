using Microsoft.AspNetCore.Authentication;
using TestProjectAzeriMed.API.Handlers;
using TestProjectAzeriMed.Identity;

var builder = WebApplication.CreateBuilder(args);
IConfiguration config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureIdentityServices(config);

builder.Services.AddAuthentication("CustomScheme")
    .AddScheme<AuthenticationSchemeOptions, CustomAuthHandler>("CustomScheme", null);
builder.Services.AddSwaggerGen();

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
