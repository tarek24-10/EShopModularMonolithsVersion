using Basket;
using Catalog;
using Ordering;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCatallogModule(builder.Configuration)
                .AddOrderingModule(builder.Configuration)
                .AddBasketModule(builder.Configuration);

var app = builder.Build();

app.Run();
