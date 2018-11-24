using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    public class OffenderController : ControllerBase
    {
        private HttpClient Client;
        
        public OffenderController() : base()
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
                    var response = await Client.GetAsync("/offenders.json");
                    response.EnsureSuccessStatusCode();

                    var result = await response.Content.ReadAsStringAsync();
                    return Content(result, "application/json");

                }
                catch (HttpRequestException ex)
                {
                    return BadRequest(ex);
                }
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Value value)
        {
            if (!ModelState.IsValid)
            {
                // throw new InvalidOperationException("Invalid");
                return BadRequest(ModelState);
            }
            // Save the value to the DB
            return CreatedAtAction("Get", new { id = value.Id }, value);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        public class Value
        {
            public int Id { get; set; }

            [MinLength(3)]

            public string Text { get; set; }
        }

    }

    internal class OpenFirebaseReponse
    {

    }
}
