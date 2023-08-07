using System.Linq;
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
// Seedind data
using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider serviceProvider = scope.ServiceProvider;

    await SeedData.Initialize(serviceProvider);
}

// Showing all the laptops in the store By considering quantity greater than 0
app.MapGet("/stores/{storenumber}/inventory", (EStoreContext context, Guid storeNumber) =>
{
    try
    {
       
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

// To show the store according to the province
app.MapGet("/stores/show", (EStoreContext context) =>
{
    try
    {
        
        List<Store> allStoreLocations = context.Store
        .Include(sl => sl.LaptopStores)
        .ThenInclude(sl => sl.Laptops)
        .ToList();
// Created dictionary to store store according to province pair
        Dictionary<Canadian, HashSet<Store>> storesByProvince = allStoreLocations
            .GroupBy(l => l.Province)
            .ToDictionary(k => k.Key, g => g.ToHashSet());

        return Results.Ok(storesByProvince);

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

// to show the laptop average by passing the brand Name
app.MapGet("/laptops/{brandName}/average", (EStoreContext context, string brandName) =>
{
    try
    {
       
        HashSet<Laptop> laptops = context.Laptops.Where(l => l.Brand.Name == brandName).ToHashSet();

        int laptopCount = laptops.Count();
        decimal averagePrice = laptops.Average(ls => ls.Price);

        return Results.Ok(new
        {
            LaptopCount = laptopCount,
            AveragePrice = averagePrice
        });

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


// to change the quantity by passing storeNumber and laptopNumber
app.MapPost("stores/{storeNumber}/{laptopNumber}/{changeQuantity}", (EStoreContext context, Guid storeNumber, Guid laptopNumber, int changeQuantity) =>
{
    try
    {
        LaptopStore? laptop = context.LaptopStore.FirstOrDefault(
           ls => ls.LaptopsNumber == laptopNumber && ls.StoreNumber == storeNumber);

        if (laptop == null)
        {
            throw new ArgumentOutOfRangeException("There is no relation between storeNumber and laptopNumber") ;
        }

        laptop.Quantity = changeQuantity;
        context.SaveChanges();

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

// to search the laptop
app.MapGet("/laptops/search", (EStoreContext context,
                                     decimal? priceAbove,
                                     decimal? priceBelow,
                                     Guid? storeNumber,
                                     string? province,
                                     LaptopCondition? condition,
                                     Guid? brandId,
                                     string? searchPhrase) =>
{
    try
    {
        HashSet<Laptop> Laptop = context.Laptops.Include(l => l.Brand).ToHashSet(); 
        
        if (priceAbove.HasValue)
        {
            // value must be greater than zero
            if (!(priceAbove > 0))
            {
                throw new ArgumentOutOfRangeException(nameof(priceAbove));
            }

            return Results.Ok(context.Laptops
                            .Where(l => l.Price > priceAbove)
                            .Include(l => l.Brand)
                            .ToHashSet());
        }

      
        if (priceBelow.HasValue)
        {
            if (!(priceBelow > 0))
            {
                throw new ArgumentOutOfRangeException(nameof(priceBelow));
            }

            return Results.Ok(context.Laptops
                            .Where(l => l.Price < priceBelow)
                            .Include(l => l.Brand)
                            .ToHashSet());
        }
        if (storeNumber.HasValue)
        {
            return Results.Ok(context.LaptopStore
                    .Where(sl =>
                    sl.StoreNumber == storeNumber
                    && sl.Quantity > 0)
                    .Include(sl => sl.Store)
                    .ToHashSet());
        }
        else if (!String.IsNullOrEmpty(province))
        {
            return Results.Ok(context.LaptopStore
                    .Where(sl =>
                    sl.Store.Province.Equals(province)
                    && sl.Quantity > 0)
                    .Include(sl => sl.Store)
                    .ToHashSet());
        }

      
        if (condition.HasValue)
        {
            return Results.Ok(context.Laptops
                 .Where(l => l.Condition == condition)
                 .Include(l => l.Brand)
                 .ToHashSet());
        }

      
        if (brandId.HasValue)
        {
            return Results.Ok(context.Laptops
                .Where(l => l.BrandId == brandId)
                .Include(l => l.Brand)
                .ToHashSet());
        }

        
        if (!string.IsNullOrEmpty(searchPhrase))
        {
            return Results.Ok(context.Laptops
                .Where(l => l.Model.Contains(searchPhrase))
                .Include(l => l.Brand)
                .ToHashSet());
        }

      
        return Results.Ok(context.Laptops
                      .Include(l => l.Brand)
                      .ToHashSet());
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



  