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

            UpdateComboBox(0);

        }

        public void UpdateComboBox(int WorldID)
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

        private void CharacterButton_Click(object sender, RoutedEventArgs e)
        {
            CharacterPage.SelectedWorldChanged(SelectedWorld);
            PageContent.Content = CharacterPage;
        }

        private void LocationButton_Click(object sender, RoutedEventArgs e)
        {
            LocationPage.SelectedWorldChanged(SelectedWorld);
            PageContent.Content = LocationPage;
        }

        private void EventButton_Click(object sender, RoutedEventArgs e)
        {
            EventPage.SelectedWorldChanged(SelectedWorld);
            PageContent.Content = EventPage;
        }
        private void GroupButton_Click(object sender, RoutedEventArgs e)
        {
            GroupPage.SelectedWorldChanged(SelectedWorld);
            PageContent.Content = GroupPage;
        }

        private void WorldComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (ComboBoxItem)WorldComboBox.SelectedItem;

            if (item == null)
                return;

            SelectedWorld = (int)item.Tag;

            if(SelectedWorld == 0)
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

                    SearchResult.Items.Add(temp);
                }

                foreach (var item in Context.Gruppe.Where(x => x.WorldID == SelectedWorld && x.Name.Contains(SearchFilter)))
                {
                    var temp = new ListBoxItem();
                    temp.Content = $"Group: {item.Name}";
                    temp.Tag = item.GroupID;

                    SearchResult.Items.Add(temp);
                }

                foreach (var item in Context.Location.Where(x => x.WorldID == SelectedWorld && x.Name.Contains(SearchFilter)))
                {
                    var temp = new ListBoxItem();
                    temp.Content = $"Location: {item.Name}";
                    temp.Tag = item.LocationID;

                    SearchResult.Items.Add(temp);
                }

                foreach (var item in Context.Event.Where(x => x.WorldID == SelectedWorld && x.Description.Contains(SearchFilter)))
                {
                    var temp = new ListBoxItem();
                    temp.Content = $"Event: {item.Description}";
                    temp.Tag = item.EventID;

                    SearchResult.Items.Add(temp);
                }
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchFilter = SearchTextBox.Text;

            MenuSearch();
        }
    }
}
