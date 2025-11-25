using LabWork24.Contexts;
using LabWork24.DTOs;
using LabWork24.Managers;
using LabWork24.Models;
using LabWork24.Services;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace LabWork24
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CinemaService _service;
        CinemaDbContext _context;
        TicketDto _ticket;

        public MainWindow()
        {
            InitializeComponent();

            OpenConnection();

            SetDataSource();
        }

        private void SetDataSource()
        {
            try
            {
                TicketDataGrid.ItemsSource = _service.GetTickets();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OpenConnection()
        {
            try
            {
                var context = new CinemaDbContext();
                _context = context;
                var service = new CinemaService(context);
                _service = service;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveTxt(string filePath)
        {
            StreamWriter writer = new(filePath);

            writer.WriteLine($"Билет № {_ticket.TicketId}");
            writer.WriteLine(_ticket.Name);
            writer.WriteLine($"Начало сеанса: {_ticket.StartDate.ToString("hh:mm dd MMMM")}");
            writer.WriteLine($"Кинотеатр: {_ticket.Cinema}");
            writer.WriteLine($"Зал: {_ticket.HallNumber}");
            writer.WriteLine($"Ряд: {_ticket.Row} Место: {_ticket.Seat}");

            writer.Close();
        }

        private void ShowTicket()
        {
            TicketIdTextBlock.Text = $"Билет № {_ticket.TicketId}";
            NameTextBlock.Text = _ticket.Name;
            StartDateTextBlock.Text = $"Начало сеанса: {_ticket.StartDate.ToString("hh:mm dd MMMM")}";
            CinemaTextBlock.Text = $"Кинотеатр: {_ticket.Cinema}";
            HallNumberTextBlock.Text = $"Зал: {_ticket.HallNumber}";
            RowTextBlock.Text = $"Ряд: {_ticket.Row} ";
            SeatTextBlock.Text = $"Место: {_ticket.Seat}";
        }

        private TicketDto GetTicket()
        {
            try
            {
                var selectedTicket = TicketDataGrid.SelectedItem as Ticket;
                return _service.GetTicketById(selectedTicket.TicketId);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private void TicketsDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                var ticket = GetTicket();

                if (ticket is null)
                {
                    MessageBox.Show("Билет не найден");
                    return;
                }

                _ticket = ticket;
                ShowTicket();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveTxtButton_Click(object sender, RoutedEventArgs e)
        {
            if (_ticket is null)
            {
                MessageBox.Show("Билет не выбран");
                return;
            }
                
            var dialog = new SaveFileDialog();

            dialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            if (dialog.ShowDialog() is true)
            {
                string filePath = dialog.FileName;

                try
                {
                    SaveTxt(filePath);

                    MessageBox.Show($"Файл успешно сохранён");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void SavePdfButton_Click(object sender, RoutedEventArgs e)
        {
            if (_ticket is null)
            {
                MessageBox.Show("Билет не выбран");
                return;
            }

            string templateName = Path.Combine($"{Environment.CurrentDirectory}\\Templates\\TicketTemplate.docx");

            var manager = new TicketManager(templateName);

            var dialog = new SaveFileDialog();
            dialog.Filter = "PDF files (*.pdf)|*.pdf";

            if (dialog.ShowDialog() is true)
            {
                string filePath = dialog.FileName;

                try
                {
                    manager.SaveTicketPdf(_ticket, filePath);

                    MessageBox.Show("Файл успешно сохранён");
                }
                catch(Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _context.Dispose();
        }
    }
}