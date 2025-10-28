using Microsoft.EntityFrameworkCore;
using SchedulingApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Scheduling API v1");
    });
}

app.MapGet(
    "/scheduling",
    (AppDbContext db) =>
    {
        return db.Schedulings.ToList();
    }
);

app.MapPost(
    "/scheduling",
    async (Scheduling newScheduling, AppDbContext db) =>
    {
        bool dateIsAlreadyTaken = await db.Schedulings.AnyAsync(item =>
            item.Schedule == newScheduling.Schedule
        );

        if (dateIsAlreadyTaken)
        {
            return Results.Conflict($"Schedule {newScheduling.Schedule} is already taken.");
        }

        db.Schedulings.Add(newScheduling);

        await db.SaveChangesAsync();

        return Results.Created($"/scheduling/{newScheduling.Id}", newScheduling);
    }
);

app.Run();
