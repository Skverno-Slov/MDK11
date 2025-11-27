using LabWork25.DTOs;
using Microsoft.Office.Interop.Excel;
using System.IO;
namespace LabWork25.Managers
{
    public class SessionManager
    {
        readonly List<string> _header;

        public SessionManager(List<string> header)
        {
            _header = header;
        }

        public void SaveSessionsCsv(List<SessionDto> sessions, string fileName)
        {
            using StreamWriter writer = new(fileName);
            WriteTable(sessions, writer);
        }

        public void SaveSessionsXlsx(List<SessionDto> sessions, string fileName)
        {
            var excelApp = new Application
            {
                Visible = false
            };

            var workbook = excelApp.Workbooks.Add();
            List<IGrouping<string, SessionDto>> sessionsByCinema = GroupSessions(sessions);

            foreach (var sessionGroup in sessionsByCinema)
            {
                Worksheet worksheet = workbook.Worksheets.Add();
                worksheet.Name = sessionGroup.Key;
                WriteXlsxHeader(worksheet);

                var sessionsList = sessionGroup.ToList();

                for (int i = 0; i < sessionsList.Count; i++)
                {
                    var session = sessionsList[i];
                    worksheet.Cells[i + 2, 1] = session.Name;
                    worksheet.Cells[i + 2, 2] = session.StartDate;
                    worksheet.Cells[i + 2, 3] = session.HallNumber;
                    worksheet.Cells[i + 2, 4] = session.Price;
                }
            }
            workbook.SaveAs(fileName, XlFileFormat.xlOpenXMLWorkbook);

            workbook.Close();
            excelApp.Quit();
        }

        private void WriteXlsxHeader(Worksheet worksheet)
        {
            if (_header is not null)
            {
                for (int i = 0; i < _header.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = _header[i];
                }
            }
        }

        private static List<IGrouping<string, SessionDto>> GroupSessions(List<SessionDto> sessions)
        {
            return sessions
                .GroupBy(s => s.Cinema)
                .OrderBy(g => g.Key)
                .ToList();
        }

        private void WriteTable(List<SessionDto> sessions, StreamWriter writer)
        {
            writer.Write('\uFEFF');
            WriteCsvHeader(writer);

            foreach (SessionDto session in sessions)
                writer.WriteLine($"{session.Name};{session.HallNumber};{session.StartDate};{session.Price}");
        }

        private void WriteCsvHeader(StreamWriter writer)
        {
            if (_header is not null)
            {
                string separator = ";";
                writer.WriteLine(string.Join(separator, _header));
            }
        }
    }
}
