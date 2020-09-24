using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using membermicroservice.Models;
using membermicroservice.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace membermicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class memberController : ControllerBase
    {
        readonly log4net.ILog _log4net;
        Uri baseAddress = new Uri("https://localhost:44399/api"); //claim  
        HttpClient client;
        public memberController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            _log4net = log4net.LogManager.GetLogger(typeof(memberController));

        }
        [HttpGet]
        public List<memberclaim> Get()
        {
            _log4net.Info("MemberController Get View Claim Action Method is called!");
            List<memberclaim> ls = new List<memberclaim>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/claim").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<memberclaim>>(data);
            }
            return ls;
        }
        [HttpGet("{id}")]
        public void Get(int id)//for displaying the claims in the index page
        {
         
        }
        // POST api/<memberController>
        [HttpPost]
        public string Post([FromBody] memberclaim obj)//for submitting claims
        {
            _log4net.Info("MemberController Submit Claim Action Method is called!");
            try
            {
                string data = JsonConvert.SerializeObject(obj);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/claim", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return "success";

                }
                return "failed";
            }
            catch(Exception)
            {
                return "Exception Occured";
            }
        }

        // PUT api/<memberController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] memberclaim obj)//for viewing claim status
        {
            _log4net.Info("MemberController View Claim Status Action Method is called!");
            string data = JsonConvert.SerializeObject(obj);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/claim/" + id, content).Result;
 
        }

    }
}
