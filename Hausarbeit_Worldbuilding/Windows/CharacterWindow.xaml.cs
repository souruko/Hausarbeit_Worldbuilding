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
    /// Interaktionslogik für CharacterWindow.xaml
    /// </summary>
    public partial class CharacterWindow : Window
    {
        Pages.IPage parent;
        WorldbuildingDBEntities Context;
        int? SelectedWorld;
        int? CharID;
        Character c;

        public CharacterWindow(Pages.IPage parent, WorldbuildingDBEntities Context, int? SelectedWorld, int? CharID)
        {
            InitializeComponent();

            this.parent = parent;
            this.Context = Context;
            this.SelectedWorld = SelectedWorld;
            this.CharID = CharID;

            if (CharID == null)
            {
                c = new Character();
                c.WorldID = (int)SelectedWorld;
            }
            else
            {
                this.c = Context.Character.First(x => x.WorldID == SelectedWorld && x.CharacterID == CharID);
            }

            NameTextBox.Text = c.Name;
            ApearanceTextBox.Text = c.Appearance;
            DescriptionTextBox.Text = c.Description;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            c.Name = NameTextBox.Text;
            c.Appearance = ApearanceTextBox.Text;
            c.Description = DescriptionTextBox.Text;

            if (CharID == null)
            {
                Context.Character.Add(c);
            }

            Context.SaveChanges();
            parent.UpdatePage();
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Context.Character.Remove(c);

            Context.SaveChanges();
            parent.UpdatePage();
            this.Close();
        }
    }
}
