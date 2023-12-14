using Microsoft.AspNetCore.Mvc;
using pizza_mama.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pizza_mama_V2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : Controller
    {
        // GET: api/<ApiController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("GetPizzas")]
        public IActionResult GetPizzas()
        {
            var pizza = new Pizza()
            {
                nom = "Pizza Test",
                prix = 8,
                vegetarienne = false,
                ingredients = "tomate, oignons, boeuf, Merguez, oeuf, moza",
            };

            return Json(pizza);
        }

    }
}
