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

namespace RygOgRejs.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window
    {
        private UserControl currentUserControlCentre, currentUserControlRight;
        public MainWindow()
        {
            InitializeComponent();

            // TEST:
            TestEntity t1 = new TestEntity { Prop1 = 1, Prop2 = "data her" };
            TestEntity t2 = new TestEntity { Prop1 = 4, Prop2 = "data her og der og alle vegne" };
            List<TestEntity> testEntities = new List<TestEntity>() { t1, t2 };
            currentUserControlCentre = new DataViewJourneys(testEntities);
            userControlCentre.Content = currentUserControlCentre;
        }

        private void OnMenuFilesClose_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Vil du afslutte Ryg & Rejs?", "Luk Ryg & Rejs", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }

        private void ButtonJourneys_Click(object sender, RoutedEventArgs e)
        {
            // TEST:
            TestEntity t1 = new TestEntity { Prop1 = 1, Prop2 = "data her" };
            TestEntity t2 = new TestEntity { Prop1 = 4, Prop2 = "data her og der og alle vegne" };
            List<TestEntity> testEntities = new List<TestEntity>() { t1, t2 };
            currentUserControlCentre = new DataViewJourneys(testEntities);
            userControlCentre.Content = currentUserControlCentre;
        }

        private void MenuHelpAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dette er et eksempel på løsning af S2 eksamensopgaven Ryg & Rejs", "Om Ryg & Rejs", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}