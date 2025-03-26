var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddOpenApi();

var app = builder.Build();

// Enable static files to be served (HTML, CSS, JS)
app.UseStaticFiles();

// Enable OpenAPI in development
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Prime Number Check API with proper response codes
app.MapGet("/api/isprime/{number}", (int number) =>
{
    // Check for invalid number (less than or equal to 1)
    if (number <= 1)
    {
        return Results.BadRequest("The number must be greater than 1.");
    }

    // Check if the number is prime
    if (number == 2)
    {
        return Results.Ok(new { Message = "The number is prime.", IsPrime = true });
    }

    if (number % 2 == 0)
    {
        return Results.Ok(new { Message = "The number is not prime.", IsPrime = false });
    }

    for (int i = 3; i <= Math.Sqrt(number); i += 2)
    {
        if (number % i == 0)
        {
            return Results.Ok(new { Message = "The number is not prime.", IsPrime = false });
        }
    }

    return Results.Ok(new { Message = "The number is prime.", IsPrime = true });
})
.WithName("CheckPrimeNumber");

app.Run();
