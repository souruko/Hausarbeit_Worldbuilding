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


            switch ((int)ConTyp)
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


            switch ((int)ConTyp % 4)
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
            if (ConnectionListBox.SelectedItem != null)
            {
                if (ToID == null) //Create New
                {
                    switch ((int)ConTyp)
                    {
                        //From Character
                        case 0:
                            var i1 = new Character_Character();
                            i1.CharacterID1 = FromID;
                            i1.CharacterID2 = (int)((ListBoxItem)ConnectionListBox.SelectedItem).Tag;
                            i1.Description = DescriptionTextBox.Text;
                            Context.Character_Character.Add(i1);
                            break;

                        case 1:
                            var i2 = new Character_Group();
                            i2.CharacterID = FromID;
                            i2.GroupID = (int)((ListBoxItem)ConnectionListBox.SelectedItem).Tag;
                            i2.Description = DescriptionTextBox.Text;
                            Context.Character_Group.Add(i2);
                            break;

                        case 2:
                            var i3 = new Character_Location();
                            i3.CharacterID = FromID;
                            i3.LocationID = (int)((ListBoxItem)ConnectionListBox.SelectedItem).Tag;
                            i3.Description = DescriptionTextBox.Text;
                            Context.Character_Location.Add(i3);
                            break;

                        case 3:
                            var i4 = new Character_Event();
                            i4.CharacterID = FromID;
                            i4.EventID = (int)((ListBoxItem)ConnectionListBox.SelectedItem).Tag;
                            i4.Description = DescriptionTextBox.Text;
                            Context.Character_Event.Add(i4);
                            break;


                        //From Group
                        case 4:
                            var i5 = new Character_Group();
                            i5.GroupID = FromID;
                            i5.CharacterID = (int)((ListBoxItem)ConnectionListBox.SelectedItem).Tag;
                            i5.Description = DescriptionTextBox.Text;
                            Context.Character_Group.Add(i5);
                            break;

                        case 5:
                            var i6 = new Group_Group();
                            i6.GroupID1 = FromID;
                            i6.GroupID2 = (int)((ListBoxItem)ConnectionListBox.SelectedItem).Tag;
                            i6.Description = DescriptionTextBox.Text;
                            Context.Group_Group.Add(i6);
                            break;

                        case 6:
                            var i7 = new Location_Group();
                            i7.GroupID = FromID;
                            i7.LocationID = (int)((ListBoxItem)ConnectionListBox.SelectedItem).Tag;
                            i7.Description = DescriptionTextBox.Text;
                            Context.Location_Group.Add(i7);
                            break;

                        case 7:
                            var i8 = new Group_Event();
                            i8.GroupID = FromID;
                            i8.EventID = (int)((ListBoxItem)ConnectionListBox.SelectedItem).Tag;
                            i8.Description = DescriptionTextBox.Text;
                            Context.Group_Event.Add(i8);
                            break;


                        //From Location
                        case 8:
                            var i9 = new Character_Location();
                            i9.LocationID = FromID;
                            i9.CharacterID = (int)((ListBoxItem)ConnectionListBox.SelectedItem).Tag;
                            i9.Description = DescriptionTextBox.Text;
                            Context.Character_Location.Add(i9);
                            break;

                        case 9:
                            var i10 = new Location_Group();
                            i10.LocationID = FromID;
                            i10.GroupID = (int)((ListBoxItem)ConnectionListBox.SelectedItem).Tag;
                            i10.Description = DescriptionTextBox.Text;
                            Context.Location_Group.Add(i10);
                            break;

                        case 10:
                            var i11 = new Location_Location();
                            i11.LocationID1 = FromID;
                            i11.LocationID2 = (int)((ListBoxItem)ConnectionListBox.SelectedItem).Tag;
                            i11.Description = DescriptionTextBox.Text;
                            Context.Location_Location.Add(i11);
                            break;

                        case 11:
                            var i12 = new Location_Event();
                            i12.LocationID = FromID;
                            i12.EventID = (int)((ListBoxItem)ConnectionListBox.SelectedItem).Tag;
                            i12.Description = DescriptionTextBox.Text;
                            Context.Location_Event.Add(i12);
                            break;

                        //From Event
                        case 12:
                            var i13 = new Character_Event();
                            i13.EventID = FromID;
                            i13.CharacterID = (int)((ListBoxItem)ConnectionListBox.SelectedItem).Tag;
                            i13.Description = DescriptionTextBox.Text;
                            Context.Character_Event.Add(i13);
                            break;

                        case 13:
                            var i14 = new Group_Event();
                            i14.EventID = FromID;
                            i14.GroupID = (int)((ListBoxItem)ConnectionListBox.SelectedItem).Tag;
                            i14.Description = DescriptionTextBox.Text;
                            Context.Group_Event.Add(i14);
                            break;

                        case 14:
                            var i15 = new Location_Event();
                            i15.EventID = FromID;
                            i15.LocationID = (int)((ListBoxItem)ConnectionListBox.SelectedItem).Tag;
                            i15.Description = DescriptionTextBox.Text;
                            Context.Location_Event.Add(i15);
                            break;

                        case 15:
                            var i16 = new Event_Event();
                            i16.EventID1 = FromID;
                            i16.EventID2 = (int)((ListBoxItem)ConnectionListBox.SelectedItem).Tag;
                            i16.Description = DescriptionTextBox.Text;
                            Context.Event_Event.Add(i16);
                            break;
                    }
                    Context.SaveChanges();
                    this.Close();
                }
                else //Edit
                {

                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
