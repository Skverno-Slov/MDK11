using LabWork25.DTOs;
using Microsoft.Office.Interop.Excel;
namespace LabWork25.Managers
{
    public class SessionManager
    {
        public void SaveSessionsCsv(SessionDto sessions, string fileName)
        {
            var excelApp = new Application
            {
                Visible = false
            };

            var workbook = excelApp.Workbooks.Add();
            var worksheet = workbook.Worksheets.Add();

            int collumnNumber = 1;
            int range = 4;
            foreach (var session in sessions)
            {

            }

            excelApp.Quit();
        }
    }
}
