using librarian_workplace_BLL.DTOs;
using librarian_workplace_BLL.Interfaces;
using librarian_workplace_DAL.Entities;
using librarian_workplace_DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection.PortableExecutable;

namespace librarian_workplace_BLL.Services
{
    public class ReaderService : IReaderService
    {
        private readonly IUnitOfWork Database;
        private readonly IBookService _bookService;
        public ReaderService(IUnitOfWork uow, IBookService bookService)
        {
            Database = uow;
            _bookService = bookService;
        }

        public async Task AddAsync(ReaderInputDTO readerDTO)
        {
            DateTime birthDate = new();
            string[] possibleFormats = { "dd/MM/yyyy", "dd-MM-yyyy", "yyyy-MM-dd", "MM/dd/yyyy", "d/MM/yyyy", "M/dd/yyyy",
                    "dd.MM.yyyy", "yyyy.MM.dd", "MM.dd.yyyy", "d.MM.yyyy", "M.dd.yyyy",  "DD.MM.yyyy", "dd.MM.YYYY", "DD.MM.YYYY"};
            DateTime.TryParseExact(readerDTO.BirthDate, possibleFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);

            await Database.Reader.AddAsync(new Reader()
            {
                Id = Guid.NewGuid(),
                FIO = readerDTO.FIO,
                DateAdded = DateTime.Now,
                DateUpdated = DateTime.Now,
                BirthDate = birthDate
            });
            await Database.SaveAsync();
        }

        public async Task UpdateAsync(ReaderUpdateDTO readerDTO)
        {
            var reader = await Database.Reader.GetByIdAsync(readerDTO.Id);
            if (reader == null)
                throw new ServiceErrorException(StatusCodes.Status400BadRequest, "Указзанного читателя нет в системе.");

            DateTime birthDate = new();
            string[] possibleFormats = { "dd/MM/yyyy", "dd-MM-yyyy", "yyyy-MM-dd", "MM/dd/yyyy", "d/MM/yyyy", "M/dd/yyyy",
                    "dd.MM.yyyy", "yyyy.MM.dd", "MM.dd.yyyy", "d.MM.yyyy", "M.dd.yyyy",  "DD.MM.yyyy", "dd.MM.YYYY", "DD.MM.YYYY"};
            DateTime.TryParseExact(readerDTO.BirthDate, possibleFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out birthDate);

            Database.Reader.Update(new Reader()
            {
                Id = readerDTO.Id,
                FIO = readerDTO.FIO ?? reader.FIO,
                DateAdded = reader.DateAdded,
                DateUpdated = DateTime.Now,
                BirthDate = birthDate != DateTime.MinValue ? birthDate : reader.BirthDate
            });
            await Database.SaveAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var reader = await Database.Reader.GetByIdAsync(id);
            if (reader != null && (await Database.ReaderBook.FindAsync(e => e.ReaderId == id)).Count() != 0)
                throw new ServiceErrorException(StatusCodes.Status400BadRequest, "Удаление указанного читателя невозможно, поскольку он имеет на руках выданные книги.");
            Database.Reader.Remove(reader);
            await Database.SaveAsync();
        }

        public async Task GiveBookAsync(ReaderGiveDTO readerBooks)
        {
            if (await Database.Reader.GetByIdAsync(readerBooks.Id) == null)
                throw new ServiceErrorException(StatusCodes.Status400BadRequest, "Указзанного читателя нет в системе.");

            var provedBooks = (await Database.Book.FindAsync(e => readerBooks.ArticleNumbers.Contains(e.ArticleNumber))).Select(x => x.ArticleNumber).ToList();
            var unprovedBooks = readerBooks.ArticleNumbers.Except(provedBooks).ToList();
            if (unprovedBooks.Count() != 0)
            {
                string unprovedBooksList = string.Join(", ", unprovedBooks);
                throw new ServiceErrorException(StatusCodes.Status400BadRequest, $"Указанных книг нет в наличии: {unprovedBooksList}.");
            }

            var tasks = provedBooks.Select(async articleNumber =>
            {
                var book = await Database.Book.GetByIdAsync(articleNumber);
                if (book.InstancesNumber <= 0)
                    throw new ServiceErrorException(StatusCodes.Status400BadRequest, $"Указанных книг нет в наличии.");
                book.InstancesNumber--;

                await Database.ReaderBook.AddAsync(new ReaderBook()
                {
                    Id = Guid.NewGuid(),
                    ReaderId = readerBooks.Id,
                    ArticuleNumber = articleNumber,
                    DateBorrowed = DateTime.Now
                });

                await _bookService.UpdateAsync(new BookInputDTO()
                {
                    ArticleNumber = articleNumber,
                    InstancesNumber = book.InstancesNumber
                });
            });

            await Task.WhenAll(tasks);

            await Database.SaveAsync();
        }

