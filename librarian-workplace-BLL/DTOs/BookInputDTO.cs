using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace librarian_workplace_BLL.DTOs
{
    public class BookInputDTO
    {
        public Guid ArticleNumber { get; set; }
        public string? Author { get; set; }
        public string? PublicationDate { get; set; }
        public int? InstancesNumber { get; set; }
    }
}
