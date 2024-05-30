using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAPIQuiz.Database;
using WebAPIQuiz.Database.DBModels;
using WebAPIQuiz.Models;
using WebAPIQuiz.Utilities;

namespace WebAPIQuiz.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [APIKeyAuthorization]
    public class LibraryController : Controller
    {
        private readonly AppDBContext _appDBContext;
        public LibraryController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        [HttpGet]
        [Route("GetBookList")]
        public async Task<ActionResult> GetBookList()
        {
            var books = await _appDBContext.TBL_Books.ToListAsync();
            return Ok(books);
        }

        [HttpPost]
        [Route("AddBook")]
        public async Task<ActionResult> AddBook(AddBookModel model)
        {
            var newBook = new BooksModel
            {
                Author = model.Author,
                Title = model.Title,
                Publisher = model.Publisher,
                DatePublished = model.DatePublished,
                DateAdded = model.DateAdded
            };

            _appDBContext.TBL_Books.Add(newBook);
            await _appDBContext.SaveChangesAsync();

            return Ok(new ResponseModel { Status = "Success", Message = "Book Added" });
        }

        // POST: api/Library/AddBooks
        [HttpPost]
        [Route("AddBooks")]
        public async Task<ActionResult> AddBooks(IEnumerable<AddBookModel> books)
        {
            var newBooks = books.Select(book => new BooksModel
            {
                Author = book.Author,
                Title = book.Title,
                Publisher = book.Publisher,
                DatePublished = book.DatePublished,
                DateAdded = book.DateAdded
            }).ToList();

            _appDBContext.TBL_Books.AddRange(newBooks);
            await _appDBContext.SaveChangesAsync();

            return Ok(new ResponseModel { Status = "Success", Message = "Books Added" });
        }

        [HttpPost]
        [Route("AddToFave")]
        public async Task<ActionResult> AddToFave(AddFaveModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var newFave = new FavesModel
            {
                IdentityUser = await _appDBContext.Users.FindAsync(userId),
                BooksModel = await _appDBContext.TBL_Books.FindAsync(model.bookID)
            };

            if (newFave.IdentityUser == null || newFave.BooksModel == null)
            {
                return NotFound();
            }

            _appDBContext.TBL_Faves.Add(newFave);
            await _appDBContext.SaveChangesAsync();

            return Ok(new ResponseModel { Status = "Success", Message = "Book Added to Favorites" });
        }

        [HttpGet]
        [Route("GetFaveBooks")]
        public async Task<ActionResult> GetFaveBooks()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var faveBooks = await _appDBContext.TBL_Faves
                .Where(f => f.IdentityUser.Id == userId)
                .Select(f => f.BooksModel)
                .ToListAsync();

            return Ok(faveBooks);
        }

    }
}
