using librarian_workplace_BLL.DTOs;
using librarian_workplace_BLL.Interfaces;
using librarian_workplace_DAL.Entities;
using librarian_workplace_DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace librarian_workplace_BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork Database;

        public BookService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public async Task AddAsync(BookInputDTO bookDTO)
        {
            if (await Database.Book.GetByIdAsync(bookDTO.ArticleNumber) != null)
                throw new ServiceErrorException(StatusCodes.Status400BadRequest, "Книга с текущим артикулом имеется в наличии.");

            DateTime publicationDate = new();

            string[] possibleFormats = { "dd/MM/yyyy", "dd-MM-yyyy", "yyyy-MM-dd", "MM/dd/yyyy", "d/MM/yyyy", "M/dd/yyyy",
                    "dd.MM.yyyy", "yyyy.MM.dd", "MM.dd.yyyy", "d.MM.yyyy", "M.dd.yyyy",  "DD.MM.yyyy", "dd.MM.YYYY", "DD.MM.YYYY"};
            DateTime.TryParseExact(bookDTO.PublicationDate, possibleFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out publicationDate);


            await Database.Book.AddAsync(new Book()
            {
                ArticleNumber = bookDTO.ArticleNumber,
                InsertDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                Author = bookDTO.Author,
                PublicationDate = publicationDate,
                InstancesNumber = bookDTO.InstancesNumber ?? 0
            });

            await Database.SaveAsync();
        }

        public async Task UpdateAsync(BookInputDTO bookDTO)
        {
            Book? book = await Database.Book.GetByIdAsync(bookDTO.ArticleNumber);
            if (book == null)
                throw new ServiceErrorException(StatusCodes.Status400BadRequest, "Книги с текущим артикулом нет в наличии.");
            if ((await Database.ReaderBook.FindAsync(e => e.ArticuleNumber == bookDTO.ArticleNumber)).Count() != 0)
                throw new ServiceErrorException(StatusCodes.Status400BadRequest, "Изменение невозможно, поскольку данная книга находится у читателя.");

            DateTime publicationDate = new();

            string[] possibleFormats = { "dd/MM/yyyy", "dd-MM-yyyy", "yyyy-MM-dd", "MM/dd/yyyy", "d/MM/yyyy", "M/dd/yyyy",
                    "dd.MM.yyyy", "yyyy.MM.dd", "MM.dd.yyyy", "d.MM.yyyy", "M.dd.yyyy",  "DD.MM.yyyy", "dd.MM.YYYY", "DD.MM.YYYY"};
            DateTime.TryParseExact(bookDTO.PublicationDate, possibleFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out publicationDate);

            book.UpdateDate = DateTime.Now;
            book.Author = bookDTO.Author ?? book.Author;
            book.PublicationDate = publicationDate != DateTime.MinValue ? publicationDate : book.PublicationDate;
            book.InstancesNumber = bookDTO.InstancesNumber ?? book.InstancesNumber;

            await Database.SaveAsync();
        }

        public async Task RemoveAsync(Guid articleNumber)
        {
            Book? book = (await Database.Book.FindAsync(e => e.ArticleNumber == articleNumber)).FirstOrDefault();
            if (book == null)
                throw new ServiceErrorException(StatusCodes.Status400BadRequest, "Книги с текущим артикулом нет в наличии.");

            if ((await Database.ReaderBook.FindAsync(e => e.ArticuleNumber == articleNumber)).Count() != 0)
                throw new ServiceErrorException(StatusCodes.Status400BadRequest, "Книга выданная читателю, удаление невозможно.");

            if (book != null)
                Database.Book.Remove(book);

            await Database.SaveAsync();
        }

        public async Task<BookInfoDTO> GetAsync(Guid articleNumber)
        {
            var book = Database.Book.ExtendedGet(e => e.ArticleNumber == articleNumber).FirstOrDefault();
            if (book == null)
                return null;
            return new BookInfoDTO()
            {
                ArticleNumber = book.ArticleNumber,
                InsertDate = book.InsertDate,
                UpdateDate = book.UpdateDate,
                Author = book.Author,
                PublicationDate = book.PublicationDate,
                InstancesNumber = book.InstancesNumber,
                ReaderIds = book.ReaderBooks.Select(x => x.Id).ToList() ?? null,
            };
        }

        public async Task<List<BookDTO>> GetAllAsync()
        {
            return (await Database.Book.GetAllAsync()).Select(x => new BookDTO()
            {
                ArticleNumber = x.ArticleNumber,
                InsertDate = x.InsertDate,
                UpdateDate = x.UpdateDate,
                Author = x.Author,
                PublicationDate = x.PublicationDate,
                InstancesNumber = x.InstancesNumber,
            }).ToList();
        }




    }
}
