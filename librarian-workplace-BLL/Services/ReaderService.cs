using librarian_workplace_BLL.DTOs;
using librarian_workplace_BLL.Interfaces;
using librarian_workplace_DAL.Entities;
using librarian_workplace_DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace librarian_workplace_BLL.Services
{
    public class ReaderService : IReaderService
    {
        private readonly IUnitOfWork Database;

        public ReaderService(IUnitOfWork uow)
        {
            Database = uow;
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
        }

        public async Task UpdateAsync(ReaderUpdateDTO readerDTO)
        {
            var reader = await Database.Reader.GetByIdAsync(readerDTO.Id);
            if(reader == null)
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

        public async Task GiveBook(ReaderGiveDTO readerBooks)
        {
            readerBooks.ArticleNumbers.ForEach(async bookId =>
            {
                await Database.ReaderBook.AddAsync(new ReaderBook()
                {
                    Id = Guid.NewGuid(),
                    ReaderId = readerBooks.Id,
                    ArticuleNumber = bookId

                });
            });
        }
    }

}

