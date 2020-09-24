using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using claimmicroservice.Models;
using claimmicroservice.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace claimmicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class claimController : ControllerBase
    {
        readonly log4net.ILog _log4net;
        
        // GET: api/<claimController>
        [HttpGet]
        public IActionResult Get()//View Bills je by dafault index e or asbe
        {
            _log4net.Info("claimController get called");
            try
            {
                memberclaimrepo ob = new memberclaimrepo();
                return Ok(ob.give());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        // GET api/<claimController>/5
        [HttpGet("{id}")]
        public IActionResult Get1(int id)
        {
            _log4net.Info("claimController getbyId called");
            try
            {
                memberclaimrepo ob = new memberclaimrepo();
                List<memberclaim> ob1 = new List<memberclaim>();
                ob1 = ob.fetchclaimsformember(id);
                return Ok(ob1);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
       
        // POST api/<claimController>
        [HttpPost]
        public IActionResult Post([FromBody] memberclaim obj)
        {
            _log4net.Info("claimController postmethod called");
            if (ModelState.IsValid)
            {
                try
                {
                    memberclaimrepo ob = new memberclaimrepo();
                    ob.create(obj);
                    return Ok("SUCCESSFULLY ADDED");
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();

        }
        Uri baseAddress = new Uri("https://localhost:44367/api");   //Port No.
        HttpClient client;
        memberclaimrepo db;
        public claimController(memberclaimrepo _db)
        {
             db = _db;
            _log4net = log4net.LogManager.GetLogger(typeof(claimController));
            client = new HttpClient();
            client.BaseAddress = baseAddress;

        }

        // PUT api/<claimController>/5
        [HttpPut("{id}")]
        public void Put(int id,[FromBody] memberclaim obj)//edit korle j er ekta page e nie chole jai ota thakbe na
        {
            _log4net.Info("claimController put called");
            string s1 = obj.claimstatus;
            List<int> ls = new List<int>();
            int p = 0;
            int op=0;

            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/policy/1/2").Result;//[100,200,300,400]]
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ls = JsonConvert.DeserializeObject<List<int>>(data);
            }
            HttpResponseMessage response1= client.GetAsync(client.BaseAddress + "/policy/"+id).Result;//used to fetch the policyid of that particular memberid
            if (response1.IsSuccessStatusCode)
            {
                string data = response1.Content.ReadAsStringAsync().Result;
               op= Convert.ToInt32(data);               //it is giving corrcet result #policyid
                p = JsonConvert.DeserializeObject<int>(data);//it is becoming 0 i don't know
            }
            int d = obj.benefitid;
            HttpResponseMessage response2 = client.GetAsync(client.BaseAddress + "/policy/" + op+"/"+id+"/"+d).Result;
            int o=0;
            if (response2.IsSuccessStatusCode)
            {
                string data = response2.Content.ReadAsStringAsync().Result;
                o = Convert.ToInt32(data);               //it is giving corrcet result #policyid
               // p = JsonConvert.DeserializeObject<int>(data);//it is becoming 0 i don't know
            }
            if (obj.claimedamount > obj.billedamount)//if the bill is very less
            {
                //  return "Rejected";
                obj.claimstatus = "REJECTED";
            }
            if(obj.claimedamount>o)//it checks for all the benefit ids also for benefitid even when no benefit id is selected
            {
                obj.claimstatus = "REJECTED";
            }
            obj.claimstatus = "ACCEPTED";
            s1 = obj.claimstatus;
            //memberclaimrepo ob = new memberclaimrepo();
            //memberclaim x = new memberclaim();
            //x = ob.getclaim(id);
            //int mid = x.memberid;
            

        }
    }
}
