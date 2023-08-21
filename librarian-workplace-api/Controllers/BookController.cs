using librarian_workplace_BLL.DTOs;
using librarian_workplace_BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using librarian_workplace_BLL.Services;

namespace librarian_workplace_api.Controllers
{
    [Route("api/v1/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(BookInputDTO bookDTO)
        {
            try
            {
                await _bookService.AddAsync(bookDTO);
                return Ok();
            }
            catch(ServiceErrorException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{articleNumber}")]
        public async Task<ActionResult<BookInfoDTO>> GetAsync(Guid articleNumber)
        {
            try
            {
                return Ok(await _bookService.GetAsync(articleNumber));
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

        [HttpGet("all")]
        public async Task<ActionResult<BookInfoDTO>> GetAllAsync()
        {
            try
            {
                return Ok(await _bookService.GetAllAsync());
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

        [HttpPatch]
        public async Task<IActionResult> UpdateAsync(BookInputDTO bookDTO)
        {
            try
            {
                await _bookService.UpdateAsync(bookDTO);
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

        [HttpDelete]
        public async Task<IActionResult> RemoveAsync(Guid articleNumber)
        {
            try
            {
                await _bookService.RemoveAsync(articleNumber);
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

    }
}
