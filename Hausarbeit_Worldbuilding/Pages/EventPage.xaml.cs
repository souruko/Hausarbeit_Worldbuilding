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

namespace Hausarbeit_Worldbuilding.Pages
{
    /// <summary>
    /// Interaktionslogik für EventPage.xaml
    /// </summary>
    public partial class EventPage : UserControl, IPage
    {
        int? SelectedWorld = null;
        string SearchFilter = "";
        WorldbuildingDBEntities Context;
        List<ListBoxItem> Items = new List<ListBoxItem>();

        public EventPage(WorldbuildingDBEntities Context, int? SelectedWorld)
        {
            this.Context = Context;

            InitializeComponent();

            FillListBox();
        }

        private void FillListBox()
        {
            EventListBox.Items.Clear();

            if (SelectedWorld == null)
                return;

            foreach (var item in Items)
            {
                if (item.Content.ToString().Contains(SearchFilter))
                {
                    EventListBox.Items.Add(item);
                }
            }
        }

        public void SelectedWorldChanged(int? SelectedWorld)
        {
            this.SelectedWorld = SelectedWorld;

            Items.Clear();

            if (SelectedWorld != null)
            {
                foreach (var item in Context.Event)
                {
                    if (item.WorldID == SelectedWorld)
                    {
                        var temp = new ListBoxItem();
                        temp.Content = item.Description;
                        temp.Tag = item.EventID;
                        temp.MouseDoubleClick += new MouseButtonEventHandler(ItemDoubleClick);

                        Items.Add(temp);
                    }
                }
            }

            FillListBox();
        }

        public void UpdatePage()
        {
            SelectedWorldChanged(SelectedWorld);
        }

        private void ItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var s = (ListBoxItem)sender;

            var window = new Windows.EventWindow(this, Context, SelectedWorld, (int)s.Tag);
            window.Visibility = Visibility.Visible;
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new Windows.EventWindow(this, Context, SelectedWorld, null);
            window.Visibility = Visibility.Visible;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchFilter = SearchTextBox.Text;

            FillListBox();

        }

        private void EventListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EventListBox.SelectedItem == null)
                ConnectionButton.IsEnabled = false;
            else
                ConnectionButton.IsEnabled = true;
        }

        private void CharacterConnection_Click(object sender, RoutedEventArgs e)
        {
            if (EventListBox.SelectedItem == null)
                return;

            var s = (ListBoxItem)EventListBox.SelectedItem;

            var window = new Windows.ConnectionWindow(ConnectionTyp.Event_Character, (int)s.Tag, null, Context, SelectedWorld);
            window.Visibility = Visibility.Visible;
        }

        private void GroupConnection_Click(object sender, RoutedEventArgs e)
        {
            if (EventListBox.SelectedItem == null)
                return;

            var s = (ListBoxItem)EventListBox.SelectedItem;

            var window = new Windows.ConnectionWindow(ConnectionTyp.Event_Group, (int)s.Tag, null, Context, SelectedWorld);
            window.Visibility = Visibility.Visible;
        }

        private void LocationConnection_Click(object sender, RoutedEventArgs e)
        {
            if (EventListBox.SelectedItem == null)
                return;

            var s = (ListBoxItem)EventListBox.SelectedItem;

            var window = new Windows.ConnectionWindow(ConnectionTyp.Event_Location, (int)s.Tag, null, Context, SelectedWorld);
            window.Visibility = Visibility.Visible;
        }

        private void EventConnection_Click(object sender, RoutedEventArgs e)
        {
            if (EventListBox.SelectedItem == null)
                return;

            var s = (ListBoxItem)EventListBox.SelectedItem;

            var window = new Windows.ConnectionWindow(ConnectionTyp.Event_Event, (int)s.Tag, null, Context, SelectedWorld);
            window.Visibility = Visibility.Visible;
        }
    }
}
