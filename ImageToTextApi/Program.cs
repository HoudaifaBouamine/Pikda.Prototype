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
        var text = await ocrService.Process(image, "ara");
        File.AppendAllText("Test.md",text + "\n\n\n---------------------------------------\n");
        return text;
    }
    catch (Exception ex)
    {
        return "Error" + ex.Message;
    }
}).DisableAntiforgery();

app.MapPost("imagePath-to-text", async (string path, [FromServices] OcrService ocrService) =>
{
    try
    {
        var text = ocrService.Process(path, "ara");
        File.AppendAllText("Test.md", text + "\n\n\n---------------------------------------\n");
        return text;
    }
    catch (Exception ex)
    {
        return "Error" + ex.Message;
    }
}).DisableAntiforgery();


app.Run();
