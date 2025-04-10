namespace BookStoreAPI.Dtos
{
    public class CreateBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
