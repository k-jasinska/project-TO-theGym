using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystem.Db;
using MigraDoc.DocumentObjectModel;

namespace GymSystem.App.Models
{
    class ReceiptGenerator
    {
        public static async void SavePdf(Entrance en)
        {
            string path = await ChoosePath(en.Id.ToString());
            var doc = new Document();
            doc.Info.Title = en.Id.ToString();
            var section = doc.AddSection();
            section.PageSetup.PageWidth = Unit.FromMillimeter(42);
            section.PageSetup.PageHeight = Unit.FromMillimeter(100);
            var paragraph = section.AddParagraph();
            paragraph.AddFormattedText("TheGym\r\n", TextFormat.Bold);
            paragraph.AddText("ul. Urzędnicza 2\r\n");
            paragraph.AddFormattedText("PARAGON FISKALNY\r\n", TextFormat.Bold);
            paragraph.Format.Font.Name = "Calibri";
            paragraph.Format.Font.Size = 12;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            var paragraph1 = section.AddParagraph();
            paragraph1.AddText(DateTime.Now.ToString());
            paragraph.Format.Alignment = ParagraphAlignment.Left;
            var table = section.AddTable();
            table.Style = "Table";
            table.Borders.Color = Color.FromCmyk(0, 0, 0, 0);
            table.Borders.Width = 0.25;
            table.Borders.Left.Width = 0.5;
            table.Borders.Right.Width = 0.5;
            table.Borders.Bottom.Width = 3;
            table.Rows.LeftIndent = 0;
            var column = table.AddColumn("3cm");
            column.Format.Alignment = ParagraphAlignment.Right;
            column = table.AddColumn("1cm");
            column.Format.Alignment = ParagraphAlignment.Left;
            var pdfRenderer = new MigraDoc.Rendering.PdfDocumentRenderer(true);
            pdfRenderer.Document = doc;
            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save(path);
        }
        public static async Task<string> ChoosePath(string title)
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("Portable Document Format", new List<string>() { ".pdf" });
            savePicker.SuggestedFileName = title;
            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            return file.Path;
        }
    }
}
