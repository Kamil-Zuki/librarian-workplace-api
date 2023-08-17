using librarian_workplace_BLL.Interfaces;
using librarian_workplace_BLL.Services;
using librarian_workplace_DAL.EF;
using librarian_workplace_DAL.Interfaces;
using librarian_workplace_DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace librarian_workplace_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddDbContext<LibraryContext>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("Db")));

            builder.Services.AddScoped<IReaderService, ReaderService>();
            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Librarian Workplace API", Version = "v1" });
            });
            var app = builder.Build();
            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Librarian Workplace API");
            });


            app.Run();
        }
    }
}