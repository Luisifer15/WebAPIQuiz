using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIQuiz.Database.DBModels
{
    public class FavesModel
    {
        public int Id { get; set; }
        [ForeignKey("UserID")]
        public IdentityUser IdentityUser { get; set; }
        [ForeignKey("bookId")]
        public BooksModel BooksModel { get; set; }
    }
}
