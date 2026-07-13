var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCarter(configurator: config => 
//{
//    var catalogModules = typeof(CatalogModule).Assembly.GetTypes()
//        .Where(t => t.IsAssignableTo(typeof(ICarterModule)))
//        .ToArray();

//    config.WithModules(catalogModules);
//});

builder.Services.AddCarterWithAssemblies(typeof(CatalogModule).Assembly //,
    //typeof(OrderingModule).Assembly,
    //typeof(BasketModule).Assembly
    );

builder.Services.AddCatalogModule(builder.Configuration)
                .AddOrderingModule(builder.Configuration)
                .AddBasketModule(builder.Configuration);

var app = builder.Build();

app.MapCarter();

app.UseCatalogModule()
   .UseOrderingModule()
   .UseBasketModule();

app.Run();
