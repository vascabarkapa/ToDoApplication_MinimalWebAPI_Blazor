using Microsoft.EntityFrameworkCore;
using Server;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

async Task<List<TodoItem>> GetTodoItems(ApplicationDbContext _db) =>
    await _db.TodoItems.ToListAsync();

app.MapGet("/api/todoItem", async (ApplicationDbContext _db) =>
    await _db.TodoItems.ToListAsync());

app.MapGet("/api/todoItem/{id}", async (ApplicationDbContext _db, int id) =>
    await _db.TodoItems.FindAsync(id) is TodoItem todoItem ?
    Results.Ok(todoItem) :
    Results.NotFound("There is not To Do Item!"));

app.MapPost("/api/todoItem", async (ApplicationDbContext _db, TodoItem todoItem) =>
{
    _db.TodoItems.Add(todoItem);
    await _db.SaveChangesAsync();

    return Results.Ok(await GetTodoItems(_db));
});

app.MapPut("/api/todoItem/{id}", async (ApplicationDbContext _db, TodoItem todoItem, int id) =>
{
    var dbTodoItem = await _db.TodoItems.FindAsync(id);

    if (dbTodoItem == null)
    {
        return Results.NotFound("There is not To Do Item!");
    }

    dbTodoItem.Name = todoItem.Name;
    dbTodoItem.Description = todoItem.Description;
    dbTodoItem.DateAndTime = todoItem.DateAndTime;
    dbTodoItem.Priority = todoItem.Priority;
    dbTodoItem.isDone = todoItem.isDone;

    await _db.SaveChangesAsync();

    return Results.Ok(await GetTodoItems(_db));
});

app.MapDelete("/api/todoItem/{id}", async (ApplicationDbContext _db, int id) =>
{
    var dbTodoItem = await _db.TodoItems.FindAsync(id);

    if (dbTodoItem == null)
    {
        Results.NotFound("There is not To Do Item!");
    }

    _db.TodoItems.Remove(dbTodoItem);
    await _db.SaveChangesAsync();

    return Results.Ok(await GetTodoItems(_db));
});

app.Run();