using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPIQuiz.Models
{
    public class AddBookModel
    {

        public string Author { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public DateTime DatePublished { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
