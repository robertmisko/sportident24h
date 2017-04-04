namespace NewSpecialEvent.Dao.ResultCtx
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Data.Entity;

    public class ResultDataseeder : CreateDatabaseIfNotExists<ResultContext>
    {
        protected override void Seed(ResultContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            var sqlFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "\\Sql", "*.sql").OrderBy(x => x);
            foreach (string file in sqlFiles)
            {
                context.Database.ExecuteSqlCommand(File.ReadAllText(file));
            }

            base.Seed(context);

            context.SaveChanges();
        }
    }
}
