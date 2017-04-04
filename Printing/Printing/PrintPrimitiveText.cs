namespace NewSpecialEvent.Printing
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PrintPrimitiveText : IPrintPrimitive
    {
        private string text;

        public PrintPrimitiveText(string text)
        {
            this.text = text;
        }

        public float CalculateHeight(PrintEngine printEngine, Graphics graphics)
        {
            if (printEngine == null)
            {
                throw new ArgumentNullException("printEngine");
            }
            else if (graphics == null)
            {
                throw new ArgumentNullException("graphics");
            }

            return printEngine.BoldFont.GetHeight(graphics);
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

            // draw it...
            graphics.DrawString(
                    this.text, 
                    printEngine.BoldFont,
                    printEngine.PaintBrush, 
                    elementBounds.Left, 
                    yPos, 
                    new StringFormat());
        }
    }
}
