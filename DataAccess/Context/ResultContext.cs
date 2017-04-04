namespace NewSpecialEvent.Dao.ResultCtx
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NewSpecialEvent.Models;

    public class ResultContext : DbContext
    {
        public ResultContext()
            : base("name=ResultContext")
        {
            // the terrible hack
            var ensureDLLIsCopied =
                    System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            Database.SetInitializer<ResultContext>(null);
        }

        public virtual DbSet<Runner> Runners { get; set; }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Result> Results { get; set; }

        public virtual DbSet<ControlData> ControlDatas { get; set; }

        public virtual DbSet<Team> Teams { get; set; }
    }
}
