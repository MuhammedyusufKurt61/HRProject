using HRProject.Core.DataAccess.EntityFramework;
using HRProject.DataAccess.Abstract;
using HRProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.DataAccess.Concrete.EntityFramework
{
    public class EfExpanseRepository:EfRepositoryBase<Expense, HRProjectContext> , IExpenseRepository
    {
    }
}
