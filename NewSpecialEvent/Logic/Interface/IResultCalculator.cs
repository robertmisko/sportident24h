namespace NewSpecialEvent.Logic
{
    using NewSpecialEvent.Models;
    using SPORTident;
    public interface IResultCalculator
    {
        Result GetResult(SportidentCard card, Runner runner);
    }
}