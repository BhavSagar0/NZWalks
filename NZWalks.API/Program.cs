using Microsoft.EntityFrameworkCore;
using NZWalks.BAL.Contracts;
using NZWalks.BAL.Implementations;
using NZWalks.BAL.RepositoryInterfaces;
using NZWalks.DAL.Context;
using NZWalks.DAL.Repositories.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NZWalksDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")), contextLifetime: ServiceLifetime.Scoped, optionsLifetime: ServiceLifetime.Scoped);
builder.Services.AddScoped<IRegionsService, RegionsService>();
builder.Services.AddScoped<IRegionsRespository, RegionsRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
