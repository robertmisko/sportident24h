namespace NewSpecialEvent.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using NewSpecialEvent.Dao.ResultCtx;
    using NewSpecialEvent.TextBoxes;

    public class InputCourse : IInputCourseName
    {
        /// <summary>
        /// The Result Context
        /// </summary>
        private ResultContext context;

        /// <summary>
        /// Initializes a new instance of the InputCourse class
        /// </summary>
        /// <param name="context">The ResultContext</param>
        public InputCourse(ResultContext context)
        {
            this.context = context;
        }

        public int CourseId { get; set; }

        public DialogResult ShowDialog()
        {
            this.context.Courses.Load();
            var inputCourseForm = new InputCourseName(this.context.Courses.ToList());
            var result = inputCourseForm.ShowDialog();
            this.CourseId = inputCourseForm.CourseId;
            return result;
        }
    }
}
