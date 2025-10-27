var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

List<Scheduling> schedulingList = new List<Scheduling>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet(
    "/scheduling",
    () =>
    {
        return schedulingList;
    }
);

app.MapPost(
    "/scheduling",
    (Scheduling newScheduling) =>
    {
        bool dateIsAlreadyTaken = false;
        foreach (var item in schedulingList)
        {
            if (item.Schedule == newScheduling.Schedule)
            {
                dateIsAlreadyTaken = true;
                break;
            }
        }

        if (dateIsAlreadyTaken)
        {
            return Results.Conflict($"Schedule {newScheduling.Schedule} is already taken.");
        }

        schedulingList.Add(newScheduling);

        return Results.Created($"/scheduling/{newScheduling.Id}", newScheduling);
    }
);

app.Run();

class Scheduling
{
    public int Id { get; set; }
    public string ClientName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime Schedule { get; set; }
}
