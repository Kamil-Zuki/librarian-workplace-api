using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace librarian_workplace_BLL.DTOs
{
    public class ReaderDTO
    {
        public Guid Id { get; set; }
        public string FIO { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
