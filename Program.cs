using Microsoft.EntityFrameworkCore;
using KnowledgeSchool.Data;
using KnowledgeSchool;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DataContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
 );

var app = builder.Build();

app.MapGet("/", ()=> "welcome");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// { block start 
// movies crud
app.MapPost("/movie", async (DataContext context, Movie movie) =>
{
    context.Movies.Add(movie);
    await context.SaveChangesAsync();
    return Results.Ok(await context.Movies.ToListAsync());
});

app.MapGet("/movie", async (DataContext context) => await context.Movies.ToListAsync());


app.MapGet("/movie/{id}", async (DataContext context, int id) =>
    await context.Movies.FindAsync(id) is Movie movie ?
        Results.Ok(movie) :
        Results.NotFound("No movie here. :/"));

app.MapPut("/movie/{id}", async (DataContext context, Movie movie, int id) =>
{
    var dbMovie = await context.Movies.FindAsync(id);
    if (dbMovie == null) return Results.NotFound("Nope.");

    dbMovie.Title = movie.Title;
    dbMovie.Image = movie.Image;
    dbMovie.Url = movie.Url;
    await context.SaveChangesAsync();

    return Results.Ok(await context.Movies.ToListAsync());
});

app.MapDelete("/movie/{id}", async (DataContext context, int id) =>
{
    var dbMovie = await context.Movies.FindAsync(id);
    if (dbMovie == null) return Results.NotFound("No movie to delete here. :)");

    context.Movies.Remove(dbMovie);
    await context.SaveChangesAsync();

    return Results.Ok(await context.Movies.ToListAsync());
});
// movies
// block end } 



// { block start 
// users crud
app.MapGet("/user", async (DataContext context) => await context.Users.ToListAsync());

app.MapGet("/user/{id}", async (DataContext context, int id) =>
    await context.Users.FindAsync(id) is User user ?
        Results.Ok(user) :
        Results.NotFound("No user here. :/"));

app.MapPost("/user", async (DataContext context, User user) =>
{
    context.Users.Add(user);
    await context.SaveChangesAsync();
    return Results.Ok(await context.Users.ToListAsync());
});

app.MapPut("/user/{id}", async (DataContext context, User user, int id) =>
{
    var dbUser = await context.Users.FindAsync(id);
    if (dbUser == null) return Results.NotFound("Nope.");

    dbUser.Name = user.Name;
    dbUser.Email = user.Email;
    dbUser.Password = user.Password;
    dbUser.Phone = user.Phone;
    dbUser.Address = user.Address;
  
    await context.SaveChangesAsync();

    return Results.Ok(await context.Users.ToListAsync());
});

app.MapDelete("/user/{id}", async (DataContext context, int id) =>
{
    var dbUser = await context.Users.FindAsync(id);
    if (dbUser == null) return Results.NotFound("No user to delete here. :)");

    context.Users.Remove(dbUser);
    await context.SaveChangesAsync();

    return Results.Ok(await context.Users.ToListAsync());
});
// users 
//  block end }


// block start {
// songs crud
app.MapGet("/song", async (DataContext context) => await context.Songs.ToListAsync());

app.MapGet("/song/{id}", async (DataContext context, int id) =>
    await context.Songs.FindAsync(id) is Song song ?
        Results.Ok(song) :
        Results.NotFound("No song here. :/"));

app.MapPost("/song", async (DataContext context, Song song) =>
{
    context.Songs.Add(song);
    await context.SaveChangesAsync();
    return Results.Ok(await context.Songs.ToListAsync());
});

app.MapPut("/song/{id}", async (DataContext context, Song song, int id) =>
{
    var dbSong = await context.Songs.FindAsync(id);
    if (dbSong == null) return Results.NotFound("Nope.");

    dbSong.Title = song.Title;
    dbSong.Image = song.Image;
    dbSong.Url = song.Url;
    await context.SaveChangesAsync();

    return Results.Ok(await context.Songs.ToListAsync());
});

app.MapDelete("/song/{id}", async (DataContext context, int id) =>
{
    var dbSong = await context.Songs.FindAsync(id);
    if (dbSong == null) return Results.NotFound("No song to delete here. :)");

    context.Songs.Remove(dbSong);
    await context.SaveChangesAsync();

    return Results.Ok(await context.Songs.ToListAsync());
});
// songs
// block end } 










app.Run();
