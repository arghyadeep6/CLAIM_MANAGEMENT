using NUnit.Framework;
using claimmicroservice.Repository;
using claimmicroservice.Controllers;
using claimmicroservice.Models;
using Microsoft.AspNetCore.Mvc;





namespace claimmicroserviceTesting
{
    public class claimControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            memberclaimrepo mcr = new memberclaimrepo();
            claimController con = new claimController(mcr);
            var data = con.Get() as OkObjectResult;
            Assert.AreEqual(200, data.StatusCode);

        }

        [Test]
        public void Test2()
        {
            memberclaimrepo cr = new memberclaimrepo();
            claimController cont = new claimController(cr);
            var data = cont.Get1(1) as OkObjectResult;
            Assert.AreEqual(200, data.StatusCode);

        }

        [Test]
        public void Test3()
        {
            memberclaim mem1 = new memberclaim {
                memberid = 1,
                claimid = 2,
                billedamount = 1200,
                claimedamount = 1000,
                claimstatus = "Pending",
                benefitid = 1
            };
            memberclaimrepo acr = new memberclaimrepo();
            claimController contr = new claimController(acr);
            var data = contr.Post(mem1) as OkObjectResult;
            Assert.AreEqual(200, data.StatusCode);

        }

    }
}