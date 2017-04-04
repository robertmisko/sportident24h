namespace NewSpecialEvent.ResultMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Dao.ResultCtx;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "This is needed for Entity Framework Migrations.")]
    internal sealed class ResultConfiguration : DbMigrationsConfiguration<ResultContext>
    {
        public ResultConfiguration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.MigrationsDirectory = @"ResultMigrations";
        }

        protected override void Seed(ResultContext context)
        {
        }
    }
}
