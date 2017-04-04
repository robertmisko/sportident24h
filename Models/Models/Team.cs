namespace NewSpecialEvent.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Team : IComparable, IComparable<Team>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public Team Self
        {
            get
            {
                return this;
            }
        }

        public int CompareTo(object obj)
        {
            var other = obj as Team;

            if (other == null) return 1;

            return CompareTo(other);
        }

        public int CompareTo(Team other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}
