namespace NewSpecialEvent.Logic
{
    using System;
    using NewSpecialEvent.Models;

    public interface IResultPostProcessor
    {
        ResultValidation Validate(Result result);

        Result CheckDuplicates(Result result);
    }
}