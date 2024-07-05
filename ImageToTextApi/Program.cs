using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<OcrService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapPost("image-to-text", async (IFormFile image, [FromServices] OcrService ocrService) =>
{
    try
    {
        return await ocrService.Process(image, "ara");
    }
    catch (Exception ex)
    {
        return "Error" + ex.Message;
    }
}).DisableAntiforgery();

app.Run();
