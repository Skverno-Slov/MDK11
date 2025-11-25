using LabWork24.Contexts;
using LabWork24.DTOs;
using LabWork24.Models;
using LabWork24.Services;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LabWork24
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CinemaService _service;
        CinemaDbContext _context;

        public MainWindow()
        {
            InitializeComponent();

            OpenConnection();

            SetDataSource();
        }

        private void SetDataSource()
        {
            TicketDataGrid.ItemsSource = _service.GetTickets();
        }

        private void OpenConnection()
        {
            var context = new CinemaDbContext();
            _context = context;
            var service = new CinemaService(context);
            _service = service;
        }

        private void SaveTxt()
        {
            StreamWriter writer = new("ticket.txt");

            writer.WriteLine(TicketIdTextBlock.Text);
            writer.WriteLine(NameTextBlock.Text);
            writer.WriteLine(StartDateTextBlock.Text);
            writer.WriteLine(CinemaTextBlock.Text);
            writer.WriteLine(HallNumberTextBlock.Text);
            writer.WriteLine($"{RowTextBlock.Text} {SeatTextBlock.Text}");

            writer.Close();

            MessageBox.Show("Билет сохранён");
        }

        private void ShowTicket(TicketDto ticket)
        {
            TicketIdTextBlock.Text = $"Билет № {ticket.TicketId}";
            NameTextBlock.Text = ticket.Name;
            StartDateTextBlock.Text = $"Начало сеанса: {ticket.StartDate.ToString("hh:mm dd MMMM")}";
            CinemaTextBlock.Text = $"Кинотеатр: {ticket.Cinema}";
            HallNumberTextBlock.Text = $"Зал: {ticket.HallNumber}";
            RowTextBlock.Text = $"Ряд: {ticket.Row} ";
            SeatTextBlock.Text = $"Место: {ticket.Seat}";
        }

        private TicketDto GetTicket()
        {
            var selectedTicket = TicketDataGrid.SelectedItem as Ticket;
            return _service.GetTicketById(selectedTicket.TicketId);
        }

        private void TicketsDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var ticket = GetTicket();

            if (ticket is null)
            {
                MessageBox.Show("Билет не найден");
                return;
            }

            ShowTicket(ticket);
        }

        private void SaveTxtButton_Click(object sender, RoutedEventArgs e)
        {
            SaveTxt();
        }
    }
}