using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRProject.Core.Entities
{
    public interface ISoftDeleteable
    {
        bool IsActive { get; set; }
    }
}
