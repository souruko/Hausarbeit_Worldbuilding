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

            FillConnectionListBox();
        }

        private void FillConnectionListBox()
        {

            if (LocationID != null)
            {

                foreach (var item in Context.Location_Location)
                {
                    if (item.LocationID1 == LocationID)
                    {
                        var temp = new ListBoxItem();
                        temp.Content = $"{item.Location1.Name} - {item.Description}";
                        temp.Tag = item.LocationID2;
                        ConnectionListBox.Items.Add(temp);
                    }

                    if (item.LocationID2 == LocationID)
                    {
                        var temp = new ListBoxItem();
                        temp.Content = $"{item.Location.Name} - {item.Description}";
                        temp.Tag = item.LocationID1;
                        ConnectionListBox.Items.Add(temp);
                    }
                }

                foreach (var item in Context.Location_Group)
                {
                    if (item.LocationID == LocationID)
                    {
                        var temp = new ListBoxItem();
                        temp.Content = $"{item.Gruppe.Name} - {item.Description}";
                        temp.Tag = item.LocationID;
                        ConnectionListBox.Items.Add(temp);
                    }
                }

                foreach (var item in Context.Character_Location)
                {

                    if (item.LocationID == LocationID)
                    {
                        var temp = new ListBoxItem();
                        temp.Content = $"{item.Location.Name} - {item.Description}";
                        temp.Tag = item.LocationID;
                        ConnectionListBox.Items.Add(temp);
                    }
                }

                foreach (var item in Context.Location_Event)
                {
                    if (item.LocationID == LocationID)
                    {
                        var temp = new ListBoxItem();
                        temp.Content = $"{item.Event.Description} - {item.Description}";
                        temp.Tag = item.LocationID;
                        ConnectionListBox.Items.Add(temp);
                    }
                }
            }
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

