
var builder = WebApplication.CreateBuilder(args);

var MainAssembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(MainAssembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(config => {
    config.Connection(builder.Configuration.GetConnectionString("Database")!);
    config.Schema.For<ShoppingCart>().Identity(x => x.UserName);
   }
);

builder.Services.AddScoped<IBasketIRepository, BasketRepository>();

builder.Services.AddValidatorsFromAssembly(MainAssembly);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddCarter();

var app = builder.Build();

app.UseExceptionHandler(options => { });

app.MapCarter();

app.Run();