        public async Task ReturnBookAsync(ReaderGiveDTO readerBooks)
        {
            if (await Database.Reader.GetByIdAsync(readerBooks.Id) == null)
                throw new ServiceErrorException(StatusCodes.Status400BadRequest, "Указзанного читателя нет в системе.");

            var provedBooks = (await Database.Book.FindAsync(e => readerBooks.ArticleNumbers.Contains(e.ArticleNumber))).Select(x => x.ArticleNumber).ToList();
            var unprovedBooks = readerBooks.ArticleNumbers.Except(provedBooks).ToList();
            if (unprovedBooks.Count() != 0)
            {
                string unprovedBooksList = string.Join(", ", unprovedBooks);
                throw new ServiceErrorException(StatusCodes.Status400BadRequest, $"Указанных книг нет в наличии: {unprovedBooksList}.");
            }

            var tasks = provedBooks.Select(async articleNumber =>
            {
                var book = await Database.Book.GetByIdAsync(articleNumber);
                if (book.InstancesNumber <= 0)
                    throw new ServiceErrorException(StatusCodes.Status400BadRequest, $"Указанных книг нет в наличии.");
                book.InstancesNumber++;


                var readerBook = (await Database.ReaderBook.FindAsync(e => e.ReaderId == readerBooks.Id && e.ArticuleNumber == articleNumber)).FirstOrDefault();
                if (readerBook != null)
                {
                    Database.ReaderBook.Remove(readerBook);

                    await _bookService.UpdateAsync(new BookInputDTO()
                    {
                        ArticleNumber = articleNumber,
                        InstancesNumber = book.InstancesNumber
                    });
                }
            });

            await Task.WhenAll(tasks);

            await Database.SaveAsync();
        }

        public async Task<ReaderGiveDTO> GetAsync(Guid id)
        {
            var reader = await Database.Reader.GetByIdAsync(id);
            List<ReaderBook>? booksHistory = new();
            if (reader != null)
            {
                booksHistory = (await Database.ReaderBook.FindAsync(e => e.ReaderId == id)).ToList();
            }

            return new ReaderGiveDTO()
            {
                Id = reader.Id,
                FIO = reader.FIO,
                DateAdded = reader.DateAdded,
                DateUpdated = reader.DateUpdated,
                BirthDate = reader.BirthDate,
                ArticleNumbers = booksHistory.Select(x => x.ArticuleNumber).ToList() ?? null
            };
        }

        public async Task<List<ReaderGiveDTO>> GetAsync(string FIO)
        {
            var fioParts = FIO.ToLower().Split(' ');
            var readers = (await Database.Reader.GetAllAsync()).Where(e => fioParts.Any(part => e.FIO.ToLower().Contains(part)));
            List<ReaderBook> booksHistory = new();
            List<ReaderGiveDTO> readerBooks = new();
            if (readers != null)
            {
                foreach(var reader in readers)
                {
                    booksHistory = (await Database.ReaderBook.FindAsync(e => e.ReaderId == reader.Id)).ToList();
                    readerBooks.Add(new ReaderGiveDTO()
                    {
                        Id = reader.Id,
                        FIO = reader.FIO,
                        DateAdded = reader.DateAdded,
                        DateUpdated = reader.DateUpdated,
                        BirthDate = reader.BirthDate,
                        ArticleNumbers = booksHistory.Select(x => x.ArticuleNumber).ToList() ?? null
                    });
                }
            }

            return readerBooks;
        }
    }

}

