using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        private HttpClient Client;

        public EventController() : base()
        {
            this.Client = new HttpClient
            {
                BaseAddress = new Uri("https://iokrf-3d980.firebaseio.com")

            };
        }

        // GET: api/<controller>
        [Produces("application/json")]
        public async Task<IActionResult> PostCallAPI(string url, object jsonObject)
        {
            try
            {
                var response = await Client.GetAsync("/events.json");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                return Content(result, "application/json");

            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
