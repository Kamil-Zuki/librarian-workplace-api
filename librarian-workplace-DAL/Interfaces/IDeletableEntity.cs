using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace librarian_workplace_DAL.Interfaces
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }
    }
}
