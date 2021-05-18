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

            foreach (var item in Context.Character)
            {
                if(item.Name.Contains(SearchFilter) && item.WorldID == SelectedWorld)
                {
                    var temp = new ListBoxItem();
                    temp.Content = item.Name;
                    temp.Tag = item.CharacterID;

                    CharacterListBox.Items.Add(temp);
                }
            }
        }

        public void SelectedWorldChanged(int? SelectedWorld)
        {
            this.SelectedWorld = SelectedWorld;

            FillListBox();
        }
    }
}
