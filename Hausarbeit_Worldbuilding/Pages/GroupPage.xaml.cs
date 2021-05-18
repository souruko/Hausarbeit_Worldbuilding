﻿using System;
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
    /// Interaktionslogik für GroupPage.xaml
    /// </summary>
    public partial class GroupPage : UserControl, IPage
    {
        int? SelectedWorld = null;
        string SearchFilter = "";
        WorldbuildingDBEntities Context;
        List<ListBoxItem> Items = new List<ListBoxItem>();

        public GroupPage(WorldbuildingDBEntities Context, int? SelectedWorld)
        {
            this.Context = Context;

            InitializeComponent();

            FillListBox();
        }

        private void FillListBox()
        {
            GroupListBox.Items.Clear();

            if (SelectedWorld == null)
                return;

            foreach (var item in Items)
            {
                if (item.Content.ToString().Contains(SearchFilter))
                {
                    GroupListBox.Items.Add(item);
                }
            }
        }

        public void SelectedWorldChanged(int? SelectedWorld)
        {
            this.SelectedWorld = SelectedWorld;

            Items.Clear();

            if (SelectedWorld != null)
            {
                foreach (var item in Context.Gruppe)
                {
                    if (item.WorldID == SelectedWorld)
                    {
                        var temp = new ListBoxItem();
                        temp.Content = item.Name;
                        temp.Tag = item.GroupID;
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

            var window = new Windows.GroupWindow(this, Context, SelectedWorld, (int)s.Tag);
            window.Visibility = Visibility.Visible;
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new Windows.GroupWindow(this, Context, SelectedWorld, null);
            window.Visibility = Visibility.Visible;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchFilter = SearchTextBox.Text;

            FillListBox();

        }
    }
}
