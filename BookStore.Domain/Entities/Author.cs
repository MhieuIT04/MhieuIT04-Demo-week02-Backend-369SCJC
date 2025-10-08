using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Entities
{
    public class Author
    {
        [Key]
       

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public string Bio { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
