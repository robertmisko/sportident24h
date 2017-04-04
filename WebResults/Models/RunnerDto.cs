namespace WebResults.Models
{
    public class RunnerDto
    {
        public RunnerDto(int id, int teamId, int positionInTeam, string name)
        {
            this.Id = id;
            this.TeamId = teamId;
            this.PositionInTeam = positionInTeam;
            this.Name = name;
        }

        public int Id { get; set; }

        public int TeamId { get; set; } 

        public int PositionInTeam { get; set; }

        public string Name { get; set; }
    }
}