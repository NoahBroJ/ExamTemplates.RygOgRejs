using System;
using System.Collections.Generic;
using System.Linq;
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
using RygOgRejs.DataAccess;
using RygOgRejs.Entities;
using RygOgRejs.Services;
using System.Collections;
using System.Data;
namespace RygOgRejs.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window
    {
        private UserControl currentUserControlCentre, currentUserControlRight;
        public DatabaseHandler dbHandler = new DatabaseHandler("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = RygOgRejs.DB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public MainWindow()
        {
            InitializeComponent();
            
            decimal[] todayTotals = dbHandler.GetTodayTotals();
            labelTotalJourneys.Content = todayTotals[0];
            labelTotalFirstClassJourneys.Content = todayTotals[1];
            labelTotalStandardJourneys.Content = todayTotals[2];
            labelTotalPassengers.Content = todayTotals[3];
            labelTotalAdults.Content = todayTotals[4];
            labelTotalChildren.Content = todayTotals[5];
            labelTotalSales.Content = "kr. " + todayTotals[6];
        }

        private void OnMenuFilesClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Vil du afslutte Ryg & Rejs?", "Luk Ryg & Rejs", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        private void ButtonJourneys_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = dbHandler.GetAllFromTable("dbo.Journeys");
            buttonJourneys.IsEnabled = false;
            buttonTransactions.IsEnabled = true;
            stackJourneys.Visibility = Visibility.Visible;
        }

        private void buttonTransactions_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = dbHandler.GetAllFromTable("dbo.Transactions");
            buttonJourneys.IsEnabled = true;
            buttonTransactions.IsEnabled = false;
            stackJourneys.Visibility = Visibility.Hidden;
        }

        private void buttonNewJourney_Click(object sender, RoutedEventArgs e)
        {
            NewJourneyWindow newJourneyWindow = new NewJourneyWindow();
            newJourneyWindow.Show();
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = (DataRowView)dataGrid.SelectedItems[0];
            int id = (int)row.Row.ItemArray[0];
            
            dtpDeparture.SelectedDate = (DateTime)row.Row.ItemArray[2];
            tbxAdults.Text = (int)row.Row.ItemArray[3] + "";
            tbxChildren.Text = (int)row.Row.ItemArray[4] + "";
            cbxFirstClass.IsChecked = (bool)row.Row.ItemArray[5];
            tbxLuggage.Text = (decimal)row.Row.ItemArray[6] + "";
        }

        private void MenuHelpAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dette er et eksempel på løsning af S2 eksamensopgaven Ryg & Rejs", "Om Ryg & Rejs", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}