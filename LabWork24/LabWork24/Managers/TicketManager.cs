using LabWork24.DTOs;
using Microsoft.Office.Interop.Word;

namespace LabWork24.Managers
{
    public class TicketManager(string templatePath)
    {
        readonly string _templatePath = templatePath;

        public void SaveTicketPdf(TicketDto ticket, string fileName)
        {
            var wordApp = new Application
            {
                Visible = false
            };

            object template = _templatePath;
            var document = wordApp.Documents.Add(template);

            ReplaceItems(document, ticket);

            document.SaveAs(fileName, WdSaveFormat.wdFormatPDF);

            wordApp.Quit();
        }

        private void ReplaceItems(Document document, TicketDto ticket)
        {
            Dictionary<string, string> replaseData = CreateReplaseData(ticket);

            try
            {
                foreach (var item in replaseData)
                    document.Content.Find
                        .Execute(FindText: item.Key,
                            ReplaceWith: item.Value,
                            Replace: WdReplace.wdReplaceAll);
            }
            catch
            {
                throw new Exception();
            }
        }

        private static Dictionary<string, string> CreateReplaseData(TicketDto ticket)
        {
            return new Dictionary<string, string>()
            {
                { "номер билета", ticket.TicketId.ToString()},
                { "название фильма", ticket.Name},
                { "чч:мм дд ММММ", ticket.StartDate.ToString("hh:mm dd MMMM")},
                { "название кинотеатра", ticket.Cinema},
                { "номер зала", ticket.HallNumber.ToString()},
                { "номер ряда", ticket.Row.ToString()},
                { "номер места",ticket.Seat.ToString()}
            };
        }
    }
}
