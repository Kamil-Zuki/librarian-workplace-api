using librarian_workplace_BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace librarian_workplace_api.Controllers
{
    [Route("api/v1/reader")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        private readonly IReaderService _readerService; 
        public ReaderController(IReaderService readerService)
        {
            _readerService = readerService;
        }


    }
}
