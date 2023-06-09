using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Page_Navigation_App.Helper;

namespace Page_Navigation_App.Popups;

public partial class Delete_Pdf : Window
{
    private string Datasource = "";
    public Delete_Pdf(string source)
    {
        Datasource = source;
        InitializeComponent();
        PdfName.Text = Datasource;
    }

    private void Delete_Pdf_Btn(object sender, ExecutedRoutedEventArgs e)
    {
        File.Delete(Datasource);

            this.Close();
    }

    private void Close_Window(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }
}