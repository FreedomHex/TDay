using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace TDay
{
    public static class PdfPrinter
    {
        public static void PrintClientInfo(int ProfileId)
        {
            Client client = new Client(ProfileId);
            if (File.Exists(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf")) { File.Delete(System.Windows.Forms.Application.UserAppDataPath + @"\Report_" + ProfileId.ToString() + ".pdf"); }
            FileStream FS = new FileStream(System.Windows.Forms.Application.UserAppDataPath+@"\Report_"+ProfileId.ToString()+".pdf",FileMode.CreateNew);
            var Doc = new iTextSharp.text.Document();
            PdfWriter.GetInstance(Doc, FS);
            Doc.Open();
            PdfPTable table = new PdfPTable(3);
            PdfPCell cell = new PdfPCell(new Phrase("Profile",new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 16,iTextSharp.text.Font.NORMAL, new BaseColor(Color.WhiteSmoke))));
            cell.BackgroundColor = new BaseColor(Color.Wheat);
            cell.Padding = 5;
            cell.Colspan = 3;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell);
            table.AddCell("Col 1 Row 1");
            table.AddCell("Col 2 Row 1");
            table.AddCell("Col 3 Row 1");
            table.AddCell("Col 1 Row 2");
            table.AddCell("Col 2 Row 2");
            table.AddCell("Col 3 Row 2");
            Doc.Add(table);
            Doc.Close();
        }

    }
}
