namespace NewSpecialEvent.Printing
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PrintPrimitiveRule : IPrintPrimitive
    {
        public float CalculateHeight(PrintEngine printEngine, Graphics graphics)
        {
            return 5;
        }

        public void Draw(PrintEngine printEngine, float yPos, Graphics graphics, Rectangle elementBounds)
        {
            if (printEngine == null)
            {
                throw new ArgumentNullException("printEngine");
            }
            else if (graphics == null)
            {
                throw new ArgumentNullException("graphics");
            }
            else if (elementBounds == null)
            {
                throw new ArgumentNullException("elementBounds");
            }

            Pen pen = new Pen(printEngine.PaintBrush, 1);
            graphics.DrawLine(pen, elementBounds.Left, yPos + 2, elementBounds.Right, yPos + 2);
        }
    }
}
