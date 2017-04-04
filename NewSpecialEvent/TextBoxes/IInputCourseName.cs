namespace NewSpecialEvent.TextBoxes
{
    using System.Windows.Forms;

    public interface IInputCourseName
    {
        int CourseId { get; set; }

        DialogResult ShowDialog();
    }
}