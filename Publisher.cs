using System.Collections.Generic;
using System.Text;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;

namespace Aggregator
{
    class Publisher
    {
        public static void Publish(string filepath, List<Record> records)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Quotes";

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            XFont font = new("Calibri", 9, XFontStyle.Regular, new(PdfFontEncoding.Unicode));

            PdfPage page = null;
            XGraphics graphics = null;
            XTextFormatter formatter = null;

            int y = 780;

            foreach (var record in records)
            {
                if (y + 10 >= 780)
                {
                    page = document.AddPage();
                    graphics = XGraphics.FromPdfPage(page);
                    formatter = new(graphics);
                    formatter.Alignment = XParagraphAlignment.Left;
                    y = 10;
                }

                formatter.DrawString(record.ToString(), font, XBrushes.Black, new XRect(10, y, page.Width - 10, 50), XStringFormats.TopLeft);
                y += 10;
            }

            document.Save(filepath);
        }
    }
}
