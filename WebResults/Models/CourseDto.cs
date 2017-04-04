namespace WebResults.Models
{
    public class CourseDto
    {
        public CourseDto(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}