using membermicroservice.Controllers;
using membermicroservice.Models;
using membermicroservice.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace membermicroservicetest
{
    public class Tests
    {
        List<memberpremium> premiumList = new List<memberpremium>();
        [SetUp]
        public void Setup()
        {
            premiumList = new List<memberpremium>
            {
                 new memberpremium
                 {
                    memberid=1,
                    policyid=1,
                    topup=1000,
                    premium=2000,
                    paiddate=DateTime.Today
                 },
                 new memberpremium
                 {
                    memberid=2,
                    policyid=2,
                    topup=1000,
                    premium=2000,
                    paiddate=DateTime.Today
                 },
                 new memberpremium
                 {
                    memberid=3,
                    policyid=3,
                    topup=2000,
                    premium=3000,
                    paiddate=DateTime.Today
                 },
                 new memberpremium
                 {
                    memberid=4,
                    policyid=4,
                    topup=4000,
                    premium=5000,
                    paiddate=DateTime.Today
                 },
                  
            };
            

        }

        [Test]
        public void GetAllTest()
        {
            Mock<memberpremiumrepo> mock = new Mock<memberpremiumrepo>();
            mock.Setup(p => p.fun()).Returns(premiumList);
            billsController controller = new billsController();
            IEnumerable<memberpremium> result = controller.Get();
            Assert.AreEqual(4, result.Count());
            

        }
        [Test]
        public void GetByIds()
        {
            Mock<memberpremiumrepo> mock = new Mock<memberpremiumrepo>();
            mock.Setup(p => p.getViewBills(1, 1)).Returns((memberpremium)premiumList.Where(x => x.memberid == 1).Where(y => y.policyid == 1));
            billsController controller = new billsController();
            IActionResult data = controller.Get1(1, 1);
            var result = data as ObjectResult;
            Assert.AreEqual(200, result.StatusCode);
        }
    }
}