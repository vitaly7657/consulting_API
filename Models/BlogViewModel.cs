namespace m21_e2_API.Models
{
    public class BlogViewModel
    {
        public int? Id { get; set; }
        public string BlogTitle { get; set; }
        public string BlogDescription { get; set; }
        public IFormFile? PictureFile { get; set; }
    }
}
