namespace NewSpecialEvent.Printing
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PrintPrimitiveColumn : IPrintPrimitive
    {
        private string splitTimes;
        private string punchedControls;
        private int numRows;

        public PrintPrimitiveColumn(string splitTimes, string punchedControls)
        {
            this.splitTimes = splitTimes;
            this.punchedControls = punchedControls;
        }

        public float CalculateHeight(PrintEngine printEngine, Graphics graphics)
        {
            return 247;
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

            string[] splits = this.splitTimes == null ? new string[0] : this.splitTimes.Split(';');
            string[] controls = this.punchedControls == null ? new string[0] : this.punchedControls.Split(';');
            int i = 1;
            this.numRows = 0;
            int xPos = elementBounds.Left;
            float origYPos = yPos;
            string text = string.Empty;

            foreach (var control in controls)
            {
                if (!string.IsNullOrEmpty(control))
                {
                    text = i.ToString(CultureInfo.InvariantCulture) + "(" + control + ")";
                    i++;
                    Rectangle rectangle = new Rectangle(xPos, (int)yPos, 80, printEngine.PrintFont.Height);
                    graphics.DrawString(text, printEngine.PrintFont, printEngine.PaintBrush, rectangle);
                    if (xPos < elementBounds.Right - 40)
                    {
                        xPos = xPos + rectangle.Width;
                    }
                    else
                    {
                        xPos = elementBounds.Left;
                        yPos = yPos + (printEngine.PrintFont.Height * 4);
                        this.numRows++;
                    }
                }
            }

            Rectangle frec = new Rectangle(xPos, (int)yPos, 80, printEngine.PrintFont.Height);
            graphics.DrawString("F", printEngine.PrintFont, printEngine.PaintBrush, frec);

            i = 0;
            TimeSpan total = new TimeSpan();
            xPos = elementBounds.Left;
            yPos = origYPos + printEngine.PrintFont.Height + 5;
            foreach (var split in splits)
            {
                if (string.IsNullOrEmpty(split))
                {
                    total += TimeSpan.FromSeconds(0);
                }
                else
                {
                    total += TimeSpan.Parse(split, CultureInfo.InvariantCulture);
                }

                var totalText = string.Format(CultureInfo.InvariantCulture, "{0:00}:{1:00}:{2:00}", total.Hours, total.Minutes, total.Seconds);
                Rectangle rectangle = new Rectangle(xPos, (int)yPos, 80, printEngine.PrintFont.Height);
                graphics.DrawString(totalText, printEngine.PrintFont, printEngine.PaintBrush, rectangle);
                if (xPos < elementBounds.Right - 40)
                {
                    xPos = xPos + rectangle.Width;
                }
                else
                {
                    xPos = elementBounds.Left;
                    yPos = yPos + (printEngine.PrintFont.Height * 4);
                }
            }

            xPos = elementBounds.Left;
            yPos = origYPos + (printEngine.PrintFont.Height * 2) + 5;
            foreach (var split in splits)
            {
                Rectangle rectangle = new Rectangle(xPos, (int)yPos, 80, printEngine.PrintFont.Height);
                graphics.DrawString(split, printEngine.PrintFont, printEngine.PaintBrush, rectangle);
                if (xPos < elementBounds.Right - 40)
                {
                    xPos = xPos + rectangle.Width;
                }
                else
                {
                    xPos = elementBounds.Left;
                    yPos = yPos + (printEngine.PrintFont.Height * 4);
                }
            }
        }
    }
}
