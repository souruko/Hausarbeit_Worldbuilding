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
    /// Interaktionslogik für CharacterPage.xaml
    /// </summary>
    public partial class CharacterPage : UserControl, IPage
    {
        int? SelectedWorld = null;
        string SearchFilter = "";
        WorldbuildingDBEntities Context;
        List<ListBoxItem> Items = new List<ListBoxItem>();

        public CharacterPage(WorldbuildingDBEntities Context, int? SelectedWorld)
        {
            this.Context = Context;

            InitializeComponent();
            FillListBox();
        }

        private void FillListBox()
        {
            CharacterListBox.Items.Clear();

            if (SelectedWorld == null)
                return;

            foreach (var item in Items)
            {
                if(item.Content.ToString().Contains(SearchFilter) )
                {
                    CharacterListBox.Items.Add(item);
                }
            }
        }

        public void SelectedWorldChanged(int? SelectedWorld)
        {
            this.SelectedWorld = SelectedWorld;

            Items.Clear();

            if(SelectedWorld != null)
            {
                foreach (var item in Context.Character)
                {
                    if (item.WorldID == SelectedWorld)
                    {
                        var temp = new ListBoxItem();
                        temp.Content = $"{item.Name}  -  {item.Description}";
                        temp.Tag = item.CharacterID;

                        temp.MouseDoubleClick += new MouseButtonEventHandler(ItemDoubleClick);
                        temp.MouseUp += new MouseButtonEventHandler(ItemRightClick);

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

            var window = new Windows.CharacterWindow(this, Context, SelectedWorld, (int)s.Tag);
            window.Visibility = Visibility.Visible;
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new Windows.CharacterWindow(this, Context, SelectedWorld, null);
            window.Visibility = Visibility.Visible;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchFilter = SearchTextBox.Text;

            FillListBox();
            
        }

        private void CharacterListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CharacterListBox.SelectedItem == null)
                ConnectionButton.IsEnabled = false;
            else
                ConnectionButton.IsEnabled = true;
        }

        private void CharacterConnection_Click(object sender, RoutedEventArgs e)
        {
            if (CharacterListBox.SelectedItem == null)
                return;

            var s = (ListBoxItem)CharacterListBox.SelectedItem;

            var window = new Windows.ConnectionWindow(ConnectionTyp.Character_Character, (int)s.Tag, null, Context, SelectedWorld);
            window.Visibility = Visibility.Visible;
        }

        private void GroupConnection_Click(object sender, RoutedEventArgs e)
        {
            if (CharacterListBox.SelectedItem == null)
                return;

            var s = (ListBoxItem)CharacterListBox.SelectedItem;

            var window = new Windows.ConnectionWindow(ConnectionTyp.Character_Group, (int)s.Tag, null, Context, SelectedWorld);
            window.Visibility = Visibility.Visible;
        }

        private void LocationConnection_Click(object sender, RoutedEventArgs e)
        {
            if (CharacterListBox.SelectedItem == null)
                return;

            var s = (ListBoxItem)CharacterListBox.SelectedItem;

            var window = new Windows.ConnectionWindow(ConnectionTyp.Character_Location, (int)s.Tag, null, Context, SelectedWorld);
            window.Visibility = Visibility.Visible;
        }

        private void EventConnection_Click(object sender, RoutedEventArgs e)
        {
            if (CharacterListBox.SelectedItem == null)
                return;

            var s = (ListBoxItem)CharacterListBox.SelectedItem;

            var window = new Windows.ConnectionWindow(ConnectionTyp.Character_Event, (int)s.Tag, null, Context, SelectedWorld);
            window.Visibility = Visibility.Visible;
        }

        private void ConnectionButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectionContextMenu.IsOpen = true;
        }

        private void ItemRightClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                ItemContextMenu.IsOpen = true;
            }
        }

        private void AddCon_Click(object sender, RoutedEventArgs e)
        {
            ConnectionContextMenu.IsOpen = true;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var s = (ListBoxItem)CharacterListBox.SelectedItem;

            var window = new Windows.CharacterWindow(this, Context, SelectedWorld, (int)s.Tag);
            window.Visibility = Visibility.Visible;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var s = (ListBoxItem)CharacterListBox.SelectedItem;


            Context.Character.Remove(Context.Character.First(x => x.CharacterID == (int)s.Tag));
            Context.SaveChanges();
            UpdatePage();
        }
    }
}
