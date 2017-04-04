namespace NewSpecialEvent.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Runner
    {
        public int Id { get; set; }

        public virtual Team Team { get; set; }

        public int Position { get; set; }

        public string Name { get; set; }

        public long SiCard { get; set; }

        public bool DropedOut { get; set; }
    }
}
