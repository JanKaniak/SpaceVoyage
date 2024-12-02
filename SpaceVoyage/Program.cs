using SpaceVoyage.Components;
using Microsoft.EntityFrameworkCore;
using SpaceVoyage.Data;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var PatchnoteConnectionString = builder.Configuration.GetConnectionString("PatchnoteDB");
var UserConnectionString = builder.Configuration.GetConnectionString("UserDB");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContextFactory<PatchnoteDataContext>(options => options.UseSqlite(PatchnoteConnectionString));
builder.Services.AddDbContextFactory<UserDataContext>(options => options.UseSqlite(UserConnectionString));

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();


app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
