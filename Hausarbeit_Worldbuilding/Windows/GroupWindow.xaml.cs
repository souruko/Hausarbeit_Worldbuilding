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
    }
}
