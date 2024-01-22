using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Rainfall Api",
        Version = "v1",
        Description = "An API which provides rainfall reading data",
        Contact = new OpenApiContact
        {
            Name = "Sorted",
            Url = new System.Uri("https://sorted.com")
        }
    });

    c.AddServer(new OpenApiServer
    {
        Url = "https://localhost:7025",
        Description = "Rainfall Api"
    });

    c.TagActionsBy(api => new[] {
                "Rainfall",
            });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
