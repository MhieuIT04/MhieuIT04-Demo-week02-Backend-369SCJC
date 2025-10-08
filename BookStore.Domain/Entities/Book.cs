using System.ComponentModel.DataAnnotations;
namespace BookStore.Domain.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }

        // Foreign key to Author
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
