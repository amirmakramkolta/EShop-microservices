using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

var reverseProxy = builder.Configuration.GetSection("ReverseProxy");

builder.Services.AddReverseProxy()
    .LoadFromConfig(reverseProxy);

builder.Services.AddRateLimiter(reteLimaterOptions =>
{
    reteLimaterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.Window = TimeSpan.FromSeconds(10);
        options.PermitLimit = 5;
    });
});

var app = builder.Build();

app.UseRateLimiter();
app.MapReverseProxy();

app.Run();
