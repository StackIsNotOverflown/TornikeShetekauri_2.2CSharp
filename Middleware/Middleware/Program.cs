var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/" || context.Request.Path == "/image")
    {
        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "MyFolder", "Screenshot (44).png");

        if (File.Exists(imagePath))
        {
            context.Response.ContentType = "image/Png"; 
            var imageBytes = await File.ReadAllBytesAsync(imagePath);
            await context.Response.Body.WriteAsync(imageBytes);
            return;
        }
        else
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("სახელი შეცვალე");
            return;
        }
    }

    await next();
});

app.Run(); 