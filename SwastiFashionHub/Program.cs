using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using SwastiFashionHub.Core.Services;
using SwastiFashionHub.Shared.Core.Services;
using SwastiFashionHub.Shared.Core.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


builder.Services.AddScoped<IDesignService, DesignService>();
builder.Services.AddScoped<IAlertService, AlertService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
