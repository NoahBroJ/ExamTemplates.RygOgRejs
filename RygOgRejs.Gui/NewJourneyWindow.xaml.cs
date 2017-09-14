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
using System.Windows.Shapes;
using RygOgRejs.DataAccess;
using RygOgRejs.Entities;
using RygOgRejs.Services;

namespace RygOgRejs.Gui
{
    /// <summary>
    /// Interaction logic for NewJourneyWindow.xaml
    /// </summary>
    public partial class NewJourneyWindow : Window
    {
        public DatabaseHandler dbHandler = new DatabaseHandler("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = RygOgRejs.DB; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        public NewJourneyWindow()
        {
            InitializeComponent();
        }

        private void btnAddJourney_Click(object sender, RoutedEventArgs e)
        {
            Destination destination = (Destination)cmbDestination.SelectedIndex;
            bool isFirstClass = (bool)cbxFirstClass.IsChecked;
            DateTime departureDate = (DateTime)dtpDepartureDate.SelectedDate;
            int.TryParse(tbxAdults.Text, out int adults);
            int.TryParse(tbxChildren.Text, out int children);
            double.TryParse(tbxLuggage.Text, out double luggage);

            string firstName = tbxFirstName.Text;
            string lastName = tbxLastName.Text;
            string ssn = tbxSsn.Text;

            try
            {
                Journey journey = new Journey(destination, departureDate, isFirstClass, adults, children, luggage);
                Payer payer = new Payer(firstName, lastName, ssn);
                Transaction transaction = new Transaction(journey.GetCurrentTotal(), journey, payer);
                
                dbHandler.CascadeInsert(journey, payer, transaction);
                Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async void cmbDestination_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string destination = Convert.ToString((Destination)cmbDestination.SelectedIndex);
            Temperature temp = await WeatherApiHandler.GetTemperature(destination);
            lblTemp.Content = temp.Temp;
        }
    }
}
