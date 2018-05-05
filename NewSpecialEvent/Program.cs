namespace NewSpecialEvent
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks.Dataflow;
    using System.Windows.Forms;
    using NewSpecialEvent.Dao;
    using NewSpecialEvent.Dao.Interface;
    using NewSpecialEvent.Dao.ResultCtx;
    using NewSpecialEvent.Logic;
    using NewSpecialEvent.TextBoxes;
    using SimpleInjector;
    using SimpleInjector.Diagnostics;
    using SimpleInjector.Extensions.ExecutionContextScoping;

    public static class Program
    {
        private static Container container;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Register();
            var form1 = container.GetInstance<Form1>();
            Application.Run(form1);

            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<ResultContext, ResultConfiguration>());
        }

        private static void Register()
        {
            // 1. Create a new Simple Injector container
            container = new Container();

            // 2. Configure the container (register)
            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            // Register context and DAO classes
            container.Register<ResultContext>(Lifestyle.Scoped);
            container.Register<IResultDataAccess, ResultDataAccess>(Lifestyle.Scoped);

             #if DEBUG
                container.Register<IDatabaseInitializer<ResultContext>, MockResultDataseeder>(Lifestyle.Singleton);
             #else
                container.Register<IDatabaseInitializer<ResultContext>, ResultDataseeder>(Lifestyle.Scoped);
             #endif

            // Register controldata, finishtime, etc providers
            container.Register<IControlDataProvider, ControlDataProvider>(Lifestyle.Singleton);
            container.Register<IFinishTimeProvider, FinishTimeProvider>(Lifestyle.Singleton);
            container.Register<IResultCalculator, ResultCalculator>(Lifestyle.Scoped);
            container.Register<ISplitTimeProvider, SplitTimeProvider>(Lifestyle.Singleton);
            container.Register<ICourseProvider, CourseProvider>(Lifestyle.Scoped);
            container.Register<ICourseParser, CourseParser>(Lifestyle.Scoped);
            container.Register<IInputCourseName, InputCourse>(Lifestyle.Scoped);
            container.Register<IResultPostProcessor, ResultPostProcessor>(Lifestyle.Scoped);
            container.Register<IRunnerService, RunnerService>(Lifestyle.Scoped);
            container.Register<IStartTimeProvider, StartTimeCalculator>(Lifestyle.Scoped);
            container.Register<INewCardHandlerService, NewCardHandlerService>(Lifestyle.Scoped);

            container.Register<ICardDataProcessor, CardDataProcessor>(Lifestyle.Scoped);

            #if DEBUG
                container.Register<IReader, MockReader>(Lifestyle.Transient);
            #else
                container.Register<IReader, SiReader>(Lifestyle.Transient);
            #endif

            container.Register<Form1>();

            Registration registration = container.GetRegistration(typeof(Form1)).Registration;

            registration.SuppressDiagnosticWarning(DiagnosticType.DisposableTransientComponent, "Form is automatically disposed.");

            // 3. Verify the container's configuration.
            container.Verify();
        }
    }
}
