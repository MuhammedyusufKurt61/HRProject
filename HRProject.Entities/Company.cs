using HRProject.Core.Entities;
using HRProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Entities
{
    public class Company:IEntity
    {
        public Guid Id { get; set; }

        public string CompanyName { get; set; }   
        
        public string TaxNo { get; set; }  
        
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public DateTime FoundationYear { get; set; }   
                
        public ICollection<Personel> Personels { get; set; }
        public Company()
        {
            Personels = new HashSet<Personel>();
        }    
    }
}
