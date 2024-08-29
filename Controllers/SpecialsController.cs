using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazingPizza.Data;
using System.Security.Cryptography.X509Certificates;

//This class creates a controller that allows us to query the database for pizza specials and returns them as JSON at the (http://localhost:5000/specials) URL.

namespace BlazingPizza.Controllers;

// Defines the route for this controller as "Specials"
[Route("Specials")]
[ApiController]
public class SpecialsController : Controller
{
    
    private readonly PizzaStoreContext _db;
    // Constructor that injects the PizzaStoreContext dependency
    public SpecialsController(PizzaStoreContext db)
    {
        _db = db;
    }

    
    // GET: /Specials
    // Retrieves a list of pizza specials and returns them as JSON
    [HttpGet]  
    public async Task<ActionResult<List<PizzaSpecial>>> GetSpecials()
    {
         // Query the database for specials, order by BasePrice descending, and return as JSON
        return(await _db.Specials.ToListAsync()).OrderByDescending(s => s.BasePrice).ToList();
    }

    

}