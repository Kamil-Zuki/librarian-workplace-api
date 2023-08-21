using librarian_workplace_BLL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace librarian_workplace_BLL.Interfaces
{
    public interface IReaderService
    {
        Task AddAsync(ReaderInputDTO readerDTO);
        Task UpdateAsync(ReaderUpdateDTO readerDTO);
        Task RemoveAsync(Guid id);
        Task GiveBookAsync(ReaderGiveDTO readerBooks);
        Task<ReaderGiveDTO> GetAsync(Guid id);
        Task<List<ReaderGiveDTO>> GetAsync(string FIO);
    }
}
