using System;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace Page_Navigation_App.Utilities;

public class PDF_Generator

{
    public  PDF_Generator()
    {



        PdfDocument document = PdfReader.Open("C:\\Users\\Lauritz Abel\\Desktop\\TestPDF\\TestVorlage.pdf",
            PdfDocumentOpenMode.Import);
        PdfDocument outputDocument = new PdfDocument();
        int count = Math.Max(document.PageCount, document.PageCount);
        for (int idx = 0; idx < count; idx++)
        {
            // Get page from 1st document
            PdfPage page1 = document.PageCount > idx ? document.Pages[idx] : new PdfPage();
            page1 = outputDocument.AddPage(page1);

            XGraphics gfx = XGraphics.FromPdfPage(page1);

            // Create a font
            XFont font = new XFont("Verdana", 10);
            XFont fontgroß = new XFont("Verdana", 20);
            // Position
            for (int i = 0; i < 13; i++)
            {
                gfx.DrawString("Position" + i.ToString(), font, XBrushes.Blue,
                    new XRect(page1.Width / 12, -490 + (i * 15), page1.Width, page1.Height), XStringFormats.BottomLeft);
            }

            // Menge
            for (int i = 0; i < 13; i++)
            {
                gfx.DrawString("Menge" + i.ToString(), font, XBrushes.Blue,
                    new XRect(page1.Width / 2.05, -490 + (i * 15), page1.Width, page1.Height),
                    XStringFormats.BottomLeft);
            }

            // Einzelpreis 
            for (int i = 0; i < 13; i++)
            {
                gfx.DrawString("Einzelpreis" + i.ToString(), font, XBrushes.Blue,
                    new XRect(page1.Width / 1.61, -490 + (i * 15), page1.Width, page1.Height),
                    XStringFormats.BottomLeft);
            }

            // Gesamtpreis Pos
            for (int i = 0; i < 13; i++)
            {
                gfx.DrawString("Positionpreis" + i.ToString(), font, XBrushes.Blue,
                    new XRect(page1.Width / 1.26, -490 + (i * 15), page1.Width, page1.Height),
                    XStringFormats.BottomLeft);
            }

            //Rechnungsbetrag
            gfx.DrawString("Gesamtpreis", font, XBrushes.Blue,
                new XRect(page1.Width / 1.26, -280, page1.Width, page1.Height), XStringFormats.BottomLeft);

            //Adresse
            gfx.DrawString("Name", font, XBrushes.Blue, new XRect(page1.Width / 11.9, -710, page1.Width, page1.Height),
                XStringFormats.BottomLeft);
            gfx.DrawString("Straße", font, XBrushes.Blue,
                new XRect(page1.Width / 11.9, -695, page1.Width, page1.Height), XStringFormats.BottomLeft);
            gfx.DrawString("PLZ+Ort", font, XBrushes.Blue,
                new XRect(page1.Width / 11.9, -680, page1.Width, page1.Height), XStringFormats.BottomLeft);

            // Rechnungsnummer 
            gfx.DrawString("2023xxx", fontgroß, XBrushes.Blue,
                new XRect(page1.Width / 3.3, -577, page1.Width, page1.Height), XStringFormats.BottomLeft);


            //Lieferdatum
            gfx.DrawString("Lieferdatum", font, XBrushes.Blue,
                new XRect(page1.Width / 2.06, -630, page1.Width, page1.Height), XStringFormats.BottomLeft);


            //Rechnungsdatum 
            gfx.DrawString("Rechnungsdatum", font, XBrushes.Blue,
                new XRect(page1.Width / 1.363, -630, page1.Width, page1.Height), XStringFormats.BottomLeft);
            


        }

        outputDocument.Save("C:\\Users\\Lauritz Abel\\Desktop\\TestPDF\\Output.pdf");
    }
}


