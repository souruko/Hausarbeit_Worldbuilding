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

namespace Hausarbeit_Worldbuilding
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Pages.CharacterPage CharacterPage;
        Pages.LocationPage LocationPage;
        Pages.EventPage EventPage;

        public MainWindow()
        {
            CharacterPage = new Pages.CharacterPage();
            LocationPage = new Pages.LocationPage();
            EventPage = new Pages.EventPage();

            InitializeComponent();

        }

        private void CharacterButton_Click(object sender, RoutedEventArgs e)
        {
            PageContent.Content = CharacterPage;
        }

        private void LocationButton_Click(object sender, RoutedEventArgs e)
        {
            PageContent.Content = LocationPage;
        }

        private void EventButton_Click(object sender, RoutedEventArgs e)
        {
            PageContent.Content = EventPage;
        }
    }
}
