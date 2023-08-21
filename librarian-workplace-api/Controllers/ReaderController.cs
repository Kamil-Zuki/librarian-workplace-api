using librarian_workplace_BLL.DTOs;
using librarian_workplace_BLL.Interfaces;
using librarian_workplace_BLL.Services;
using librarian_workplace_DAL.Entities;
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

        [HttpPost]
        public async Task<IActionResult> AddAsync(ReaderInputDTO readerDTO)
        {
            try
            {
                await _readerService.AddAsync(readerDTO);
                return Ok();
            }
            catch (ServiceErrorException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("books")]
        public async Task<IActionResult> GiveBookAsync(ReaderGiveDTO readerBooks)
        {
            try
            {
                await _readerService.GiveBookAsync(readerBooks);
                return Ok();
            }
            catch (ServiceErrorException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                return Ok(await _readerService.GetAsync(id));
            }
            catch (ServiceErrorException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{fio}")]
        public async Task<ActionResult> GetByFIOAsync(string fio)
        {
            try
            {
                return Ok(await _readerService.GetAsync(fio));
            }
            catch (ServiceErrorException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
