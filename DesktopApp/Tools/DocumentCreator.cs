using Database.Records;
using Xceed.Words.NET;

namespace DesktopApp.Tools;

internal class DocumentCreator
{
    public static void CreateListOfRepots(List<ReportRecord> reports, string filePath)
    {
        using var doc = DocX.Create(filePath);

        doc.InsertParagraph("Reports").Bold().FontSize(16).Alignment = Xceed.Document.NET.Alignment.center;

        var table = doc.AddTable(reports.Count + 1, 3);

        table.Rows[0].Cells[0].Paragraphs.First().Append("UserId").Bold();
        table.Rows[0].Cells[1].Paragraphs.First().Append("Date").Bold();
        table.Rows[0].Cells[2].Paragraphs.First().Append("Type").Bold();
        table.Rows[0].Cells[3].Paragraphs.First().Append("Details").Bold();

        for (int i = 0; i < reports.Count; i++)
        {
            table.Rows[i + 1].Cells[0].Paragraphs.First().Append(reports[i].GeneratedBy.ToString());
            table.Rows[i + 1].Cells[1].Paragraphs.First().Append(reports[i].ReportDate.ToString());
            table.Rows[i + 1].Cells[2].Paragraphs.First().Append(reports[i].ReportType);
            table.Rows[i + 1].Cells[3].Paragraphs.First().Append(reports[i].Details);
        }

        doc.InsertTable(table);

        doc.Save();
    }
}