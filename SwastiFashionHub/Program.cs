using Blazored.LocalStorage;
using Radzen;
using SwastiFashionHub.Shared.Core.Extensions;
using SwastiFashionHub.Shared.Core.Http;
using SwastiFashionHub.Shared.Core.Services;
using SwastiFashionHub.Shared.Core.Services.Interface;

using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor(
    options =>
    {
        options.DetailedErrors = true;
        options.DisconnectedCircuitMaxRetained = 100;
        options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(3);
        options.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(1);
        options.MaxBufferedUnacknowledgedRenderBatches = 10;
    }
    ).AddHubOptions(options =>
    {
        options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
        options.EnableDetailedErrors = false;
        options.HandshakeTimeout = TimeSpan.FromSeconds(15);
        options.KeepAliveInterval = TimeSpan.FromSeconds(15);
        options.MaximumParallelInvocationsPerClient = 1;
        options.MaximumReceiveMessageSize = long.MaxValue;
        options.StreamBufferCapacity = 100;
    })
    .AddCircuitOptions(option => { option.DetailedErrors = true; });

builder.Services.AddHttpClientInterceptor();

var baseUrl = builder.Configuration.GetValue<string>("BaseUrl");
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(baseUrl),
    Timeout = Timeout.InfiniteTimeSpan
}.EnableIntercept(sp));

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IHttpService, HttpService>()
    .AddScoped<IToastService, ToastService>()
    .AddScoped<LocalStorage>()
    .AddScoped<IConfirmService, ConfirmService>()
    .AddScoped<IAlertService, AlertService>()
    .AddScoped<IDesignService, DesignService>()
    .AddScoped<IPartyService, PartyService>()
    .AddScoped<IFabricService, FabricService>()
    .AddScoped<SpinnerService>()
    .AddScoped<DialogService>()
    .AddBlazoredLocalStorage();



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
