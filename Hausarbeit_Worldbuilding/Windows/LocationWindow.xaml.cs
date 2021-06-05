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

namespace Hausarbeit_Worldbuilding.Windows
{
    /// <summary>
    /// Interaktionslogik für LocationWindow.xaml
    /// </summary>
    public partial class LocationWindow : Window
    {
        Pages.IPage parent;
        WorldbuildingDBEntities Context;
        int? SelectedWorld;
        int? LocationID;
        Location c;

        public LocationWindow(Pages.IPage parent, WorldbuildingDBEntities Context, int? SelectedWorld, int? LocationID)
        {
            InitializeComponent();

            this.parent = parent;
            this.Context = Context;
            this.SelectedWorld = SelectedWorld;
            this.LocationID = LocationID;

            if (LocationID == null)
            {
                c = new Location();
                c.WorldID = (int)SelectedWorld;

                DeleteButton.IsEnabled = false;
            }
            else
            {
                this.c = Context.Location.First(x => x.WorldID == SelectedWorld && x.LocationID == LocationID);
            }

            NameTextBox.Text = c.Name;
            DescriptionTextBox.Text = c.Description;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            c.Name = NameTextBox.Text;
            c.Description = DescriptionTextBox.Text;

            if (LocationID == null)
            {
                Context.Location.Add(c);
            }

            Context.SaveChanges();
            parent.UpdatePage();
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Context.Location.Remove(c);

            Context.SaveChanges();
            parent.UpdatePage();
            this.Close();
        }
    }
}

