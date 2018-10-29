using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.NewFolder
{
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        // GET: api/<controller>
         [Produces("application/json")]
         public async Task<IActionResult> Get(string offenders)
         {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://iokrf-3d980.firebaseio.com");
                    var response = await client.GetAsync("/offenders.json");
                    response.EnsureSuccessStatusCode();

                    var result = await response.Content.ReadAsStringAsync();
                    return new JsonResult(result);

                }
                catch (HttpRequestException ex)
                {
                    return BadRequest(ex);
                }
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id?}")]
        public IActionResult Get(int id, string query)
        {
            return Ok(new Value { Id = id, Text = "value " + id });
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

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
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
