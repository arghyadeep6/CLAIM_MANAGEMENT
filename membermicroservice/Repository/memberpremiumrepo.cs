using membermicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace membermicroservice.Repository
{
    public class memberpremiumrepo
    {
        public List<memberpremium> m = new List<memberpremium>()
        {
         new memberpremium()
         {
             memberid=1,
             policyid=1,
             topup=1000,
             premium=2000,
             paiddate=DateTime.Today
         },
         new memberpremium()
         {
             memberid=2,
             policyid=2,
             topup=1000,
             premium=2000,
             paiddate=DateTime.Today
         },
         new memberpremium()
         {
             memberid=3,
             policyid=3,
             topup=2000,
             premium=3000,
             paiddate=DateTime.Today
         },
         new memberpremium()
         {
             memberid=4,
             policyid=4,
             topup=4000,
             premium=5000,
             paiddate=DateTime.Today
         }

};
          public virtual List<memberpremium> fun()
        {
            return m;
        }
            public virtual memberpremium getViewBills(int mid, int pid)
        {
            memberpremium memberp = new memberpremium();
           foreach(var item in m)
            {
                if(item.memberid == mid && item.policyid == pid)
                {
                    return item;
                }
            }
            return memberp;
        }
    }
}
