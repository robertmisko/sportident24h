namespace NewSpecialEvent.Printing
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// The PrintEngine.
    /// </summary>
    /// <seealso cref="System.Drawing.Printing.PrintDocument" />
    public partial class PrintEngine : PrintDocument
    {
        private static Queue<IPrintable> printObjects = new Queue<IPrintable>();

        private ArrayList printElements;

        private int printIndex = 0;

        private int pageNum = 0;

        public Font PrintFont
        {
            get { return new Font("Arial", 9); }
            protected set { }
        }

        public Font BoldFont
        {
            get { return new Font("Arial", 11, FontStyle.Bold); }
            protected set { }
        }

        public Brush PaintBrush
        {
            get { return Brushes.Black; }
            protected set { }
        }

        public PrintElement Header { get; set; }

        public PrintElement Footer { get; set; }

        public void AddPrintObject(IPrintable printObject)
        {
            printObjects.Enqueue(printObject);
            if (printObjects.Count >= 3)
            {
                this.Print();
            }
        }

        public void ShowPreview()
        {
            // now, show the print dialog...
            PrintPreviewDialog dialog = new PrintPreviewDialog();
            dialog.Document = this;

            // show the dialog...
            dialog.ShowDialog();
            dialog.BringToFront();
        }

        public string ReplaceTokens(string buf)
        {
            if (buf == null)
            {
                throw new ArgumentNullException("buf");
            }

            // replace...
            buf = buf.Replace("[pagenum]", this.pageNum.ToString(CultureInfo.InvariantCulture));

            // return...
            return buf;
        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            // reset...
            this.printElements = new ArrayList();
            this.pageNum = 0;
            this.printIndex = 0;

            // go through the objects in the list and create print elements for each one...
            if (PrintEngine.printObjects.Count > 0)
            {
                do
                {
                    // create an element...
                    IPrintable printObject = PrintEngine.printObjects.Dequeue();
                    PrintElement element = new PrintElement(printObject);
                    this.printElements.Add(element);

                    // tell it to print...
                    printObject.Print(element);
                }
                while (PrintEngine.printObjects.Count > 0);
            }
        }

        // OnPrintPage - called when printing needs to be done...
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }

            // adjust the page number...
            this.pageNum++;

            Rectangle pageBounds = new Rectangle(
                                        e.MarginBounds.Left,
                                        (int)e.MarginBounds.Top, 
                                                e.MarginBounds.Width,
                                        (int)e.MarginBounds.Height);
            float yPos = pageBounds.Top;

            bool morePages = false;
            int elementsOnPage = 0;
            while (this.printIndex < this.printElements.Count)
            {
                // get the element...
                PrintElement element = (PrintElement)this.printElements[this.printIndex];

                // how tall is the primitive?
                float height = element.CalculateHeight(this, e.Graphics);

                // will it fit on the page?
                if (yPos + height > pageBounds.Bottom)
                {
                    // we don't want to do this if we're the first thing on the page...
                    if (elementsOnPage != 0)
                    {
                        morePages = true;
                        break;
                    }
                }

                element.Draw(this, yPos, e.Graphics, pageBounds);

                // move the ypos...
                yPos += height;

                // next...
                this.printIndex++;
                elementsOnPage++;
            }

            e.HasMorePages = morePages;
        }
    }
}