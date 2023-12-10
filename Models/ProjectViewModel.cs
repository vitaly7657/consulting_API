namespace m21_e2_API.Models
{
    public class ProjectViewModel
    {
        public int? Id { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public IFormFile? PictureFile { get; set; }
    }
}
