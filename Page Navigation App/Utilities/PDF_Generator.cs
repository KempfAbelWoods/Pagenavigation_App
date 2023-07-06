using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Windows.Forms;
using Page_Navigation_App.Configs;
using Page_Navigation_App.DB;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace Page_Navigation_App.Utilities;

public class PDF_Generator

{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="PDFname">sets the name of the output PDF</param>
    /// <param name="dboutputPath"> Path where to save the PDF, if "" the Path set in the Db will be used</param>
    /// <param name="orderID"> ID of Order to print, if "" the example will be used</param>
    /// /// <param name="deliverydate"> Liederdatum</param>
    //Todo Adresskopf in DB Settings abspeichern (es müssen mehrere Adressen hinterlegbar sein)
    public byte[] PDF_Generate(string PDFname, string dboutputPath, string orderID, string deliverydate)
    {
        string Position = "Position";
        string Menge = "Menge";
        string Einzelpreis = "Einzelpreis";
        string Positionpreis = "Positionpreis";
        string Gesamtpreis = "Gesamtpreis";
        string Rechnungsnummer = "Rechnungsnummer"; //Todo irgendwie übergeben
        string Lieferdatum = deliverydate;
        string Rechnungsdatum = DateTime.Now.ToString();
        string[] Adresse = { "Name", "Straße", "PLZ+Ort" };
        int pos_Anzahl = 13;
        float Preis = 0;

        var (data, err) = Rw_Settings.ReadwithID("1", Paths.sqlite_path);
        if (err != null)
        {
            MessageBox.Show(err.GetException().Message);
        }
        else if (data.Count == 0)
        {
            MessageBox.Show("No Directory for Pdf Files set");
        }
        else if (data.Count == 1)
        {
            var (pdfmodel, err1) = Rw_Settings.ReadwithID("2", Paths.sqlite_path);
            if (err1 != null)
            {
                MessageBox.Show(err1.GetException().Message);
            }
            else if (pdfmodel.Count == 0)
            {
                MessageBox.Show("No Directory for Pdf Files set");
            }
            else if (pdfmodel.Count == 1)
            {
                PdfDocument document = PdfReader.Open(pdfmodel[0].Ressource,
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

                    var (tasklist, err2) = Rw_Tasks.ReadwithOrderID(orderID, Paths.sqlite_path);
                    if (err2 != null)
                    {
                        MessageBox.Show(err2.GetException().Message);
                    }

                    if (tasklist.Count > 0 && orderID != "")
                    {
                        pos_Anzahl = tasklist.Count;
                    }

                    List<Db_Ressources> ressource = new List<Db_Ressources>();
                    for (int i = 0; i < pos_Anzahl; i++)
                    {
                        if (orderID != "" && i < tasklist.Count)
                        {
                            (ressource, var err3) = Rw_Ressources.ReadwithID(tasklist[i].Ressource, Paths.sqlite_path);
                            if (err3 != null)
                            {
                                MessageBox.Show(err3.GetException().Message);
                            }

                            if (ressource.Count == 1)
                            {
                                Position = ressource[0].Name;
                                Menge = tasklist[i].ActualHours.ToString();
                                Einzelpreis = ressource[0].Costs.ToString();
                                Positionpreis = (tasklist[i].ActualHours * ressource[0].Costs).ToString();
                                Preis += (tasklist[i].ActualHours * ressource[0].Costs);
                            }
                        }


                        // Position
                        gfx.DrawString(Position, font, XBrushes.Blue,
                            new XRect(page1.Width / 12, -490 + (i * 15), page1.Width, page1.Height),
                            XStringFormats.BottomLeft);


                        // Menge

                        gfx.DrawString(Menge, font, XBrushes.Blue,
                            new XRect(page1.Width / 2.05, -490 + (i * 15), page1.Width, page1.Height),
                            XStringFormats.BottomLeft);


                        // Einzelpreis 

                        gfx.DrawString(Einzelpreis, font, XBrushes.Blue,
                            new XRect(page1.Width / 1.61, -490 + (i * 15), page1.Width, page1.Height),
                            XStringFormats.BottomLeft);


                        // Gesamtpreis Pos

                        gfx.DrawString(Positionpreis, font, XBrushes.Blue,
                            new XRect(page1.Width / 1.26, -490 + (i * 15), page1.Width, page1.Height),
                            XStringFormats.BottomLeft);
                    }

                    if (orderID != "")
                    {
                        Gesamtpreis = Preis.ToString();
                    }

                    //Rechnungsbetrag
                    gfx.DrawString(Gesamtpreis, font, XBrushes.Blue,
                        new XRect(page1.Width / 1.26, -280, page1.Width, page1.Height), XStringFormats.BottomLeft);

                    //Adresse
                    gfx.DrawString(Adresse[0], font, XBrushes.Blue,
                        new XRect(page1.Width / 11.9, -710, page1.Width, page1.Height),
                        XStringFormats.BottomLeft);
                    gfx.DrawString(Adresse[1], font, XBrushes.Blue,
                        new XRect(page1.Width / 11.9, -695, page1.Width, page1.Height), XStringFormats.BottomLeft);
                    gfx.DrawString(Adresse[2], font, XBrushes.Blue,
                        new XRect(page1.Width / 11.9, -680, page1.Width, page1.Height), XStringFormats.BottomLeft);

                    // Rechnungsnummer 
                    gfx.DrawString(Rechnungsnummer, fontgroß, XBrushes.Blue,
                        new XRect(page1.Width / 3.3, -577, page1.Width, page1.Height), XStringFormats.BottomLeft);


                    //Lieferdatum
                    gfx.DrawString(Lieferdatum, font, XBrushes.Blue,
                        new XRect(page1.Width / 2.06, -630, page1.Width, page1.Height), XStringFormats.BottomLeft);


                    //Rechnungsdatum 
                    gfx.DrawString(Rechnungsdatum, font, XBrushes.Blue,
                        new XRect(page1.Width / 1.363, -630, page1.Width, page1.Height), XStringFormats.BottomLeft);
                }

                try
                {
                    if (dboutputPath == "")
                    {
                        outputDocument.Save(data[0].Ressource + "\\" + PDFname + ".pdf");
                    }
                    else
                    {
                        outputDocument.Save(dboutputPath + "\\" + PDFname + ".pdf");
                    }
                    
                    using (var ms = new MemoryStream())
                    {
                        outputDocument.Save(ms);
                        return ms.ToArray();
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }
        else
        {
            MessageBox.Show("Somehow there exist 2 or more elements in the Database, pls call the support.");
        }

        return null;
    }
}