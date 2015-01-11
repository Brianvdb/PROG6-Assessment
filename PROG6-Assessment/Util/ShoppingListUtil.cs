using iTextSharp.text;
using iTextSharp.text.pdf;
using PROG6_Assessment.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PROG6_Assessment.Util
{
    public class ShoppingListUtil
    {

        public static void SaveAsPDF(ObservableCollection<ProductVM> products, double totalPrice)
        {
            try
            {
                Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                dlg.FileName = "Bonnetje"; // Default file name
                dlg.DefaultExt = ".pdf"; // Default file extension
                dlg.Filter = "PDF File (.pdf)|*.pdf"; // Filter files by extension

                // Show save file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                string filename = "";

                // Process save file dialog box results
                if (result == true)
                {
                    // Save document
                    filename = dlg.FileName;
                }
                else
                {
                    return;
                }

                System.IO.FileStream fs = new FileStream(filename, FileMode.Create);
                Document document = new Document(new Rectangle(200f, 500f), 10, 10, 10, 10);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                document.Open();

                Font font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                Paragraph paragraph = new Paragraph("Appie Bonnetje", font);
                paragraph.Alignment = Element.ALIGN_CENTER;
                document.Add(paragraph);

                font = FontFactory.GetFont(FontFactory.HELVETICA, 9);
                paragraph = new Paragraph("Onderwijsboulevard 256", font);
                paragraph.Alignment = Element.ALIGN_CENTER;
                document.Add(paragraph);

                paragraph = new Paragraph("5223 DJ 's-Hertogenbosch", font);
                paragraph.Alignment = Element.ALIGN_CENTER;
                document.Add(paragraph);

                document.Add(new Paragraph(" "));
                document.Add(new Paragraph("------------------------------------------------------------", font));

                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 100;
                table.HorizontalAlignment = Element.ALIGN_CENTER;

                foreach (ProductVM product in products)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(product.Amount + " x " + product.ProductName, font));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(product.Price.ToString(), font));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    table.AddCell(cell);
                }

                document.Add(table);

                document.Add(new Paragraph("------------------------------------------------------------", font));

                table = new PdfPTable(2);
                table.WidthPercentage = 100;
                table.HorizontalAlignment = Element.ALIGN_CENTER;
                font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);

                PdfPCell tableCell = new PdfPCell(new Phrase("Totaal", font));
                tableCell.Border = Rectangle.NO_BORDER;
                tableCell.HorizontalAlignment = Element.ALIGN_LEFT;
                table.AddCell(tableCell);

                tableCell = new PdfPCell(new Phrase("€" + totalPrice, font));
                tableCell.Border = Rectangle.NO_BORDER;
                tableCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                table.AddCell(tableCell);

                font = FontFactory.GetFont(FontFactory.HELVETICA, 7);

                document.Add(table);

                paragraph = new Paragraph(DateTime.Now.ToString() + " ", font);
                paragraph.Alignment = Element.ALIGN_RIGHT;
                document.Add(paragraph);

                document.Add(new Paragraph(" "));

                font = FontFactory.GetFont(FontFactory.HELVETICA, 9);

                paragraph = new Paragraph("Bedankt voor uw bestelling en graag tot ziens!", font);
                paragraph.Alignment = Element.ALIGN_CENTER;
                document.Add(paragraph);

                paragraph = new Paragraph("www.ah.nl", font);
                paragraph.Alignment = Element.ALIGN_CENTER;
                document.Add(paragraph);

                document.Close();
                writer.Close();
                fs.Close();

                string message = "Uw winkelwagentje is succesvol opgeslagen.";
                string caption = "Succesvol";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBox.Show(message, caption, buttons, icon);
            }
            catch (Exception e)
            {
                string message = "Er heeft zich een fout voorgedaan tijdens het opslaan: " + e.Message;
                string caption = "Fout";
                MessageBoxButton buttons = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(message, caption, buttons, icon);
            }
        }
    }
}
