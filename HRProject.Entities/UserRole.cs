using HRProject.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Entities
{
    public class UserRole:IdentityRole<Guid>, IEntity
    {
    }
}
