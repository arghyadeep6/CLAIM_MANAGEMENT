using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using membermicroservice.Models;
using membermicroservice.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace membermicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class billsController : ControllerBase
    {
        // GET: api/<billsController>
        [HttpGet]
        public IEnumerable<memberpremium> Get()
        {
            memberpremiumrepo ob = new memberpremiumrepo();
            List<memberpremium> l = new List<memberpremium>();
            l = ob.fun();
            return l;
        }

        // GET api/<billsController>/5
        [HttpGet("{mid}/{pid}")]
        public IActionResult Get1(int mid , int pid)
        {
            memberpremiumrepo ob = new memberpremiumrepo();
            memberpremium l = new memberpremium();
            try
            {
                l = ob.getViewBills(mid, pid);
                if (l == null)
                    return BadRequest(l);
                return Ok(l);
            }
            catch(Exception)
            {
                return BadRequest(l);
            }
        }


    }
}
