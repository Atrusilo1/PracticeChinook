using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additonal Namespace
using System.ComponentModel;
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.POCOs;
using ChinookSystem.DAL;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class CustomerController
    {
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<RepresentitiveCustomer> RepresentitiveCustomers_Get(int employeeId)
        {      
            using (var context = new ChinookContext())
            {

                var result = from x in context.Customers 
                             where x.SupportRepId == employeeId
                             orderby x.LastName, x.FirstName 
                             select new RepresentitiveCustomer 
                             {
                                 Name = x.LastName,
                                 City = x.City,
                                 State = x.State,
                                 Phone = x.Phone,
                                 Email = x.Email                                          
                             };
        
                return result.ToList();
            }
        }

    }

   
}
