using BlazingPizza.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient(); //allows the app to access HTTP commands 
builder.Services.AddSqlite<PizzaStoreContext>("Data Source=pizza.db"); // registers the new PizzaStoreContext amd provide file name for SQLite database


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

//creates a database scope with the PizzaStoreContext. If there isn't a database already created, it calls the SeedData static class to create one.

//Get the IServiceScopeFactory from the service provider 
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

//create scope
using (var scope = scopeFactory.CreateScope())
{
    //get scope service provider
    var scopedServices = scope.ServiceProvider;

    //get the PizzaContext from the scoped service provider
    var db =scope.ServiceProvider.GetRequiredService<PizzaStoreContext>();

    //ensure the database is created
    if (db.Database.EnsureCreated())
    {
        //seed the database with initial data
        SeedData.Initialize(db);
    }
}

app.Run();