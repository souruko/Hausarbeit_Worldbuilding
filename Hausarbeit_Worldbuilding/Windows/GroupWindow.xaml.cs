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
    /// Interaktionslogik für GroupWindow.xaml
    /// </summary>
    public partial class GroupWindow : Window
    {
        Pages.IPage parent;
        WorldbuildingDBEntities Context;
        int? SelectedWorld;
        int? GroupID;
        Gruppe c;

        public GroupWindow(Pages.IPage parent, WorldbuildingDBEntities Context, int? SelectedWorld, int? GroupID)
        {
            InitializeComponent();

            this.parent = parent;
            this.Context = Context;
            this.SelectedWorld = SelectedWorld;
            this.GroupID = GroupID;

            if (GroupID == null)
            {
                c = new Gruppe();
                c.WorldID = (int)SelectedWorld;

                DeleteButton.IsEnabled = false;
            }
            else
            {
                this.c = Context.Gruppe.First(x => x.WorldID == SelectedWorld && x.GroupID == GroupID);
            }

            NameTextBox.Text = c.Name;
            DescriptionTextBox.Text = c.Description;

            FillConnectionListBox();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            c.Name = NameTextBox.Text;
            c.Description = DescriptionTextBox.Text;

            if (GroupID == null)
            {
                Context.Gruppe.Add(c);
            }

            Context.SaveChanges();
            parent.UpdatePage();
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Context.Gruppe.Remove(c);

            Context.SaveChanges();
            parent.UpdatePage();
            this.Close();
        }

        private void FillConnectionListBox()
        {

            if (GroupID != null)
            {

                foreach (var item in Context.Group_Group)
                {
                    if (item.GroupID1 == GroupID)
                    {
                        var temp = new ListBoxItem();
                        temp.Content = $"{item.Gruppe1.Name} - {item.Description}";
                        temp.Tag = item.GroupID1;
                        ConnectionListBox.Items.Add(temp);
                    }

                    if (item.GroupID2 == GroupID)
                    {
                        var temp = new ListBoxItem();
                        temp.Content = $"{item.Gruppe.Name} - {item.Description}";
                        temp.Tag = item.GroupID1;
                        ConnectionListBox.Items.Add(temp);
                    }
                }

                foreach (var item in Context.Character_Group)
                {
                    if (item.GroupID == GroupID)
                    {
                        var temp = new ListBoxItem();
                        temp.Content = $"{item.Gruppe.Name} - {item.Description}";
                        temp.Tag = item.GroupID;
                        ConnectionListBox.Items.Add(temp);
                    }
                }

                foreach (var item in Context.Location_Group)
                {

                    if (item.GroupID == GroupID)
                    {
                        var temp = new ListBoxItem();
                        temp.Content = $"{item.Location.Name} - {item.Description}";
                        temp.Tag = item.GroupID;
                        ConnectionListBox.Items.Add(temp);
                    }
                }

                foreach (var item in Context.Group_Event)
                {
                    if (item.GroupID == GroupID)
                    {
                        var temp = new ListBoxItem();
                        temp.Content = $"{item.Event.Description} - {item.Description}";
                        temp.Tag = item.GroupID;
                        ConnectionListBox.Items.Add(temp);
                    }
                }
            }
        }
    }
}
