using librarian_workplace_BLL.DTOs;

namespace librarian_workplace_BLL.Interfaces
{
    public interface IBookService
    {
        Task AddAsync(BookInputDTO bookDTO);
        Task<BookInfoDTO> GetAsync(Guid articleNumber);
        Task UpdateAsync(BookInputDTO bookDTO);
        Task RemoveAsync(Guid articleNumber);
        Task<List<BookDTO>> GetAllAsync();
    }
}
