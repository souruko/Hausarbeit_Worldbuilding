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
    /// Interaktionslogik für ConnectionWindow.xaml
    /// </summary>
    /// 

    public partial class ConnectionWindow : Window
    {




        List<ListBoxItem> Items = new List<ListBoxItem>();
        string SearchFilter = "";

        WorldbuildingDBEntities Context;
        int? SelectedWorld;
        ConnectionTyp ConTyp;
        int FromID;
        int? ToID;

        public ConnectionWindow(ConnectionTyp ConTyp, int FromID, int? ToID, WorldbuildingDBEntities Context, int? SelectedWorld)
        {
            InitializeComponent();

            this.ConTyp = ConTyp;
            this.Context = Context;
            this.SelectedWorld = SelectedWorld;
            this.FromID = FromID;
            this.ToID = ToID;
       
            FillInformation();

            if(ToID == null)
            {
                DeleteButton.IsEnabled = false;
            }

        }

        private void FillListBox()
        {
            ConnectionListBox.Items.Clear();

            if (SelectedWorld == null)
                return;

            foreach (var item in Items)
            {
                if (item.Content.ToString().Contains(SearchFilter))
                {
                    ConnectionListBox.Items.Add(item);
                }
            }
        }

        private void FillInformation()
        {


            switch((int)ConTyp)
            {
                //From == Character
                case 0:
                case 1:
                case 2:
                case 3:
                    FromTextBox.Text = Context.Character.Where(x => x.WorldID == SelectedWorld && x.CharacterID == FromID).First().Name;

                    break;

                //From == Group
                case 4:
                case 5:
                case 6:
                case 7:
                    FromTextBox.Text = Context.Gruppe.Where(x => x.WorldID == SelectedWorld && x.GroupID == FromID).First().Name;

                    break;

                //From == Location
                case 8:
                case 9:
                case 10:
                case 11:
                    FromTextBox.Text = Context.Location.Where(x => x.WorldID == SelectedWorld && x.LocationID == FromID).First().Name;

                    break;

                //From == Event
                case 12:
                case 13:
                case 14:
                case 15:
                    FromTextBox.Text = Context.Event.Where(x => x.WorldID == SelectedWorld && x.EventID == FromID).First().Description;

                    break;
            }


            switch((int)ConTyp % 4)
            {
                //To == Character
                case 0:
                    if (SelectedWorld != null)
                    {
                        foreach (var item in Context.Character)
                        {
                            if (item.WorldID == SelectedWorld)
                            {
                                var temp = new ListBoxItem();
                                temp.Content = item.Name;
                                temp.Tag = item.CharacterID;

                                Items.Add(temp);

                            }
                        }
                        FillListBox();
                    }
                    break;

                //To == Group
                case 1:
                    if (SelectedWorld != null)
                    {
                        foreach (var item in Context.Gruppe)
                        {
                            if (item.WorldID == SelectedWorld)
                            {
                                var temp = new ListBoxItem();
                                temp.Content = item.Name;
                                temp.Tag = item.GroupID;

                                Items.Add(temp);

                            }
                        }
                        FillListBox();
                    }
                    break;

                //To == Location
                case 2:
                    if (SelectedWorld != null)
                    {
                        foreach (var item in Context.Location)
                        {
                            if (item.WorldID == SelectedWorld)
                            {
                                var temp = new ListBoxItem();
                                temp.Content = item.Name;
                                temp.Tag = item.LocationID;

                                Items.Add(temp);

                            }
                        }
                        FillListBox();
                    }
                    break;

                //To == Event
                case 3:
                    if (SelectedWorld != null)
                    {
                        foreach (var item in Context.Event)
                        {
                            if (item.WorldID == SelectedWorld)
                            {
                                var temp = new ListBoxItem();
                                temp.Content = item.Description;
                                temp.Tag = item.EventID;

                                Items.Add(temp);

                            }
                        }
                        FillListBox();
                    }
                    break;
            }
            
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchFilter = SearchTextBox.Text;

            FillListBox();

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
