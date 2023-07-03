using HRProject.Core.DataAccess.EntityFramework;
using HRProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.DataAccess.Abstract
{
    public interface IExpenseRepository : IRepository<Expense>
    {
    }
}
