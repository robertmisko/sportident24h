namespace NewSpecialEvent.Printing
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPrintPrimitive
    {
        float CalculateHeight(PrintEngine printEngine, Graphics graphics);

        void Draw(PrintEngine printEngine, float yPos, Graphics graphics, Rectangle elementBounds);
    }
}
