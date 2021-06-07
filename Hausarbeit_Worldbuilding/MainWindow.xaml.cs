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

    public enum ConnectionTyp
    {
        Character_Character = 0,
        Character_Group = 1,
        Character_Location = 2,
        Character_Event = 3,

        Group_Character = 4,
        Group_Group = 5,
        Group_Location = 6,   
        Group_Event = 7,

        Location_Character = 8,
        Location_Group = 9,
        Location_Location = 10,
        Location_Event = 11,

        Event_Character = 12,
        Event_Group = 13,
        Event_Location = 14,
        Event_Event = 15,
    }

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Pages.CharacterPage CharacterPage;
        Pages.GroupPage GroupPage;
        Pages.LocationPage LocationPage;
        Pages.EventPage EventPage;

        string SearchFilter = "";
        WorldbuildingDBEntities Context;

        int? SelectedWorld = 0;

        public MainWindow()
        {
            Context = new WorldbuildingDBEntities();

            CharacterPage = new Pages.CharacterPage(Context, SelectedWorld);
            GroupPage = new Pages.GroupPage(Context, SelectedWorld);
            LocationPage = new Pages.LocationPage(Context, SelectedWorld);
            EventPage = new Pages.EventPage(Context, SelectedWorld);


            InitializeComponent();

            SelectedWorld = Properties.Settings.Default.SelectedWorld;

            UpdateComboBox(SelectedWorld);

        }

        public void UpdateComboBox(int? WorldID)
        {
            WorldComboBox.Items.Clear();
            int selectionIndex = 0;
            
            foreach (var item in Context.World)
            {
                var temp = new ComboBoxItem();
                temp.Content = item.Name;
                temp.Tag = item.WorldID;

                int index = WorldComboBox.Items.Add(temp);

                if (item.WorldID == WorldID)
                    selectionIndex = index;
            }
            var tempNW = new ComboBoxItem();
            tempNW.Content = "Create New World ...";
            tempNW.Tag = 0;

            WorldComboBox.Items.Add(tempNW);

            if(WorldID != 0)
            {
                WorldComboBox.SelectedIndex = selectionIndex;
            }
        }

        private void ResetButtonColors()
        {
            CharacterButton.Background = Brushes.DarkGray;
            LocationButton.Background = Brushes.DarkGray;
            GroupButton.Background = Brushes.DarkGray;
            EventButton.Background = Brushes.DarkGray;
        }

        private void CharacterButton_Click(object sender, RoutedEventArgs e)
        {
            CharacterPage.SelectedWorldChanged(SelectedWorld);
            PageContent.Content = CharacterPage;
            ResetButtonColors();
            CharacterButton.Background = Brushes.Gray;
        }

        private void LocationButton_Click(object sender, RoutedEventArgs e)
        {
            LocationPage.SelectedWorldChanged(SelectedWorld);
            PageContent.Content = LocationPage;
            ResetButtonColors();
            LocationButton.Background = Brushes.Gray;
        }

        private void EventButton_Click(object sender, RoutedEventArgs e)
        {
            EventPage.SelectedWorldChanged(SelectedWorld);
            PageContent.Content = EventPage;
            ResetButtonColors();
            EventButton.Background = Brushes.Gray;
        }
        private void GroupButton_Click(object sender, RoutedEventArgs e)
        {
            GroupPage.SelectedWorldChanged(SelectedWorld);
            PageContent.Content = GroupPage;
            ResetButtonColors();
            GroupButton.Background = Brushes.Gray;
        }

        private void WorldComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ComboBoxItem)WorldComboBox.SelectedItem;

            if (item == null)
                return;

            SelectedWorld = (int)item.Tag;
            if(SelectedWorld == null)
                Properties.Settings.Default.SelectedWorld = 0;
            else
                Properties.Settings.Default.SelectedWorld = (int)SelectedWorld;
            Properties.Settings.Default.Save();

            if (SelectedWorld == 0)
            {
                var window = new Windows.WorldWindow(this, Context);
                window.Visibility = Visibility.Visible;
            }
            else
            {
                var currentPage = (Pages.IPage)PageContent.Content;
                if (currentPage != null)
                {
                    currentPage.SelectedWorldChanged(SelectedWorld);
                }
                MenuSearch();

            }
        }

 

        private void MenuSearch()
        {
            SearchResult.Items.Clear();

            if (SelectedWorld != null && SearchFilter != "")
            {
                foreach (var item in Context.Character.Where(x => x.WorldID == SelectedWorld && x.Name.Contains(SearchFilter)))
                {
                    var temp = new ListBoxItem();
                    temp.Content = $"Character: {item.Name}";
                    temp.Tag = item.CharacterID;
                    temp.MouseDoubleClick += new MouseButtonEventHandler(CharacterItem_DoubleClick);
                    SearchResult.Items.Add(temp);
                }

                foreach (var item in Context.Gruppe.Where(x => x.WorldID == SelectedWorld && x.Name.Contains(SearchFilter)))
                {
                    var temp = new ListBoxItem();
                    temp.Content = $"Group: {item.Name}";
                    temp.Tag = item.GroupID;
                    temp.MouseDoubleClick += new MouseButtonEventHandler(GroupItem_DoubleClick);
                    SearchResult.Items.Add(temp);
                }

                foreach (var item in Context.Location.Where(x => x.WorldID == SelectedWorld && x.Name.Contains(SearchFilter)))
                {
                    var temp = new ListBoxItem();
                    temp.Content = $"Location: {item.Name}";
                    temp.Tag = item.LocationID;
                    temp.MouseDoubleClick += new MouseButtonEventHandler(LocationItem_DoubleClick);
                    SearchResult.Items.Add(temp);
                }

                foreach (var item in Context.Event.Where(x => x.WorldID == SelectedWorld && x.Description.Contains(SearchFilter)))
                {
                    var temp = new ListBoxItem();
                    temp.Content = $"Event: {item.Description}";
                    temp.Tag = item.EventID;
                    temp.MouseDoubleClick += new MouseButtonEventHandler(EventItem_DoubleClick);
                    SearchResult.Items.Add(temp);
                }
            }
        }

        private void CharacterItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var s = (ListBoxItem)sender;

            var window = new Windows.CharacterWindow(null, Context, SelectedWorld, (int)s.Tag);
            window.Visibility = Visibility.Visible;
        }

        private void GroupItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var s = (ListBoxItem)sender;

            var window = new Windows.GroupWindow(null, Context, SelectedWorld, (int)s.Tag);
            window.Visibility = Visibility.Visible;
        }

        private void LocationItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var s = (ListBoxItem)sender;

            var window = new Windows.LocationWindow(null, Context, SelectedWorld, (int)s.Tag);
            window.Visibility = Visibility.Visible;
        }

        private void EventItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var s = (ListBoxItem)sender;

            var window = new Windows.EventWindow(null, Context, SelectedWorld, (int)s.Tag);
            window.Visibility = Visibility.Visible;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchFilter = SearchTextBox.Text;

            MenuSearch();
        }
    }
}
