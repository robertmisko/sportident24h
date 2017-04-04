namespace NewSpecialEvent.Printing
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PrintElement
    {
        private ArrayList printPrimitives = new ArrayList();
        private IPrintable printObject;

        public PrintElement(IPrintable printObject)
        {
            this.printObject = printObject;
        }

        public void AddText(string buf)
        {
            this.AddPrimitive(new PrintPrimitiveText(buf));
        }

        public void AddPrimitive(IPrintPrimitive primitive)
        {
            this.printPrimitives.Add(primitive);
        }

        public void AddData(string dataName, string dataValue)
        {
            // add this data to the collection...
            this.AddText(dataName + ": " + dataValue);
        }

        // AddHorizontalRule - add a rule to the element...
        public void AddHorizontalRule()
        {
            // add a rule object...
            this.AddPrimitive(new PrintPrimitiveRule());
        }

        // AddBlankLine - add a blank line...
        public void AddBlankLine()
        {
            // add a blank line...
            this.AddText(string.Empty);
        }

        // AddHeader - add a header...
        public void AddHeader(string buf)
        {
            this.AddText(buf);
            this.AddHorizontalRule();
        }

        public void AddControls(string splitTimes, string punchedControls)
        {
            this.AddPrimitive(new PrintPrimitiveColumn(splitTimes, punchedControls));
        }

        public float CalculateHeight(PrintEngine engine, Graphics graphics)
        {
            // loop through the print height...
            float height = 0;
            foreach (IPrintPrimitive primitive in this.printPrimitives)
            {
                // get the height...
                height += primitive.CalculateHeight(engine, graphics);
            }

            // return the height...
            return height;
        }

        public void Draw(PrintEngine engine, float yPos, Graphics graphics, Rectangle pageBounds)
        {
            // where...
            float height = this.CalculateHeight(engine, graphics);
            Rectangle elementBounds = new Rectangle(pageBounds.Left, (int)yPos, pageBounds.Right - pageBounds.Left, (int)height);

            // now, tell the primitives to print themselves...
            foreach (IPrintPrimitive primitive in this.printPrimitives)
            {
                // render it...
                primitive.Draw(engine, yPos, graphics, elementBounds);

                // move to the next line...
                yPos += primitive.CalculateHeight(engine, graphics);
            }
        }
    }
}
