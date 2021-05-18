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
    /// Interaktionslogik für EventWindow.xaml
    /// </summary>
    public partial class EventWindow : Window
    {
        Pages.IPage parent;
        WorldbuildingDBEntities Context;
        int? SelectedWorld;
        int? EventID;
        Event c;

        public EventWindow(Pages.IPage parent, WorldbuildingDBEntities Context, int? SelectedWorld, int? EventID)
        {
            InitializeComponent();

            this.parent = parent;
            this.Context = Context;
            this.SelectedWorld = SelectedWorld;
            this.EventID = EventID;

            if (EventID == null)
            {
                c = new Event();
                c.WorldID = (int)SelectedWorld;
            }
            else
            {
                this.c = Context.Event.First(x => x.WorldID == SelectedWorld && x.EventID == EventID);
            }

            DescriptionTextBox.Text = c.Description;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            c.Description = DescriptionTextBox.Text;

            if (EventID == null)
            {
                Context.Event.Add(c);
            }

            Context.SaveChanges();
            parent.UpdatePage();
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Context.Event.Remove(c);

            Context.SaveChanges();
            parent.UpdatePage();
            this.Close();
        }
    }
}
