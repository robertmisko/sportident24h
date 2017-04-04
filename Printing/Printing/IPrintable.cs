namespace NewSpecialEvent.Printing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPrintable
    {
        void Print(PrintElement element);
    }
}
