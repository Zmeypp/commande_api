using CommandesAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuration de la base de données
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=commandeservice.db"));

// Configuration de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Activation de Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Création automatique de la base de données
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

// CRUD Routes
app.MapGet("/commandes", async (AppDbContext db) =>
    await db.Commandes.ToListAsync());

app.MapGet("/commandes/{id}", async (int id, AppDbContext db) =>
    await db.Commandes.FindAsync(id) is Commande commande ? Results.Ok(commande) : Results.NotFound());

app.MapPost("/commandes", async (AppDbContext db, Commande commande) =>
{
    db.Commandes.Add(commande);
    await db.SaveChangesAsync();
    return Results.Created($"/commandes/{commande.Id}", commande);
});

app.MapPut("/commandes/{id}", async (int id, AppDbContext db, Commande inputCommande) =>
{
    var commande = await db.Commandes.FindAsync(id);
    if (commande == null) return Results.NotFound();

    commande.Id = inputCommande.Id;
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/commandes/{id}", async (int id, AppDbContext db) =>
{
    var commande = await db.Commandes.FindAsync(id);
    if (commande == null) return Results.NotFound();

    db.Commandes.Remove(commande);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();
