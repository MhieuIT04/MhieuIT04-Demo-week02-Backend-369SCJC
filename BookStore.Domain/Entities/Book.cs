using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int PublicationYear { get; set; } // << THÊM DÒNG NÀY
        public string? Description { get; set; }  // << VÀ DÒNG NÀY

        // Khóa ngoại
        public int AuthorId { get; set; }

        // Navigation property
        public Author Author { get; set; } = null!;
    }
}