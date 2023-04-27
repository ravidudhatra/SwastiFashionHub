using Microsoft.EntityFrameworkCore;
using SwastiFashionHub.Core.Services.Interface;
using SwastiFashionHub.Core.Services;
using SwastiFashionHub.Data.Context;
using AutoMapper;
using SwastiFashionHub.Core.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDesignService, DesignService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddDbContext<SwastiFashionHubLlpContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SFHDbConnectionString"),
    sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(maxRetryCount: 10,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorNumbersToAdd: null);
    }));

var config = new MapperConfiguration(c => {
    c.AddProfile<DesignMapper>();
});

builder.Services.AddSingleton<IMapper>(s => config.CreateMapper());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseStaticFiles(); // enable static file serving
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
