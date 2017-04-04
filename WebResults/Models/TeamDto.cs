namespace WebResults.Models
{
    public class TeamDto
    {
        public TeamDto(int id, string name, string category)
        {
            this.Id = id;
            this.Name = name;
            this.Category = category;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }
    }
}