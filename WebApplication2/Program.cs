using System.Net.NetworkInformation;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using WebApplication2.Data;
using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EStoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("" +
        "EStoreConnectionString"));
});

builder.Services.Configure<JsonOptions>(options => {
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();
using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider serviceProvider = scope.ServiceProvider;

    await SeedData.Initialize(serviceProvider);
}


app.MapGet("/stores/{storenumber}/inventory", (EStoreContext context, Guid storeNumber) =>
{
    try
    {
        // Find data in the database
        HashSet<LaptopStore> laptops = context.LaptopStore.Where
        (l => l.StoreNumber == storeNumber && l.Quantity > 0)
        .Include(l => l.Laptops)
        .ThenInclude(l => l.Brand)
        .ToHashSet();

        return Results.Ok(laptops); ;
    }
    catch (InvalidOperationException ex)
    {
        return Results.NotFound(ex.Message);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapPost("stores/{storeNumber}/{laptopNumber}/changeQuantity", (EStoreContext db,
    Guid storeNumber, Guid laptopNumber, int quantity) =>
{
    try
    {
        LaptopStore laptop = db.LaptopStore.FirstOrDefault(
            ls => ls.LaptopsNumber == laptopNumber && ls.StoreNumber == storeNumber);

        laptop.Quantity = quantity;
        db.SaveChanges();

        return Results.Ok(laptop);

    }
    catch (InvalidOperationException ex)
    {
        return Results.NotFound(ex.Message);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});



app.Run();



  